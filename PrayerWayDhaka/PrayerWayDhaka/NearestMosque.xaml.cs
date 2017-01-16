using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Windows.Devices.Geolocation;
using PrayerWayDhaka.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Xml.Linq;

namespace PrayerWayDhaka
{
    public partial class NearestMosque : PhoneApplicationPage
    {
        XDocument LoadData = XDocument.Load("Data.xml");

        // Constructor
        public NearestMosque()
        {
            InitializeComponent();
            //Settings = IsolatedStorageSettings.ApplicationSettings;
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (MyCoordinates.Count != 0)
            {
                MyCoordinates.Clear();
            }

            LoadFromXml();

            if (_isNewInstance)
            {
                _isNewInstance = false;

                //LoadSettings();
                if (_isLocationAllowed)
                {
                    LocationPanel.Visibility = Visibility.Collapsed;
                    BuildApplicationBar();
                    GetCurrentCoordinate();
                }
            }
        }

        private void LoadFromXml()
        {
            var mosqueList = from mosque in LoadData.Descendants("Data")
                          select new Data()
                          {
                              Latitude = mosque.Attribute("Latitude").Value,
                              Longitude = mosque.Attribute("Longitude").Value
                          };

            foreach (var mosque in mosqueList)
            {
                try
                {
                    double latitude = Convert.ToDouble(mosque.Latitude);
                    double longitude = Convert.ToDouble(mosque.Longitude);

                    GeoCoordinate coordinate = new GeoCoordinate(latitude, longitude);
                    MyCoordinates.Add(coordinate);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error!\nLatitude: " + mosque.Latitude + "\nLongitude: " + mosque.Longitude);
                }
            }
        }

        private double disSqr(double a, double b)
        {
            return a * a + b * b;
        }

        private int IndexOfNearestPointInMap()
        {
            double dis = 1.0e20, tmp;
            int loc = 0;

            for (int i = 0; i < MyCoordinates.Count; i++)
            {
                    tmp = disSqr(MyCoordinates[i].Latitude - MyCoordinate.Latitude, MyCoordinates[i].Longitude - MyCoordinate.Longitude);
                        dis = Math.Min(dis, tmp);

                        if (tmp == dis)
                            loc = i;
            }
            return loc;
        }

        /// <summary>
        /// Event handler for location usage permission at startup.
        /// </summary>
        private void LocationUsage_Click(object sender, EventArgs e)
        {
            LocationPanel.Visibility = Visibility.Collapsed;
            BuildApplicationBar();
            if (sender == AllowButton)
            {
                _isLocationAllowed = true;
                //SaveSettings();
                GetCurrentCoordinate();
            }
            else
            {
                //NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                NavigationService.GoBack();
            }
        }

        /// <summary>
        /// We must satisfy Maps API's Terms and Conditions by specifying
        /// the required Application ID and Authentication Token.
        /// See http://msdn.microsoft.com/en-US/library/windowsphone/develop/jj207033(v=vs.105).aspx#BKMK_appidandtoken
        /// </summary>
        private void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            #if DEBUG
            #warning Please obtain a valid application ID and authentication token.
            #else
            #error You must specify a valid application ID and authentication token.
            #endif

            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = "AK6OqSOGLP9aAsUtEdXq";
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = "Bc2sDtaN3FbyHNX6p_hDkA";
        }

        /// <summary>
        /// Method for hiding directions panel on main page.
        /// </summary>
        private void HideDirections()
        {
            _isDirectionsShown = false;
            AppBarDirectionsMenuItem.Text = AppResources.DirectionsOnMenuItemText;
            DirectionsTitleRowDefinition.Height = new GridLength(0);
            DirectionsRowDefinition.Height = new GridLength(0);
            HeadingSlider.Visibility = Visibility.Visible;
            PitchSlider.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Event handler for clicking "route" app bar button.
        /// </summary>
        private void NearestMosque_Click(object sender, EventArgs e)
        {
            HideDirections();

            if (!_isLocationAllowed)
            {
                MessageBoxResult result = MessageBox.Show(AppResources.NoCurrentLocationMessageBoxText + " " + AppResources.LocationUsageQueryText,
                                                          AppResources.ApplicationTitle,
                                                          MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    _isLocationAllowed = true;
                    //SaveSettings();
                    GetCurrentCoordinate();
                }
            }
            else if (MyCoordinate == null)
            {
                MessageBox.Show(AppResources.NoCurrentLocationMessageBoxText, AppResources.ApplicationTitle, MessageBoxButton.OK);
            }
            else
            {
                _isRouteSearch = true;
                int nearestIndex = IndexOfNearestPointInMap();
                ShowRoute(MyCoordinates[nearestIndex].Latitude, MyCoordinates[nearestIndex].Longitude);                
                ButtonShowRouteOff.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Event handler for clicking "locate me" app bar button.
        /// </summary>
        private void LocateMe_Click(object sender, EventArgs e)
        {
            if (_isLocationAllowed)
            {
                GetCurrentCoordinate();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(AppResources.LocationUsageQueryText,
                                                          AppResources.ApplicationTitle,
                                                          MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    _isLocationAllowed = true;
                    //SaveSettings();
                    GetCurrentCoordinate();
                }
            }
        }

        /// <summary>
        /// Event handler for clicking "download" app bar button.
        /// </summary>
        private void Download_Click(object sender, EventArgs e)
        {
            MapDownloaderTask mapDownloaderTask = new MapDownloaderTask();
            mapDownloaderTask.Show();
        }

        /// <summary>
        /// Event handler for search input text box key down.
        /// </summary>
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (SearchTextBox.Text.Length > 0)
                {
                    // New query - Clear the map of markers and routes
                    if (MyMapRoute != null)
                    {
                        MyMap.RemoveRoute(MyMapRoute);
                    }
                    MyCoordinates.Clear();
                    DrawMapMarkers();

                    HideDirections();
                    AppBarDirectionsMenuItem.IsEnabled = false;

                    SearchForTerm(SearchTextBox.Text);
                    this.Focus();
                }
            }
        }

        /// <summary>
        /// Method to initiate a geocode query for a search term.
        /// </summary>
        /// <param name="searchTerm">Search term for location or destination</param>
        private void SearchForTerm(String searchTerm)
        {
            ShowProgressIndicator(AppResources.SearchingProgressText);
            MyGeocodeQuery = new GeocodeQuery();
            MyGeocodeQuery.SearchTerm = searchTerm;

            MyGeocodeQuery.GeoCoordinate = MyCoordinate == null ? new GeoCoordinate(0, 0) : MyCoordinate;
            MyGeocodeQuery.QueryCompleted += GeocodeQuery_QueryCompleted;
            MyGeocodeQuery.QueryAsync();
        }

        /// <summary>
        /// Event handler for geocode query completed.
        /// </summary>
        /// <param name="e">Results of the geocode query - list of locations</param>
        private void GeocodeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            HideProgressIndicator();
            if (e.Error == null)
            {
                if (e.Result.Count > 0)
                {
                    if (_isRouteSearch)
                    {
                        MyCoordinates.Add(e.Result[0].GeoCoordinate);

                        List<GeoCoordinate> routeCoordinates = new List<GeoCoordinate>();
                        routeCoordinates.Add(MyCoordinate);
                        routeCoordinates.Add(e.Result[0].GeoCoordinate);
                        CalculateRoute(routeCoordinates);
                    }
                    else
                    {
                        for (int i = 0; i < e.Result.Count; i++)
                        {
                            MyCoordinates.Add(e.Result[i].GeoCoordinate);
                        }
                        MyMap.SetView(e.Result[0].GeoCoordinate, 14, MapAnimationKind.Parabolic);
                    }
                }
                else
                {
                    MessageBox.Show(AppResources.NoMatchFoundMessageBoxText, AppResources.ApplicationTitle, MessageBoxButton.OK);
                }
                MyGeocodeQuery.Dispose();
            }
            DrawMapMarkers();
        }



        /// <summary>
        /// Event handler for search input text box losing focus.
        /// </summary>
        private void SearchTextBox_LostFocus(object sender, EventArgs e)
        {
            SearchTextBox.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Event handler for clicking directions on/off menu item.
        /// </summary>
        private void Directions_Click(object sender, EventArgs e)
        {
            _isDirectionsShown = !_isDirectionsShown;
            if (_isDirectionsShown)
            {
                // Center map on the starting point (phone location) and zoom quite close
                MyMap.SetView(MyCoordinate, 14, MapAnimationKind.Parabolic);
                ShowDirections();

                // Draw markers for possible waypoints when directions are shown.
                // Start and end points are already drawn with different colors.
                if (_isDirectionsShown && MyRoute.LengthInMeters > 0)
                {
                    for (int i = 1; i < MyRoute.Legs[0].Maneuvers.Count - 1; i++)
                    {
                        DrawMapMarker(MyRoute.Legs[0].Maneuvers[i].StartGeoCoordinate, Colors.Purple, routeMapLayer);
                    }
                }
            }
            else
            {
                HideDirections();
                DrawMapMarkers();
            }
        }

        /// <summary>
        /// Event handler for pitch slider value change.
        /// </summary>
        private void PitchValueChanged(object sender, EventArgs e)
        {
            if (PitchSlider != null)
            {
                MyMap.Pitch = PitchSlider.Value;
            }
        }

        /// <summary>
        /// Event handler for heading slider value change.
        /// </summary>
        private void HeadingValueChanged(object sender, EventArgs e)
        {
            if (HeadingSlider != null)
            {
                double value = HeadingSlider.Value;
                if (value > 360) value -= 360;
                MyMap.Heading = value;
            }
        }

        /// <summary>
        /// Event handler for clicking travel mode buttons.
        /// </summary>
        private void TravelModeButton_Click(object sender, EventArgs e)
        {
            // Clear the map before before making the query
            if (MyMapRoute != null)
            {
                MyMap.RemoveRoute(MyMapRoute);
            }
            MyMap.Layers.Clear();

            if (sender == DriveButton)
            {
                _travelMode = TravelMode.Driving;
            }
            else if (sender == WalkButton)
            {
                _travelMode = TravelMode.Walking;
            }
            DriveButton.IsEnabled = !DriveButton.IsEnabled;
            WalkButton.IsEnabled = !WalkButton.IsEnabled;

            // Route from current location to first search result
            List<GeoCoordinate> routeCoordinates = new List<GeoCoordinate>();
            routeCoordinates.Add(MyCoordinate);
            routeCoordinates.Add(MyRouteCoordinates[0]);
            CalculateRoute(routeCoordinates);
        }

        /// <summary>
        /// Event handler for selecting a maneuver in directions list.
        /// Centers the map on the selected maneuver.
        /// </summary>
        private void RouteManeuverSelected(object sender, EventArgs e)
        {
            object selectedObject = RouteLLS.SelectedItem;
            int selectedIndex = RouteLLS.ItemsSource.IndexOf(selectedObject);
            MyMap.SetView(MyRoute.Legs[0].Maneuvers[selectedIndex].StartGeoCoordinate, 14, MapAnimationKind.Parabolic);
        }

        /// <summary>
        /// Method for showing directions panel on main page.
        /// </summary>
        private void ShowDirections()
        {
            _isDirectionsShown = true;
            AppBarDirectionsMenuItem.Text = AppResources.DirectionsOffMenuItemText;
            DirectionsTitleRowDefinition.Height = GridLength.Auto;
            DirectionsRowDefinition.Height = new GridLength(2, GridUnitType.Star);
            HeadingSlider.Visibility = Visibility.Collapsed;
            PitchSlider.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Event handler for clicking a map marker. 
        /// Initiates reverse geocode query.
        /// </summary>
        private void Marker_Click(object sender, EventArgs e)
        {
            Polygon p = (Polygon)sender;

            string[] s = (string[])p.Tag;
            
            if (s[0].Equals("#FFFF0000") || s[0].Equals("#FF800080"))
            {
                isTappedPinIsCurrentPosition = true;

                GeoCoordinate geoCoordinate = new GeoCoordinate(Convert.ToDouble(s[1]), Convert.ToDouble(s[2]));

                //GeoCoordinate geoCoordinate = (GeoCoordinate)p.Tag;
                if (MyReverseGeocodeQuery == null || !MyReverseGeocodeQuery.IsBusy)
                {
                    MyReverseGeocodeQuery = new ReverseGeocodeQuery();
                    MyReverseGeocodeQuery.GeoCoordinate = new GeoCoordinate(geoCoordinate.Latitude, geoCoordinate.Longitude);
                    MyReverseGeocodeQuery.QueryCompleted += ReverseGeocodeQuery_QueryCompleted;
                    MyReverseGeocodeQuery.QueryAsync();
                }
            }
            else
            {
                var mosqueDetails = from mosque in LoadData.Descendants("Data")
                                 where mosque.Attribute("Latitude").Value == s[1]
                                       && mosque.Attribute("Longitude").Value == s[2]
                                 select new Data()
                                 {
                                     Name = mosque.Attribute("Name").Value,
                                     Address = mosque.Attribute("Address").Value
                                 };

                string address = "";
                string name = "";

                foreach (var mosque in mosqueDetails)
                {
                    name = mosque.Name;
                    address = mosque.Address;
                }

                String msgBoxText = "Name: " + name + "\n";

                string[] addresses = address.Split('#');

                foreach (string add in addresses)
                {
                    msgBoxText += add + "\n";
                }

                popupTextBlock.Text = msgBoxText;

                string latitude = s[1];
                string longitude = s[2];
                ButtonRoute.Tag = new string[] { latitude, longitude };

                popupForMarker.IsOpen = true;
            }
        }

        /// <summary>
        /// Event handler for reverse geocode query.
        /// </summary>
        /// <param name="e">Results of the reverse geocode query - list of locations</param>
        private void ReverseGeocodeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            if (e.Error == null)
            {
                if (e.Result.Count > 0)
                {
                    String msgBoxText = "";

                    MapAddress address = e.Result[0].Information.Address;

                    if (address.Street.Length > 0)
                    {
                        msgBoxText += "\n" + address.Street;
                        if (address.HouseNumber.Length > 0) msgBoxText += " " + address.HouseNumber;
                    }
                    if (address.PostalCode.Length > 0) msgBoxText += "\n" + address.PostalCode;
                    if (address.City.Length > 0) msgBoxText += "\n" + address.City;
                    if (address.Country.Length > 0) msgBoxText += "\n" + address.Country;

                    if (isTappedPinIsCurrentPosition)
                        ButtonRoute.Visibility = Visibility.Collapsed;

                    popupTextBlock.Text = msgBoxText;

                    string latitude = e.Result[0].GeoCoordinate.Latitude.ToString();
                    string longitude = e.Result[0].GeoCoordinate.Longitude.ToString();
                    ButtonRoute.Tag = new string[] { latitude, longitude };

                    popupForMarker.IsOpen = true;
                }
                else
                {
                    MessageBox.Show(AppResources.NoInfoMessageBoxText, AppResources.ApplicationTitle, MessageBoxButton.OKCancel);
                }
                MyReverseGeocodeQuery.Dispose();
            }
        }

        private void PopUpButtonRoute_OnClick(object sender, RoutedEventArgs e)
        {
            popupForMarker.IsOpen = false;
            _isRouteSearch = true;

            var myParameters = (string[])((Button)sender).Tag;
            string latitude = myParameters[0];
            string longitude = myParameters[1];

            ShowRoute(Convert.ToDouble(latitude), Convert.ToDouble(longitude));
            ButtonShowRouteOff.Visibility = Visibility.Visible;
        }

        private void ShowRoute(double latitude, double longitude)
        {
            if (MyMapRoute != null)
            {
                MyMap.RemoveRoute(MyMapRoute);
            }
            MyRouteCoordinates.Clear();

            HideDirections();
            AppBarDirectionsMenuItem.IsEnabled = false;

            MyGeocodeQuery = new GeocodeQuery();

            MyGeocodeQuery.GeoCoordinate = MyCoordinate == null ? new GeoCoordinate(0, 0) : MyCoordinate;

            if (_isRouteSearch) // Query is made to locate the destination of a route
            {
                destinationGeocoordinate = new GeoCoordinate(latitude, longitude);
               
                MyRouteCoordinates.Add(destinationGeocoordinate);

                List<GeoCoordinate> routeCoordinates = new List<GeoCoordinate>();
                routeCoordinates.Add(MyCoordinate);
                routeCoordinates.Add(destinationGeocoordinate);

                CalculateRoute(routeCoordinates);
            }
            AppBarDirectionsMenuItem.IsEnabled = true;
            MyGeocodeQuery.Dispose();
        }

        /// <summary>
        /// Method to initiate a route query.
        /// </summary>
        /// <param name="route">List of geocoordinates representing the route</param>
        private void CalculateRoute(List<GeoCoordinate> route)
        {
            ShowProgressIndicator(AppResources.CalculatingRouteProgressText);
            MyRouteQuery = new RouteQuery();
            MyRouteQuery.TravelMode = _travelMode;
            MyRouteQuery.Waypoints = route;
            MyRouteQuery.QueryCompleted += RouteQuery_QueryCompleted;
            MyRouteQuery.QueryAsync();
        }

        /// <summary>
        /// Event handler for route query completed.
        /// </summary>
        /// <param name="e">Results of the geocode query - the route</param>
        private void RouteQuery_QueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {
            HideProgressIndicator();
            if (e.Error == null)
            {
                MyRoute = e.Result;
                MyMapRoute = new MapRoute(MyRoute);
                MyMap.AddRoute(MyMapRoute);

                // Update route information and directions
                DestinationText.Text = SearchTextBox.Text;
                double distanceInKm = (double)MyRoute.LengthInMeters / 1000;
                DestinationDetailsText.Text = distanceInKm.ToString("0.0") + " km, "
                                              + MyRoute.EstimatedDuration.Hours + " hrs "
                                              + MyRoute.EstimatedDuration.Minutes + " mins.";

                List<string> routeInstructions = new List<string>();
                foreach (RouteLeg leg in MyRoute.Legs)
                {
                    for (int i = 0; i < leg.Maneuvers.Count; i++)
                    {
                        RouteManeuver maneuver = leg.Maneuvers[i];
                        string instructionText = maneuver.InstructionText;
                        distanceInKm = 0;

                        if (i > 0)
                        {
                            distanceInKm = (double)leg.Maneuvers[i - 1].LengthInMeters / 1000;
                            instructionText += " (" + distanceInKm.ToString("0.0") + " km)";
                        }
                        routeInstructions.Add(instructionText);
                    }
                }
                RouteLLS.ItemsSource = routeInstructions;

                AppBarDirectionsMenuItem.IsEnabled = true;

                if (_isDirectionsShown)
                {
                    MyMap.SetView(MyCoordinate, 14, MapAnimationKind.Parabolic);
                }
                else
                {
                    MyMap.SetView(MyRoute.Legs[0].BoundingBox, MapAnimationKind.Parabolic);
                }
                MyRouteQuery.Dispose();
            }

            MyMap.Layers.Clear();

            routeMapLayer = new MapLayer();

            for (int i = 0; i < MyRouteCoordinates.Count; i++)
            {
                DrawMapMarker(MyRouteCoordinates[i], Colors.Blue, routeMapLayer);
            }

            if (_isDirectionsShown && MyRoute.LengthInMeters > 0)
            {
                for (int i = 1; i < MyRoute.Legs[0].Maneuvers.Count - 1; i++)
                {
                    DrawMapMarker(MyRoute.Legs[0].Maneuvers[i].StartGeoCoordinate, Colors.Purple, routeMapLayer);
                }
            }

            // Draw marker for current position
            if (MyCoordinate != null)
            {
                DrawMapMarker(MyCoordinate, Colors.Red, routeMapLayer);
            }
            MyMap.Layers.Add(routeMapLayer);
        }


        private void ButtonShowRouteOff_OnClick(object sender, RoutedEventArgs e)
        {
            _isRouteSearch = false;

            ButtonShowRouteOff.Visibility = Visibility.Collapsed;
            AppBarDirectionsMenuItem.IsEnabled = false;
            
            if (MyMapRoute != null)
            {
                MyMap.RemoveRoute(MyMapRoute);
            }
            if (_isDirectionsShown)
            {
                _isDirectionsShown = false;
                HideDirections();
            }    
            DrawMapMarkers();

            MyMap.SetView(MyCoordinate, 14, MapAnimationKind.Parabolic);
        }

        private void PopupButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            if (isTappedPinIsCurrentPosition)
            {
                isTappedPinIsCurrentPosition = false;
                ButtonRoute.Visibility = Visibility.Visible;
            }
            popupForMarker.IsOpen = false;
        }

        /// <summary>
        /// Method to get current coordinate asynchronously so that the UI thread is not blocked. Updates MyCoordinate.
        /// Using Location API requires ID_CAP_LOCATION capability to be included in the Application manifest file.
        /// </summary>
        public void GetCurrentCoordinate()
        {
            ShowProgressIndicator("detecting location...");

            try
            {
                geolocator = new Geolocator();

                if (geolocator.LocationStatus == PositionStatus.Disabled)
                    throw new System.Exception();

                geolocator.DesiredAccuracy = PositionAccuracy.High;
                geolocator.MovementThreshold = 10;
                geolocator.PositionChanged += geolocator_PositionChanged;
            }
            catch (Exception)
            {
                if (geolocator.LocationStatus == PositionStatus.Disabled)
                {
                    MessageBox.Show(AppResources.LocationDisabledMessageBoxText, AppResources.ApplicationTitle, MessageBoxButton.OK);
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
            }
        }

        //This is the handler for PositionChanged event. It will tell to the user his position
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
                        MyMap.SetView(MyCoordinate, 14, MapAnimationKind.Parabolic);
                        isFirstTimeMapLoading = false;
                    }
                    else
                    {
                        if (!_isRouteSearch)
                        {
                            mapLayer.RemoveAt(mapLayer.Count - 1);
                            DrawMapMarker(MyCoordinate, Colors.Red, mapLayer);
                            MyMap.SetView(MyCoordinate, MyMap.ZoomLevel, MapAnimationKind.Parabolic);
                        }
                        else
                        {
                            // Route from current location to first search result
                            List<GeoCoordinate> routeCoordinates = new List<GeoCoordinate>();
                            routeCoordinates.Add(MyCoordinate);
                            routeCoordinates.Add(destinationGeocoordinate);

                            // New query - Clear the map of markers and routes
                            if (MyMapRoute != null)
                            {
                                MyMap.RemoveRoute(MyMapRoute);
                            }

                            CalculateRoute(routeCoordinates);
                        }
                    }
                    HideProgressIndicator();
                });
            }
            catch (Exception)
            {
                // Couldn't get current location - location might be disabled in settings
                MessageBox.Show(AppResources.LocationDisabledMessageBoxText, AppResources.ApplicationTitle, MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Method to draw markers on top of the map. Old markers are removed.
        /// </summary>
        private void DrawMapMarkers()
        {
            MyMap.Layers.Clear();

            mapLayer = new MapLayer();

            for (int i = 0; i < MyCoordinates.Count; i++)
            {
                DrawMapMarker(MyCoordinates[i], Colors.Blue, mapLayer);
            }

            // Draw marker for current position
            if (MyCoordinate != null)
            {
                DrawMapMarker(MyCoordinate, Colors.Red, mapLayer);
            }

            // Draw markers for possible waypoints when directions are shown.
            // Start and end points are already drawn with different colors.
            if (_isDirectionsShown && MyRoute.LengthInMeters > 0)
            {
                for (int i = 1; i < MyRoute.Legs[0].Maneuvers.Count - 1; i++)
                {
                    DrawMapMarker(MyRoute.Legs[0].Maneuvers[i].StartGeoCoordinate, Colors.Purple, routeMapLayer);
                }
            }
            MyMap.Layers.Add(mapLayer);
        }


        /// <summary>
        /// Helper method to draw a single marker on top of the map.
        /// </summary>
        /// <param name="coordinate">GeoCoordinate of the marker</param>
        /// <param name="color">Color of the marker</param>
        /// <param name="mapLayer">Map layer to add the marker</param>
        private void DrawMapMarker(GeoCoordinate coordinate, Color color, MapLayer layer)
        {

            // Create a map marker
            Polygon polygon = new Polygon();

            polygon.Points.Add(new Point(0, 0));
            polygon.Points.Add(new Point(0, 50));
            polygon.Points.Add(new Point(25, 0));
            polygon.Fill = new SolidColorBrush(color);

            string[] s = { color.ToString(), coordinate.Latitude.ToString(), coordinate.Longitude.ToString() };
            polygon.Tag = s;

            polygon.MouseLeftButtonUp += new MouseButtonEventHandler(Marker_Click);

            // Create a MapOverlay and add marker.
            MapOverlay overlay = new MapOverlay();
            overlay.Content = polygon;
            overlay.GeoCoordinate = new GeoCoordinate(coordinate.Latitude, coordinate.Longitude);
            overlay.PositionOrigin = new Point(0.0, 1.0);
            layer.Add(overlay);
        }

        /// <summary>
        /// Helper method to build a localized ApplicationBar
        /// </summary>
        private void BuildApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.    
            ApplicationBar = new ApplicationBar();

            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.BackgroundColor = Color.FromArgb(255, 70, 130, 180);
            ApplicationBar.IsVisible = true;
            ApplicationBar.IsMenuEnabled = true;

            ApplicationBarIconButton appBarNearestMosqueutton = new ApplicationBarIconButton(new Uri("/Assets/appbar.show.route.png", UriKind.Relative));
            appBarNearestMosqueutton.Text = AppResources.NearestMosqueMenuButtonText;
            appBarNearestMosqueutton.Click += new EventHandler(NearestMosque_Click);
            ApplicationBar.Buttons.Add(appBarNearestMosqueutton);

            ApplicationBarIconButton appBarLocateMeButton = new ApplicationBarIconButton(new Uri("/Assets/appbar.locate.me.png", UriKind.Relative));
            appBarLocateMeButton.Text = AppResources.LocateMeMenuButtonText;
            appBarLocateMeButton.Click += new EventHandler(LocateMe_Click);
            ApplicationBar.Buttons.Add(appBarLocateMeButton);

            ApplicationBarIconButton appBarDownloadButton = new ApplicationBarIconButton(new Uri("/Assets/appbar.download.png", UriKind.Relative));
            appBarDownloadButton.Text = AppResources.DownloadMenuButtonText;
            appBarDownloadButton.Click += new EventHandler(Download_Click);
            ApplicationBar.Buttons.Add(appBarDownloadButton);

            AppBarDirectionsMenuItem = new ApplicationBarMenuItem(AppResources.DirectionsOnMenuItemText);
            AppBarDirectionsMenuItem.Click += new EventHandler(Directions_Click);
            AppBarDirectionsMenuItem.IsEnabled = false;
            ApplicationBar.MenuItems.Add(AppBarDirectionsMenuItem);

            // Create menu items with localized strings from AppResources.
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarAboutUsMenuItemText);
            appBarMenuItem.Click += AboutUs;
            ApplicationBar.MenuItems.Add(appBarMenuItem);

            appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarHelpMenuItemText);
            appBarMenuItem.Click += Help;
            ApplicationBar.MenuItems.Add(appBarMenuItem);

            appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarDisclaimerMenuItemText);
            appBarMenuItem.Click += Disclaimer;
            ApplicationBar.MenuItems.Add(appBarMenuItem);

            appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarPrivacyPolicyMenuItemText);
            appBarMenuItem.Click += PrivacyPolicy;
            ApplicationBar.MenuItems.Add(appBarMenuItem);

            appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarReviewMenuItemText);
            appBarMenuItem.Click += Review;
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        private void Review(object sender, EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private void PrivacyPolicy(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PrivacyPolicy.xaml", UriKind.Relative));
        }

        private void Disclaimer(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Disclaimer.xaml", UriKind.Relative));
        }

        private void Help(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Help.xaml", UriKind.Relative));
        }

        private void AboutUs(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutUs.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Helper method to show progress indicator in system tray
        /// </summary>
        /// <param name="msg">Text shown in progress indicator</param>
        private void ShowProgressIndicator(String msg)
        {
            if (ProgressIndicator == null)
            {
                ProgressIndicator = new ProgressIndicator();
                ProgressIndicator.IsIndeterminate = true;
            }
            ProgressIndicator.Text = msg;
            ProgressIndicator.IsVisible = true;
            SystemTray.SetProgressIndicator(this, ProgressIndicator);
        }

        /// <summary>
        /// Helper method to hide progress indicator in system tray
        /// </summary>
        private void HideProgressIndicator()
        {
            ProgressIndicator.IsVisible = false;
            SystemTray.SetProgressIndicator(this, ProgressIndicator);
        }

        /// <summary>
        /// Helper method to load application settings
        /// </summary>
        public void LoadSettings()
        {
            if (Settings.Contains("isLocationAllowed"))
            {
                _isLocationAllowed = (bool)Settings["isLocationAllowed"];
            }
        }

        /// <summary>
        /// Helper method to save application settings
        /// </summary>
        public void SaveSettings()
        {
            if (Settings.Contains("isLocationAllowed"))
            {
                if ((bool)Settings["isLocationAllowed"] != _isLocationAllowed)
                {
                    // Store the new value
                    Settings["isLocationAllowed"] = _isLocationAllowed;
                }
            }
            else
            {
                Settings.Add("isLocationAllowed", _isLocationAllowed);
            }
        }


        private void ClearCurrentPosition()
        {
            mapLayer.RemoveAt(mapLayer.Count - 1);
            MyMap.SetView(MyCoordinate, 14, MapAnimationKind.Parabolic);

            MessageBox.Show(mapLayer[mapLayer.Count - 1].GeoCoordinate.Latitude.ToString());
        }

        // Application bar menu items
        private ApplicationBarMenuItem AppBarDirectionsMenuItem = null;

        // Progress indicator shown in system tray
        private ProgressIndicator ProgressIndicator = null;

        // My current location
        private GeoCoordinate MyCoordinate = null;

        // List of coordinates representing search hits / destination of route
        private List<GeoCoordinate> MyCoordinates = new List<GeoCoordinate>();

        // Geocode query
        private GeocodeQuery MyGeocodeQuery = null;

        // Route query
        private RouteQuery MyRouteQuery = null;

        // Reverse geocode query
        private ReverseGeocodeQuery MyReverseGeocodeQuery = null;

        // Route information
        private Route MyRoute = null;

        // Route overlay on map
        private MapRoute MyMapRoute = null;

        /// <summary>
        /// True when this object instance has been just created, otherwise false
        /// </summary>
        private bool _isNewInstance = true;

        /// <summary>
        /// True when access to user location is allowed, otherwise false
        /// </summary>
        private bool _isLocationAllowed = false;

        /// <summary>
        /// True when color mode has been temporarily set to light, otherwise false
        /// </summary>
        private bool _isTemporarilyLight = false;

        /// <summary>
        /// True when route is being searched, otherwise false
        /// </summary>
        private bool _isRouteSearch = false;

        /// <summary>
        /// True when directions are shown, otherwise false
        /// </summary>
        private bool _isDirectionsShown = false;

        /// <summary>
        /// Travel mode used when calculating route
        /// </summary>
        private TravelMode _travelMode = TravelMode.Driving;

        /// <summary>
        /// Accuracy of my current location in meters;
        /// </summary>
        private double _accuracy = 0.0;

        /// <summary>
        /// Used for saving location usage permission
        /// </summary>
        private IsolatedStorageSettings Settings;

        private bool isTappedPinIsCurrentPosition = false;

        public Geolocator geolocator = null;

        private bool isFirstTimeMapLoading = true;

        private MapLayer mapLayer = null;

        private bool isUpdatingCurrentPosition = false;

        private bool isRouteShowingOn = false;

        private List<GeoCoordinate> MyRouteCoordinates = new List<GeoCoordinate>();

        private MapLayer routeMapLayer = null;

        private GeoCoordinate destinationGeocoordinate = null;
    }
}