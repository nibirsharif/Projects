using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.Device.Location;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Net.NetworkInformation;

namespace VehicleTracker
{
    public partial class Page1 : PhoneApplicationPage
    {
        /// <summary>
        /// Mobile Service
        /// </summary>
        private IMobileServiceTable<Item> itemTable = App.MobileService.GetTable<Item>();

        // Network Interface Key
        bool isNetwork = NetworkInterface.GetIsNetworkAvailable();

        /// <summary>
        /// Constructor
        /// </summary>
        public Page1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Insert Item INTO Database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private async Task InsertItem(Item item)
        {
            try
            {
                // This code inserts a new TodoItem into the database. When the operation completes
                // and Mobile Services has assigned an Id, the item is added to the CollectionView
                await itemTable.InsertAsync(item);
            }
            catch (Exception)
            {
            }
        }

        // My current location
        private GeoCoordinate MyCoordinate = null;

        // Reverse geocode query
        private ReverseGeocodeQuery MyReverseGeocodeQuery = null;

        // Geo Location Accuracy
        private double _accuracy = 0.0;

        //
        Geolocator geolocator = new Geolocator();

        /// <summary>
        /// Save Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Save_Click(object sender, EventArgs e)
        {
            indicator.IsVisible = true;
            //var todoItem = new TodoItem { Text = TextInput.Text };
            //await InsertTodoItem(todoItem);

            //Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracy = PositionAccuracy.High;

            try
            {
	            Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
	            _accuracy = currentPosition.Coordinate.Accuracy;

	            //MyCoordinate = new GeoCoordinate(currentPosition.Coordinate.Latitude, currentPosition.Coordinate.Longitude);
                MyCoordinate = new GeoCoordinate(currentPosition.Coordinate.Point.Position.Latitude, currentPosition.Coordinate.Point.Position.Longitude);

	            if (MyReverseGeocodeQuery == null || !MyReverseGeocodeQuery.IsBusy)
	            {
		            MyReverseGeocodeQuery = new ReverseGeocodeQuery();
		            MyReverseGeocodeQuery.GeoCoordinate = new GeoCoordinate(MyCoordinate.Latitude, MyCoordinate.Longitude);
		            MyReverseGeocodeQuery.QueryCompleted += ReverseGeocodeQuery_QueryCompleted;
		            MyReverseGeocodeQuery.QueryAsync();
	            }

            }
            catch (Exception ex)
            {
                if (!isNetwork)
                {
                    MessageBox.Show("Please check your internet connectivity.");
                }

                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master   switch is off
                    MessageBox.Show("location  is disabled in phone settings.");
                }
            }
            indicator.IsVisible = false;
        }

        /// <summary>
        /// Reverse GeocodeQuery Query Completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ReverseGeocodeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            //indicator.IsVisible = true;
            try
            {
                Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                if (e.Error == null)
                {
                    if (e.Result.Count > 0)
                    {
                        string _message = "", _description = "", _density = "";
                        if (low.IsChecked == true)
                        {
                            _message = "Traffic Jam";
                            _density = "Low";
                        }
                        else if (medium.IsChecked == true)
                        {
                            _message = "Traffic Jam";
                            _density = "Medium";
                        }
                        else if (high.IsChecked == true)
                        {
                            _message = "Traffic Jam";
                            _density = "High";
                        }
                        else if (emergency.IsChecked == true)
                        {
                            _message = "Emergency";
                            _description = em.Text;
                        }
                        else if (help.IsChecked == true)
                        {
                            _message = "Help";
                            _description = hp.Text;
                        }

                        MapAddress address = e.Result[0].Information.Address;
                        //CurrentLocTextBlock.Text = "Current Location: " + address.City + ", " + address.State;
                        string loc = address.City + ", " + address.District + address.Province + ", " + address.Street;

                        if (!string.IsNullOrEmpty(_message) == true)
                        {
                            var item = new Item { Message = _message, Description = _description, Location = loc, Lat = currentPosition.Coordinate.Point.Position.Latitude, Long = currentPosition.Coordinate.Point.Position.Longitude, UserId = App.MobileService.CurrentUser.UserId, Density = _density, Date = System.DateTime.UtcNow };
                            await InsertItem(item);

                            MessageBox.Show("Current Location: " + address.City + ", " + address.District + ", " + address.Province + ", " + address.Street);
                        }
                        else
                        {
                            MessageBox.Show("Please select a option.");
                        }
                    }
                }
            }
            catch(Exception)
            {
            }
            //indicator.IsVisible = false;
        }
        
        /// <summary>
        /// On Navigated To
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.AcquirePushChannel();
        }

        /// <summary>
        /// On Application Bar State Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplicationBarStateChanged(object sender, ApplicationBarStateChangedEventArgs e)
        {
            var appBar = sender as ApplicationBar;
            if (appBar == null) return;

            appBar.Opacity = e.IsMenuVisible ? 1 : .65;
        }
    }
}