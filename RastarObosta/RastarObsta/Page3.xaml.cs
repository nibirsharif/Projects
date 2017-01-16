using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using Windows.Devices.Geolocation;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using RastarObsta.Resources;
using System.Diagnostics;
using GoogleAds;
using Windows.Storage;
using Windows.Storage.Streams;
using System.IO.IsolatedStorage;
using Newtonsoft.Json.Linq;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Net.NetworkInformation;

namespace RastarObsta
{
    public partial class Page3 : PhoneApplicationPage
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
        List<Class3> listB = new List<Class3>();
        List<Class2> rate = new List<Class2>();
        List<Class1> listV = new List<Class1>();
        private bool _version = false;
        private int temp = 0;
        private int minLevel = 13;
        private int maxLevel = 18;
        private int count = 0;

        bool isNetwork = NetworkInterface.GetIsNetworkAvailable();

        public Page3()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
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

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isNetwork)
            {
                MessageBox.Show("Please check your internet connectivity.");
                Application.Current.Terminate();
            }

            if (count == 0)
            {
                popupForMessage.IsOpen = true;
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

                try
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
                    webClient.DownloadStringAsync(new Uri("http://api.rastarobosta.com/marker/all"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                count = count + 1;
            }
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                string json = e.Result;
                if (!string.IsNullOrEmpty(json))
                {
                    JObject o = JObject.Parse(json);

                    IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
                    string value = (string)appSettings["version"];

                    if (value == o["version"].ToString())
                    {
                        _version = true;
                    }
                    else
                    {
                        _version = false;
                    }

                }
            }
            catch { }
        }

        public async void GetCurrentCoordinate()
        {
            try
            {
                geolocator = new Geolocator();

                if (geolocator.LocationStatus == PositionStatus.Disabled)
                    throw new System.Exception();

                geolocator.DesiredAccuracy = PositionAccuracy.High;
                geolocator.MovementThreshold = 10;
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(3));
                geolocator.PositionChanged += geolocator_PositionChanged;
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

        private void DrawMapMarkers()
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += webClient_DownloadStringCompleted1;
                webClient.DownloadStringAsync(new Uri("http://api.rastarobosta.com/marker/all"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string _fileName = "markers.json";

        async void webClient_DownloadStringCompleted1(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                try
                {
                    string json = e.Result;
                    if (!string.IsNullOrEmpty(json))
                    {
                        if (_version == false)
                        {
                            JObject o = JObject.Parse(json);

                            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                            if (!settings.Contains("version"))
                            {
                                settings.Add("version", o["version"].ToString());
                            }
                            else
                            {
                                settings["version"] = o["version"].ToString();
                            }
                            settings.Save();

                            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                            StorageFile textFile = await localFolder.CreateFileAsync(_fileName, CreationCollisionOption.ReplaceExisting);

                            using (IRandomAccessStream textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
                            {
                                using (DataWriter textWriter = new DataWriter(textStream))
                                {
                                    textWriter.WriteString(json);
                                    await textWriter.StoreAsync();
                                }
                            }
                            //for (int i = 0; i < json.Length; i++)
                            //{
                            //    listA.Add(new Class1() { ver = o["version"].ToString(), id = int.Parse(o["markers"][i]["id"].ToString()), lat = double.Parse(o["markers"][i]["latitude"].ToString()), lon = double.Parse(o["markers"][i]["longitude"].ToString()), ori = double.Parse(o["markers"][i]["orientation"].ToString()) });
                            //}

                            JArray items = (JArray)o["markers"];
                            int i = 0, length = items.Count;

                            while (i < length)
                            {
                                listA.Add(new Class1() { id = int.Parse(o["markers"][i]["id"].ToString()), lat = double.Parse(o["markers"][i]["latitude"].ToString()), lon = double.Parse(o["markers"][i]["longitude"].ToString()), ori = double.Parse(o["markers"][i]["orientation"].ToString()) });
                                Debug.WriteLine("s");
                                i++;
                            }
                        }
                        else if (_version == true)
                        {
                            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                            StorageFile textFile = await localFolder.GetFileAsync(_fileName);

                            using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
                            {
                                try
                                {
                                    using (DataReader textReader = new DataReader(textStream))
                                    {
                                        uint textLength = (uint)textStream.Size;
                                        await textReader.LoadAsync(textLength);
                                        string jsonContents = textReader.ReadString(textLength);

                                        if (!string.IsNullOrEmpty(jsonContents))
                                        {

                                            JObject o1 = JObject.Parse(jsonContents);
                                            //for (int i = 0; i < json.Length; i++)
                                            //{
                                            //    listA.Add(new Class1() { ver = o1["version"].ToString(), id = int.Parse(o1["markers"][i]["id"].ToString()), lat = double.Parse(o1["markers"][i]["latitude"].ToString()), lon = double.Parse(o1["markers"][i]["longitude"].ToString()), ori = double.Parse(o1["markers"][i]["orientation"].ToString()) });
                                            //}

                                            JArray items = (JArray)o1["markers"];
                                            int i = 0, length = items.Count;

                                            while (i < length)
                                            {
                                                listA.Add(new Class1() {  id = int.Parse(o1["markers"][i]["id"].ToString()), lat = double.Parse(o1["markers"][i]["latitude"].ToString()), lon = double.Parse(o1["markers"][i]["longitude"].ToString()), ori = double.Parse(o1["markers"][i]["orientation"].ToString()) });
                                                Debug.WriteLine("f");
                                                i++;
                                            }
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }
                catch { }

                try
                {
                    WebClient webClient1 = new WebClient();
                    webClient1.DownloadStringCompleted += webClient_DownloadStringCompleted2;
                    webClient1.DownloadStringAsync(new Uri("http://api.rastarobosta.com/marker/rating"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch { }
        }

        //async void Delay()
        //{
        //    await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(15));
        //}

        private void webClient_DownloadStringCompleted2(object sender, DownloadStringCompletedEventArgs e)
        {
            MyMap.Layers.Clear();

            mapLayer = new MapLayer();

            try
            {
                string json = e.Result;
                if (!string.IsNullOrEmpty(json))
                {
                    JObject o = JObject.Parse(json);

                    int i = 0;

                    while (i < listA.Count)
                    //for (int i = 0; i < listA.Count; i++)
                    {
                        Debug.WriteLine("c");
                        double latitude = listA[i].lat;
                        double longitude = listA[i].lon;

                        GeoCoordinate coordinate = new GeoCoordinate(latitude, longitude);
                        MyCoordinates.Add(coordinate);

                        var msgProperty = o.Property(listA[i].id.ToString());

                        if (msgProperty != null)
                        {
                            var rat = msgProperty.Value;

                            switch (rat.ToString())
                            {
                                case "1":
                                    DrawMapMarker(MyCoordinates[i], listA[i].id, listA[i].ori, Colors.Green, mapLayer);
                                    break;
                                case "2":
                                    DrawMapMarker(MyCoordinates[i], listA[i].id, listA[i].ori, Colors.LightGray, mapLayer);
                                    break;
                                case "3":
                                    DrawMapMarker(MyCoordinates[i], listA[i].id, listA[i].ori, Colors.Yellow, mapLayer);
                                    break;
                                case "4":
                                    DrawMapMarker(MyCoordinates[i], listA[i].id, listA[i].ori, Colors.Gray, mapLayer);
                                    break;
                                case "5":
                                    DrawMapMarker(MyCoordinates[i], listA[i].id, listA[i].ori, Colors.Orange, mapLayer);
                                    break;
                                case "6":
                                    DrawMapMarker(MyCoordinates[i], listA[i].id, listA[i].ori, Colors.Purple, mapLayer);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                        }
                        i++;
                    }
                }
            }
            catch { }

            try
            {
                if (MyCoordinate != null)
                {
                    DrawMapMarker(MyCoordinate, 0, 0, Colors.Red, mapLayer);
                }
            }
            catch { }
            popupForMessage.IsOpen = false;
            MyMap.Layers.Add(mapLayer);
        }

        private async void DrawMapMarker(GeoCoordinate coordinate, int num, double ori, Color color, MapLayer layer)
        {
            Debug.WriteLine("Loading... " + num);
            Rectangle rectangle = new Rectangle();

            BitmapImage arrImg;

            if (color == Colors.Red)
            {
                arrImg = new BitmapImage(new Uri("/Assets/Icons/location.png", UriKind.RelativeOrAbsolute));
                rectangle.Height = 20;
                rectangle.Width = 20;
            }
            else if (color == Colors.Green)
            {
                //await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1));
                arrImg = new BitmapImage(new Uri("/Assets/Arrows20/A.png", UriKind.RelativeOrAbsolute));
                rectangle.Height = 30;
                rectangle.Width = 30;
                RotateTransform myRotateTransform = new RotateTransform();
                myRotateTransform.Angle = ori;

                rectangle.RenderTransform = myRotateTransform;
            }
            else if (color == Colors.LightGray)
            {
                //await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1));
                arrImg = new BitmapImage(new Uri("/Assets/Arrows20/B.png", UriKind.RelativeOrAbsolute));
                rectangle.Height = 30;
                rectangle.Width = 30;
                RotateTransform myRotateTransform = new RotateTransform();
                myRotateTransform.Angle = ori;

                rectangle.RenderTransform = myRotateTransform;
            }
            else if (color == Colors.Yellow)
            {
                //await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1));
                arrImg = new BitmapImage(new Uri("/Assets/Arrows20/C.png", UriKind.RelativeOrAbsolute));
                rectangle.Height = 30;
                rectangle.Width = 30;
                RotateTransform myRotateTransform = new RotateTransform();
                myRotateTransform.Angle = ori;

                rectangle.RenderTransform = myRotateTransform;
            }
            else if (color == Colors.Gray)
            {
                //await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1));
                arrImg = new BitmapImage(new Uri("/Assets/Arrows20/D.png", UriKind.RelativeOrAbsolute));
                rectangle.Height = 30;
                rectangle.Width = 30;
                RotateTransform myRotateTransform = new RotateTransform();
                myRotateTransform.Angle = ori;

                rectangle.RenderTransform = myRotateTransform;
            }
            else if (color == Colors.Orange)
            {
                //await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1));
                arrImg = new BitmapImage(new Uri("/Assets/Arrows20/E.png", UriKind.RelativeOrAbsolute));
                rectangle.Height = 30;
                rectangle.Width = 30;
                RotateTransform myRotateTransform = new RotateTransform();
                myRotateTransform.Angle = ori;

                rectangle.RenderTransform = myRotateTransform;
            }
            else if (color == Colors.Purple)
            {
                //await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1));
                arrImg = new BitmapImage(new Uri("/Assets/Arrows20/F.png", UriKind.RelativeOrAbsolute));
                rectangle.Height = 30;
                rectangle.Width = 30;
                RotateTransform myRotateTransform = new RotateTransform();
                myRotateTransform.Angle = ori;

                rectangle.RenderTransform = myRotateTransform;
            }
            else
            {
                arrImg = null;
            }

            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = arrImg;
            rectangle.Fill = imgBrush;

            string[] s = { color.ToString(), num.ToString(), coordinate.Latitude.ToString(), coordinate.Longitude.ToString() };
            rectangle.Tag = s;

            MapOverlay overlay = new MapOverlay();
            overlay.Content = rectangle;
            overlay.GeoCoordinate = new GeoCoordinate(coordinate.Latitude, coordinate.Longitude);
            overlay.PositionOrigin = new Point(0.0, 1.0);

            layer.Add(overlay);
        }

        private void OnAdReceived(object sender, AdEventArgs e)
        {
            Debug.WriteLine("Received ad successfully");
        }

        private void OnFailedToReceiveAd(object sender, AdErrorEventArgs errorCode)
        {
            Debug.WriteLine("Failed to receive ad with error " + errorCode.ErrorCode);
        }

        private void OnApplicationBarStateChanged(object sender, ApplicationBarStateChangedEventArgs e)
        {
            //var appBar = sender as ApplicationBar;
            //if (appBar == null) return;

            //appBar.Opacity = e.IsMenuVisible ? 1 : .65;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popupForMessage1.IsOpen = true;
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            if (popupForMessage1.IsOpen == true)
            {
                popupForMessage1.IsOpen = false;
            }
        }

        private void Rate_Click(object sender, EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
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

        private void Privacy_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationService.BackStack.Count() == 1)
            {
                NavigationService.RemoveBackEntry();
            }
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
    }
}