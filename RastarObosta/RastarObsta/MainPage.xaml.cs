using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RastarObsta.Resources;
using Windows.Devices.Geolocation;
using System.Windows.Media;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using Microsoft.Phone.Tasks;
using GoogleAds;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Net.NetworkInformation;
using Newtonsoft.Json.Linq;

namespace RastarObsta
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool _isLocationAllowed = false;
        private ProgressIndicator ProgressIndicator = null;
        public Geolocator geolocator = null;
        private double _accuracy = 0.0;
        private GeoCoordinate MyCoordinate = null;
        private bool isFirstTimeMapLoading = true;
        private MapLayer mapLayer = null;
        private bool isTappedPinIsCurrentPosition = false;
        private List<GeoCoordinate> MyCoordinates = new List<GeoCoordinate>();
        List<Class1> listA = new List<Class1>();
        List<Class2> rate = new List<Class2>();
        List<Rate> r = new List<Rate>();

        private int counter = 0;
        private int temp = 0;

        private int minLevel = 13;
        private int maxLevel = 18;
        private int count = 0;

        Boolean b = false;

        bool isNetwork = NetworkInterface.GetIsNetworkAvailable();

        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isNetwork)
            {
                MessageBox.Show("Please check your internet connectivity.");
                Application.Current.Terminate();
            }

            try
            {
                IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
                DateTime start = DateTime.Parse(appSettings["date"].ToString());
                DateTime end = DateTime.Now;

                if (Math.Abs((end.Date - start.Date).Days) > 0)
                {
                    appSettings["date"] = end.Date.ToString();
                    appSettings["userData"] = "2".ToString();
                    appSettings.Save();
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
                    MessageBox.Show("Location is disabled in phone settings.");
                }
            }
        }

        private void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = "b266b7e3-c2b5-4e87-924e-feb54c3d391a";
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = "ldysj2C0ITlDgTD3FO0zeg";
        }

        private void ZoomLevelChanged(object sender, EventArgs e)
        {
            //if (MyMap.ZoomLevel < minLevel)
            //    MyMap.ZoomLevel = minLevel;
            //if (MyMap.ZoomLevel > maxLevel)
            //    MyMap.ZoomLevel = maxLevel;
        }

        private void OnApplicationBarStateChanged(object sender, ApplicationBarStateChangedEventArgs e)
        {
            //var appBar = sender as ApplicationBar;
            //if (appBar == null) return;

            //appBar.Opacity = e.IsMenuVisible ? 1 : .65;
        }

        private void LocationUsage_Click(object sender, RoutedEventArgs e)
        {
            LocationPanel.Visibility = Visibility.Collapsed;
            ApplicationBar.IsVisible = true;
            if (sender == AllowButton)
            {
                try
                {
                    _isLocationAllowed = true;
                    GetCurrentCoordinate();
                }
                catch (Exception ex)
                {
                    if (!isNetwork)
                    {
                        MessageBox.Show("Please check your internet connectivity.");
                    }

                    if ((uint)ex.HResult == 0x80004004)
                    {
                        MessageBox.Show("Location is disabled in phone settings.");
                    }
                }
            }
            else
            {
                Application.Current.Terminate();
            }
        }

        public async void GetCurrentCoordinate()
        {
            try
            {
                popupForMessage1.IsOpen = true;
                geolocator = new Geolocator();

                if (geolocator.LocationStatus == PositionStatus.Disabled)
                    throw new System.Exception();

                geolocator.DesiredAccuracy = PositionAccuracy.High;
                geolocator.MovementThreshold = 10;
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(3));
                geolocator.PositionChanged += geolocator_PositionChanged;

                popupForMessage1.IsOpen = false;
                popupForMessage2.IsOpen = true;
            }
            catch (Exception)
            {
                if (geolocator.LocationStatus == PositionStatus.Disabled)
                {
                    MessageBox.Show("Location is disabled in phone settings.");
                    Application.Current.Terminate();
                }
            }
        }

        public void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            try
            {
                Dispatcher.BeginInvoke(() =>
                {
                    _accuracy = args.Position.Coordinate.Accuracy;
                    MyCoordinate = new GeoCoordinate(args.Position.Coordinate.Latitude, args.Position.Coordinate.Longitude);
                    if (isFirstTimeMapLoading)
                    {
                        DrawMapMarkers();
                        MyMap.SetView(MyCoordinate, 16, MapAnimationKind.Parabolic);
                        isFirstTimeMapLoading = false;
                    }
                });
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    MessageBox.Show("Location is disabled in phone settings.");
                }
            }
        }

        private async void DrawMapMarkers()
        {
            try
            {
                Geolocator geolocator = new Geolocator();
                Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));

                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
                webClient.DownloadStringAsync(new Uri("http://api.rastarobosta.com/marker/near-me/3ebb60d6dbe8e49b?lat=" + currentPosition.Coordinate.Latitude + "&lng="+currentPosition.Coordinate.Longitude));
            }
            catch
            {
            }
        }

        async void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                MyMap.Layers.Clear();

                mapLayer = new MapLayer();

                try
                {
                    string json = e.Result;
                    if (!string.IsNullOrEmpty(json))
                    {
                        Class1 results = Newtonsoft.Json.JsonConvert.DeserializeObject<Class1>(json);
                        Newtonsoft.Json.Linq.JObject o = Newtonsoft.Json.Linq.JObject.Parse(json);

                        JArray items = (JArray)o["markers"];
                        int i = 0, length = items.Count;

                        while (i < length)
                        //for (int i = 0; i < json.Length; i++)
                        {
                            listA.Add(new Class1() { id = int.Parse(o["markers"][i]["id"].ToString()), lat = double.Parse(o["markers"][i]["latitude"].ToString()), lon = double.Parse(o["markers"][i]["longitude"].ToString()), ori = double.Parse(o["markers"][i]["orientation"].ToString()) });
                            i++;
                        }
                    }
                }
                catch { }

                for (int i = 0; i < 20; i++)
                {
                    try
                    {

                        double latitude = listA[i].lat;
                        double longitude = listA[i].lon;

                        GeoCoordinate coordinate = new GeoCoordinate(latitude, longitude);
                        MyCoordinates.Add(coordinate);

                        DrawMapMarker(MyCoordinates[i], listA[i].id, listA[i].ori, Colors.Blue, mapLayer);
                    }
                    catch { }
                }

                if (MyCoordinate != null)
                {
                    DrawMapMarker(MyCoordinate, 0, 0, Colors.Red, mapLayer);
                }

                MyMap.Layers.Add(mapLayer);
                popupForMessage2.IsOpen = false;

                popupForMessage11.IsOpen = true;
                await Task.Delay(TimeSpan.FromSeconds(10));
                popupForMessage11.IsOpen = false;
            }
            catch { }
        }

        async void Delay()
        {
            await Task.Delay(TimeSpan.FromSeconds(15));
        }

        private async void DrawMapMarker(GeoCoordinate coordinate, int num ,double ori, Color color, MapLayer layer)
        {
            Rectangle rectangle = new Rectangle();

            BitmapImage arrImg;

            if (color == Colors.Red)
            {
                arrImg = new BitmapImage(new Uri("/Assets/Icons/location.png", UriKind.RelativeOrAbsolute));
                rectangle.Height = 20;
                rectangle.Width = 20;
            }
            else
            {
                //await Task.Delay(TimeSpan.FromSeconds(1));
                arrImg = new BitmapImage(new Uri("/Assets/Arrows20/G.png", UriKind.RelativeOrAbsolute));
                rectangle.Height = 35;
                rectangle.Width = 35;
                RotateTransform myRotateTransform = new RotateTransform();
                myRotateTransform.Angle = ori;

                rectangle.RenderTransform = myRotateTransform;
            }

            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = arrImg;
            rectangle.Fill = imgBrush;

            string[] s = { color.ToString(), num.ToString(), coordinate.Latitude.ToString(), coordinate.Longitude.ToString() };
            rectangle.Tag = s;

            rectangle.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(Marker_Click);

            MapOverlay overlay = new MapOverlay();
            overlay.Content = rectangle;
            overlay.GeoCoordinate = new GeoCoordinate(coordinate.Latitude, coordinate.Longitude);
            overlay.PositionOrigin = new Point(0.0, 1.0);

            layer.Add(overlay);
        }
        private void Marker_Click(object sender, EventArgs e)
        {
            popupForMessage11.IsOpen = false;
            popupForMessage5.IsOpen = false;
            Rectangle rectangle = (Rectangle)sender;

            string[] s = (string[])rectangle.Tag;         

            if (s[0].Equals("#FFFF0000") || s[0].Equals("#FF800080"))
            {
                if (s[0].Equals("#FFFF0000"))
                    isTappedPinIsCurrentPosition = true;
            }
            else
            {
                var markerDetails = from marker in listA.OrderByDescending(x => x.id).ToList()
                                    where marker.id == int.Parse(s[1])
                                    select new Class1
                                    {
                                        id = marker.id,
                                        lat = marker.lat,
                                        lon = marker.lon,
                                        ori = marker.ori
                                    };

                double la = 0;
                double lo = 0;
                int n = 0;
                double ori = 0;

                foreach (var marker in markerDetails)
                {
                    n = marker.id;
                    la = marker.lat;
                    lo = marker.lon;
                    ori = marker.ori;
                }

                DrawMapMarker1(la, lo, ori, mapLayer);
                temp = n;

                popupForMarker.IsOpen = true;
                ContentPanel.IsHitTestVisible = false;
            }
        }

        MapLayer ml = null;

        private void DrawMapMarker1(double lat, double lon, double ori, MapLayer layer)
        {
            ml = new MapLayer();
            Rectangle rectangle = new Rectangle();
            BitmapImage arrImg;

            arrImg = new BitmapImage(new Uri("/Assets/Arrows20/Selected.png", UriKind.RelativeOrAbsolute));
            rectangle.Height = 35;
            rectangle.Width = 35;
            RotateTransform myRotateTransform = new RotateTransform();
            myRotateTransform.Angle = ori;

            rectangle.RenderTransform = myRotateTransform;

            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = arrImg;
            rectangle.Fill = imgBrush;

            MapOverlay overlay = new MapOverlay();
            overlay.Content = rectangle;
            overlay.GeoCoordinate = new GeoCoordinate(lat, lon);
            overlay.PositionOrigin = new Point(0.0, 1.0);

            ml.Add(overlay);
            MyMap.Layers.Add(ml);
        }

        string hexString = string.Empty;

        private void DeviceID()
        {
            object uniqueId;
            
            if (Microsoft.Phone.Info.DeviceExtendedProperties.TryGetValue("DeviceUniqueId", out uniqueId))
                hexString = BitConverter.ToString((byte[])uniqueId).Replace("-", string.Empty);
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (b == true)
                {
                    var itemToRemove = r.Single(rt => rt.marker == temp);
                    if (itemToRemove != null)
                        r.Remove(itemToRemove);
                    counter -= 1;
                }
            }
            catch { }

            DeviceID();
            rate.Add(new Class2 { IMI = hexString, ID = temp, Rate = int.Parse(btn1.Content.ToString()) });
            r.Add(new Rate { marker = temp, rate = int.Parse(btn1.Content.ToString()) });

            b = true;
            counter += 1;
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (b == true)
                {
                    var itemToRemove = r.Single(rt => rt.marker == temp);
                    if (itemToRemove != null)
                        r.Remove(itemToRemove);
                    counter -= 1;
                }
            }
            catch { }

            DeviceID();
            rate.Add(new Class2 { IMI = hexString, ID = temp, Rate = int.Parse(btn2.Content.ToString()) });
            r.Add(new Rate { marker = temp, rate = int.Parse(btn2.Content.ToString()) });

            b = true;
            counter += 1;
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (b == true)
                {
                    var itemToRemove = r.Single(rt => rt.marker == temp);
                    if (itemToRemove != null)
                        r.Remove(itemToRemove);
                    counter -= 1;
                }
            }
            catch { }
            
            DeviceID();
            rate.Add(new Class2 { IMI = hexString, ID = temp, Rate = int.Parse(btn3.Content.ToString()) });
            r.Add(new Rate { marker = temp, rate = int.Parse(btn3.Content.ToString()) });

            b = true;
            counter += 1;
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (b == true)
                {
                    var itemToRemove = r.Single(rt => rt.marker == temp);
                    if (itemToRemove != null)
                        r.Remove(itemToRemove);
                    counter -= 1;
                }
            }
            catch { }

            DeviceID();
            rate.Add(new Class2 { IMI = hexString, ID = temp, Rate = int.Parse(btn4.Content.ToString()) });
            r.Add(new Rate { marker = temp, rate = int.Parse(btn4.Content.ToString()) });

            b = true;
            counter += 1;
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (b == true)
                {
                    var itemToRemove = r.Single(rt => rt.marker == temp);
                    if (itemToRemove != null)
                        r.Remove(itemToRemove);
                    counter -= 1;
                }
            }
            catch { }
            
            DeviceID();
            rate.Add(new Class2 { IMI = hexString, ID = temp, Rate = int.Parse(btn5.Content.ToString()) });
            r.Add(new Rate { marker = temp, rate = int.Parse(btn5.Content.ToString()) });

            b = true;
            counter += 1;
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (b == true)
                {
                    var itemToRemove = r.Single(rt => rt.marker == temp);
                    if (itemToRemove != null)
                        r.Remove(itemToRemove);
                    counter -= 1;
                }
            }
            catch { }

            DeviceID();
            rate.Add(new Class2 { IMI = hexString, ID = temp, Rate = int.Parse(btn6.Content.ToString()) });
            r.Add(new Rate { marker = temp, rate = int.Parse(btn6.Content.ToString()) });

            b = true;
            counter += 1;
        }

        private async void CancelButton1_Click(object sender, RoutedEventArgs e)
        {
            if (counter >=2 && b == true)
            {
                popupForMessage5.IsOpen = false;

                Class4 c4 = new Class4
                {
                    uuid = hexString,
                    rates = r,
                    rate = 1
                };

                string jsonData = JsonConvert.SerializeObject(c4);

                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;

                Uri uri = new Uri("http://api.rastarobosta.com/marker", UriKind.Absolute);
                webClient.UploadStringAsync(uri, "POST", jsonData);

                popupForMarker.IsOpen = false;
                ContentPanel.IsHitTestVisible = true;

                popupForMessage6.IsOpen = true;
                await Task.Delay(TimeSpan.FromSeconds(3));
                popupForMessage6.IsOpen = false;
                popupForMessage7.IsOpen = true;
                await Task.Delay(TimeSpan.FromSeconds(3));
                popupForMessage7.IsOpen = false;

                NavigationService.Navigate(new Uri("/Page3.xaml", UriKind.Relative));
            }
            else if (counter == 1 && b == true)
            {
                popupForMarker.IsOpen = false;
                ContentPanel.IsHitTestVisible = true;
                popupForMessage5.IsOpen = true;
                b = false;
            }
            else
            {
                popupForMarker.IsOpen = false;
                ContentPanel.IsHitTestVisible = true;
                ml.RemoveAt(0);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            popupForMarker.IsOpen = false;
            ContentPanel.IsHitTestVisible = true;
            ml.RemoveAt(0);
            popupForMessage4.IsOpen = true;
            ContentPanel.IsHitTestVisible = false;
        }

        private void CheckRating()
        {
            OkButton.IsEnabled = rate.Count >= 2 ? true : false;
        }

        private void Page1_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PivotPage1.xaml", UriKind.Relative));
        }

        private void Fb_Click(object sender, EventArgs e)
        {
            ShareStatusTask shareStatusTask = new ShareStatusTask();
            shareStatusTask.Status = "Check out http://www.rastarobosta.com - Piurai Pinik!";
            shareStatusTask.Show();
        }

        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }

        private void Rate_Click(object sender, EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            counter = 0;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationService.BackStack.Count() == 1)
            {
                NavigationService.RemoveBackEntry();
            }
            r.Clear();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            e.Cancel = true;

            popupForMessage3.IsOpen = true;
            ContentPanel.IsHitTestVisible = false;
        }

        private void OkButtonx_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Terminate();
        }

        private void CancelButtonx_Click(object sender, RoutedEventArgs e)
        {
            popupForMessage3.IsOpen = false;
            ContentPanel.IsHitTestVisible = true;
        }

        private void Privacy_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
        }

        private void ContentPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        }

        private async void ov_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                popupForMessage11.IsOpen = false;
                IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
                DateTime start = DateTime.Parse(appSettings["date"].ToString());

                DateTime end = DateTime.Now;

                if ((end.Date - start.Date).Days == 0)
                {
                    string value = (string)appSettings["userData"];

                    int j = int.Parse(value);

                    if (j > 0 && j <= 2)
                    {
                        j = j - 1;
                        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                        if (!settings.Contains("userData"))
                        {
                            settings.Add("userData", j.ToString());
                        }
                        else
                        {
                            settings["userData"] = j.ToString();
                        }
                        settings.Save();

                        DeviceID();
                        Class5 c5 = new Class5
                        {
                            uuid = hexString,
                            rate = "overtake"
                        };

                        string jsonData = JsonConvert.SerializeObject(c5);

                        WebClient webClient = new WebClient();
                        webClient.Headers["Content-type"] = "application/json";
                        webClient.Encoding = Encoding.UTF8;

                        Uri uri = new Uri("http://api.rastarobosta.com/marker", UriKind.Absolute);
                        webClient.UploadStringAsync(uri, "POST", jsonData);

                        AudioPlayer.Source = new Uri("/Assets/car.mp3", UriKind.RelativeOrAbsolute);
                        popupForMessage8.IsOpen = true;
                        await Task.Delay(TimeSpan.FromSeconds(2));
                        popupForMessage8.IsOpen = false;
                        popupForMessage9.IsOpen = true;
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        popupForMessage9.IsOpen = false;

                        NavigationService.Navigate(new Uri("/Page3.xaml", UriKind.Relative));
                    }
                    else
                    {
                        popupForMessage10.IsOpen = true;
                        await Task.Delay(TimeSpan.FromSeconds(3));
                        popupForMessage10.IsOpen = false;
                    }
                }
            }
            catch
            {
                showmessage();
            }
        }

        private async void showmessage()
        {
            popupForMessage10.IsOpen = true;
            await Task.Delay(TimeSpan.FromSeconds(3));
            popupForMessage10.IsOpen = false;
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            popupForMarker.IsOpen = false;
            ContentPanel.IsHitTestVisible = true;

            ml.RemoveAt(0);
        }

        private void btnX1_Click(object sender, RoutedEventArgs e)
        {
            if (popupForMessage4.IsOpen == true)
            {
                popupForMessage4.IsOpen = false;
                ContentPanel.IsHitTestVisible = true;
            }
        }

        private void help_yourself_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PivotPage2.xaml", UriKind.Relative));
        }

        private void made_possible_by_Click(object sender, EventArgs e)
        {
            WebBrowserTask browser = new WebBrowserTask();

            browser.Uri = new Uri("http://rastarobosta.com/made_possible_by.html");

            browser.Show();
        }
    }
}