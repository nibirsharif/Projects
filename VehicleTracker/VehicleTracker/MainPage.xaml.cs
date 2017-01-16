#define DEBUG_AGENT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using VehicleTracker.Resources;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using System.Windows.Threading;
using System.Windows.Media;
using Geo;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using Microsoft.Phone.Scheduler;

namespace VehicleTracker
{
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// Mobile Service
        /// </summary>
        private MobileServiceCollection<Item, Item> items;
        private IMobileServiceTable<Item> itemTable = App.MobileService.GetTable<Item>();

        private MobileServiceCollection<Speed, Speed> _speeds;
        private IMobileServiceTable<Speed> speedTable = App.MobileService.GetTable<Speed>();

        private MobileServiceCollection<VisitedPlace, VisitedPlace> _visitedPlace;
        private IMobileServiceTable<VisitedPlace> visitedPlaceTable = App.MobileService.GetTable<VisitedPlace>();

        /// <summary>
        /// GeoLocation Service
        /// </summary>
        private GeoCoordinateWatcher _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
        private Pushpin _pushpin;
        private MapLayer _mapLayer;
        private MapOverlay _overlay;
        private MapPolyline _line;
        private DispatcherTimer _timer = new DispatcherTimer();
        private long _startTime;
        private bool _a;

        private string _startLocation;
        private string _endLocation;

        Windows.Devices.Geolocation.Geolocator geolocator = new Windows.Devices.Geolocation.Geolocator();

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            //this.Loaded += MainPage_Loaded;

            // create a line which illustrates the run
            _line = new MapPolyline();
            _line.StrokeColor = Colors.Blue;
            _line.StrokeThickness = 7;
            Map.MapElements.Add(_line);

            _watcher.PositionChanged += Watcher_PositionChanged;

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            // Map Token
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = "ec74fb70-8b8b-4eea-8f9c-be6e84bf869b";
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = "2jkeQyHh7EgWG2iTvcVF5g";
        }

        /// <summary>
        /// Get Run Time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan runTime = TimeSpan.FromMilliseconds(System.Environment.TickCount - _startTime);
            timeLabel.Text = runTime.ToString(@"hh\:mm\:ss");
        }

        // My current location
        private GeoCoordinate MyCoordinate = null;

        // Reverse geocode query
        private Microsoft.Phone.Maps.Services.ReverseGeocodeQuery MyReverseGeocodeQuery = null;

        // Accuracy label
        private double _accuracy = 0.0;

        /// <summary>
        /// Start Button Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_timer.IsEnabled)
                {
                    // Geo Location for Starting Position
                    geolocator.DesiredAccuracy = Windows.Devices.Geolocation.PositionAccuracy.High;

                    Windows.Devices.Geolocation.Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                    _accuracy = currentPosition.Coordinate.Accuracy;

                    MyCoordinate = new GeoCoordinate(currentPosition.Coordinate.Point.Position.Latitude, currentPosition.Coordinate.Point.Position.Longitude);

                    if (MyReverseGeocodeQuery == null || !MyReverseGeocodeQuery.IsBusy)
                    {
                        MyReverseGeocodeQuery = new Microsoft.Phone.Maps.Services.ReverseGeocodeQuery();
                        MyReverseGeocodeQuery.GeoCoordinate = new GeoCoordinate(MyCoordinate.Latitude, MyCoordinate.Longitude);
                        MyReverseGeocodeQuery.QueryCompleted += ReverseGeocodeQuery_QueryCompleted;
                        MyReverseGeocodeQuery.QueryAsync();
                    }

                    // Watcher
                    _a = true;
                    _watcher.Stop();
                    _timer.Stop();
                    StartButton.Content = "Start";

                    // Create a map marker
                    _pushpin = new Pushpin();
                    _pushpin.Content = "My Car";
                    _pushpin.Background = new SolidColorBrush(Colors.Blue);

                    // Create a MapOverlay and add marker.
                    _overlay = new MapOverlay();
                    _overlay.Content = _pushpin;
                    _overlay.GeoCoordinate = new GeoCoordinate(Map.Center.Latitude, Map.Center.Longitude);
                    _overlay.PositionOrigin = new Point(0.0, 1.0);

                    // create a map layer
                    _mapLayer = new MapLayer();
                    _mapLayer.Clear();
                    _mapLayer.Add(_overlay);
                    Map.Layers.Add(_mapLayer);
                }
                else
                {
                    indicator.IsVisible = true;
                    // Geo Location for Ending Location
                    geolocator.DesiredAccuracy = Windows.Devices.Geolocation.PositionAccuracy.High;

                    Windows.Devices.Geolocation.Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                    _accuracy = currentPosition.Coordinate.Accuracy;

                    MyCoordinate = new GeoCoordinate(currentPosition.Coordinate.Point.Position.Latitude, currentPosition.Coordinate.Point.Position.Longitude);

                    if (MyReverseGeocodeQuery == null || !MyReverseGeocodeQuery.IsBusy)
                    {
                        MyReverseGeocodeQuery = new Microsoft.Phone.Maps.Services.ReverseGeocodeQuery();
                        MyReverseGeocodeQuery.GeoCoordinate = new GeoCoordinate(MyCoordinate.Latitude, MyCoordinate.Longitude);
                        MyReverseGeocodeQuery.QueryCompleted += ReverseGeocodeQuery_QueryCompleted1;
                        MyReverseGeocodeQuery.QueryAsync();
                    }

                    // Watcher
                    _a = false;
                    _watcher.Start();
                    _timer.Start();
                    _startTime = System.Environment.TickCount;
                    StartButton.Content = "Stop";
                    indicator.IsVisible = false;
                }

                averageSpeed();
            }
            catch(Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master   switch is off
                    MessageBox.Show("location  is disabled in phone settings.");
                }
            }
            //averageSpeed();
            //visitedPlace();
        }

        /// <summary>
        /// Start Location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ReverseGeocodeQuery_QueryCompleted(object sender, Microsoft.Phone.Maps.Services.QueryCompletedEventArgs<IList<Microsoft.Phone.Maps.Services.MapLocation>> e)
        {
            try
            {
                Windows.Devices.Geolocation.Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                if (e.Error == null)
                {
                    if (e.Result.Count > 0)
                    {
                        Microsoft.Phone.Maps.Services.MapAddress address = e.Result[0].Information.Address;
                        _endLocation = address.City + ", " + address.District + address.Province + ", " + address.Street;
                        MessageBox.Show("Current Location: " + address.City + ", " + address.District + ", " + address.Province + ", " + address.Street);
                        visitedPlace();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// End Location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ReverseGeocodeQuery_QueryCompleted1(object sender, Microsoft.Phone.Maps.Services.QueryCompletedEventArgs<IList<Microsoft.Phone.Maps.Services.MapLocation>> e)
        {
            try
            {
                Windows.Devices.Geolocation.Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                if (e.Error == null)
                {
                    if (e.Result.Count > 0)
                    {
                        Microsoft.Phone.Maps.Services.MapAddress address = e.Result[0].Information.Address;
                        _startLocation = address.City + ", " + address.District + address.Province + ", " + address.Street;
                        MessageBox.Show("Current Location: " + address.City + ", " + address.District + ", " + address.Province + ", " + address.Street);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Average Speed
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        private async Task InsertItem(Speed speed)
        {
            try
            {
                // This code inserts a new TodoItem into the database. When the operation completes
                // and Mobile Services has assigned an Id, the item is added to the CollectionView
                await speedTable.InsertAsync(speed);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Visited Places
        /// </summary>
        /// <param name="visitedPlace"></param>
        /// <returns></returns>
        private async Task InsertItem1(VisitedPlace visitedPlace)
        {
            try
            {
                // This code inserts a new TodoItem into the database. When the operation completes
                // and Mobile Services has assigned an Id, the item is added to the CollectionView
                await visitedPlaceTable.InsertAsync(visitedPlace);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Background Task
        /// </summary>
        #region Background Task

        PeriodicTask periodicTask = null;
        string periodicTaskName = "NotificationService";

        private void RemoveAgent(string name)
        {
            try
            {
                ScheduledActionService.Remove(name);
            }
            catch (Exception)
            {
            }
        }

        private void StopTracking()
        {
            RemoveAgent(periodicTaskName);
        }

        private void StartTracking()
        {
            periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;

            // If the task already exists and the IsEnabled property is false, background
            // agents have been disabled by the user
            if (periodicTask != null && !periodicTask.IsEnabled)
            {
                MessageBox.Show("Background agents for this application have been disabled by the user.");
                return;
            }

            // If the task already exists and background agents are enabled for the
            // application, you must remove the task and then add it again to update 
            // the schedule
            if (periodicTask != null && periodicTask.IsEnabled)
            {
                RemoveAgent(periodicTaskName);
            }

            periodicTask = new PeriodicTask(periodicTaskName);

            // The description is required for periodic agents. This is the string that the user
            // will see in the background services Settings page on the device.
            periodicTask.Description = "Notification Service.";
            ScheduledActionService.Add(periodicTask);

            // If debugging is enabled, use LaunchForTest to launch the agent in one minute.
#if(DEBUG_AGENT)
            ScheduledActionService.LaunchForTest(periodicTaskName, TimeSpan.FromSeconds(60));
#endif
        }

        #endregion

        /// <summary>
        /// Alert Button Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlertButton_Click(object sender, RoutedEventArgs e)
        {
            periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;

            if (periodicTask == null)
            {
                StartTracking();
                alert.Content = "On";
            }
            else
            {
                StopTracking();
                alert.Content = "Off";
            } 
        }

        /// <summary>
        /// ID_CAP_LOCATION
        /// </summary>
        private double _kilometres;
        private long _previousPositionChangeTick;
        private TimeSpan _time;
        private double _speed;
        private double _averageSpeed;

        /// <summary>
        /// List Table for Average Speed
        /// </summary>
        List<double> speeds = new List<double>();
        List<double> sspeeds = new List<double>();

        /// <summary>
        /// Watcher for Position Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            try
            {
                var coord = new GeoCoordinate(e.Position.Location.Latitude, e.Position.Location.Longitude);

                if (_line.Path.Count > 0)
                {
                    var previousPoint = _line.Path.Last();
                    var distance = coord.GetDistanceTo(previousPoint);
                    var millisPerKilometer = (1000.0 / distance) * (System.Environment.TickCount - _previousPositionChangeTick);
                    _kilometres += distance / 1000.0;

                    _time = TimeSpan.FromMilliseconds(millisPerKilometer);
                    _speed = 60 / _time.TotalMinutes;

                    paceLabel.Text = TimeSpan.FromMilliseconds(millisPerKilometer).ToString(@"mm\:ss");
                    //paceLabel.Text = _speed.ToString("00.00");

                    distanceLabel.Text = string.Format("{0:f2}", _kilometres);
                    //caloriesLabel.Text = string.Format("{0:f0}", _kilometres * 65);
                    caloriesLabel.Text = string.Format("{0:f2}", _speed);

                    if (_speed < 80)
                    {
                        a.Stroke = new SolidColorBrush(Colors.Green);
                        b.Text = "Fine!";
                    }
                    else if (_speed > 80 && _speed < 100)
                    {
                        a.Stroke = new SolidColorBrush(Colors.Green);
                        b.Text = "Fast!";
                    }
                    else if (_speed > 100 && _speed < 120)
                    {
                        a.Stroke = new SolidColorBrush(Colors.Yellow);
                        b.Text = "Too Fast!";
                    }
                    else if (_speed > 120 && _speed < 140)
                    {
                        a.Stroke = new SolidColorBrush(Colors.Yellow);
                        b.Text = "Extremely Fast!";
                    }
                    else if (_speed > 140)
                    {
                        a.Stroke = new SolidColorBrush(Colors.Red);
                        b.Text = "Dangerous!";
                    }

                    do
                    {
                        speeds.Add(_speed);
                    } while (_a == true);

                    PositionHandler handler = new PositionHandler();
                    var heading = handler.CalculateBearing(new Position(previousPoint), new Position(coord));
                    Map.SetView(coord, Map.ZoomLevel, heading, MapAnimationKind.Parabolic);
                }
                else
                {
                    Map.Center = coord;
                }

                _line.Path.Add(coord);
                _previousPositionChangeTick = System.Environment.TickCount;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Average Speed
        /// </summary>
        private async void averageSpeed()
        {
            if (speeds.Count >= 1)
            {
                _averageSpeed = speeds.Average();
                var speed = new Speed { UserId = App.MobileService.CurrentUser.UserId, Speeds = _averageSpeed };
                await InsertItem(speed);
            }

            // Server Speed
            try
            {
                _speeds = await speedTable.ToCollectionAsync();
            }
            catch (Exception)
            {
            }

            foreach (var item in _speeds)
            {
                sspeeds.Add(item.Speeds);
            }

            if (_speed > sspeeds.Average())
            {
                MessageBox.Show("Slow down! Your are driving beyound your limit.");
            }

            MessageBox.Show("Average speed: " + sspeeds.Average());
        }

        /// <summary>
        /// Visited Places
        /// </summary>
        private async void visitedPlace()
        {
            var visitedPlaces = new VisitedPlace { UserId = App.MobileService.CurrentUser.UserId, Date = System.DateTime.UtcNow , StartLocation = _startLocation, EndLocation = _endLocation };
            await InsertItem1(visitedPlaces);
        }

        /// <summary>
        /// On Navigate Method
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            try
            {
                string parameterValue = NavigationContext.QueryString["parameter"];

                if (parameterValue == "Facebook")
                {
                    await FacebookAuthenticate();
                }
                if (parameterValue == "Twitter")
                {
                    await TwitterAuthenticate();
                }
            }
            catch (Exception)
            {
            }

            // When we navigate to the page - put the semi-completed log text in the display

            // Get a reference to the parent application
            App thisApp = App.Current as App;

            // Now set up the text in the background text button

            periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;

            if (periodicTask == null)
            {
                alert.Content = "On";
            }
            else
            {
                alert.Content = "Off";
            }
        }

        /// <summary>
        /// On Back key Pressed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            e.Cancel = true;

            MessageBoxResult result = MessageBox.Show("Do you want to exit?", "Vehicle Tracker", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                Application.Current.Terminate();
            }
        }

        /*async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (geolocator.LocationStatus == Windows.Devices.Geolocation.PositionStatus.Disabled)
            {
                MessageBoxResult result = MessageBox.Show("The Location Services is turned Off.");
            }
        }*/

        /// <summary>
        /// Facebook Authentication
        /// </summary>
        /// <returns></returns>
        private async Task FacebookAuthenticate()
        {
            while (App.MobileService.CurrentUser == null)
            {
                string message;
                try
                {
                    await App.MobileService.LoginAsync(MobileServiceAuthenticationProvider.Facebook);
                    message = string.Format("User Authenticated - {0}", App.MobileService.CurrentUser.UserId);
                }
                catch (InvalidOperationException e)
                {
                    message = "Login unsuccessful\n" + e.Message;
                }

                MessageBox.Show(message);
            }
        }

        /// <summary>
        /// Twitter Authentication
        /// </summary>
        /// <returns></returns>
        private async Task TwitterAuthenticate()
        {
            while (App.MobileService.CurrentUser == null)
            {
                string message;
                try
                {
                    await App.MobileService.LoginAsync(MobileServiceAuthenticationProvider.Twitter);
                    message = string.Format("User Authenticated - {0}", App.MobileService.CurrentUser.UserId);
                }
                catch (InvalidOperationException e)
                {
                    message = "Login unsuccessful\n" + e.Message;
                }

                MessageBox.Show(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page1_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }

        private void Page2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
        }

        private void Page3_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page3.xaml", UriKind.Relative));
        }

        private void Page4_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page4.xaml", UriKind.Relative));
        }

        private void Page5_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page5.xaml", UriKind.Relative));
        }

        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Zoom Level Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sliderZoomLevel_ValueChanged(object sender, EventArgs e)
        {
            if (sliderZoomLevel != null)
            {
                Map.ZoomLevel = Convert.ToInt32(sliderZoomLevel.Value);
            }
        }

        /// <summary>
        /// Application Bar State Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplicationBarStateChanged(object sender, ApplicationBarStateChangedEventArgs e)
        {
            //var appBar = sender as ApplicationBar;
            //if (appBar == null) return;

            //appBar.Opacity = e.IsMenuVisible ? 1 : .65;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (App.MobileService.CurrentUser != null && App.MobileService.CurrentUser.UserId != null)
                    App.MobileService.Logout();

                MessageBox.Show("Logged out.");
                NavigationService.Navigate(new Uri("/SignIn.xaml", UriKind.Relative));
            }
            catch (InvalidOperationException iopEx)
            {
                MessageBox.Show(string.Format("Error Occured: \n {0}", iopEx.ToString()));
            }
        }
    }
}