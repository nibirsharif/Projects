using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using Microsoft.Phone.Maps.Toolkit;
using System.Windows.Media;

namespace VehicleTracker
{
    public partial class Page6 : PhoneApplicationPage
    {
        // Parameter
        double parameterValue1;
        double parameterValue2;
        string parameterValue3;

        /// <summary>
        /// Constructor
        /// </summary>
        public Page6()
        {
            InitializeComponent();

            // Map Token
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = "ec74fb70-8b8b-4eea-8f9c-be6e84bf869b";
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = "2jkeQyHh7EgWG2iTvcVF5g";
        }

        /// <summary>
        /// On Navigate To Method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Parsing To Double
            parameterValue1 = double.Parse(NavigationContext.QueryString["_lat"]);
            parameterValue2 = double.Parse(NavigationContext.QueryString["_long"]);
            parameterValue3 = NavigationContext.QueryString["_message"];

            // Map Title
            t.Text = parameterValue3;

            Map MyMap = new Map(); // Map Object
            MyMap.CartographicMode = MapCartographicMode.Road;
            MyMap.Center = new GeoCoordinate(parameterValue1, parameterValue2);
            MyMap.ZoomLevel = 16;

            MapLayer mapLayer = new MapLayer();

            // Create a map marker
            Pushpin pushpin = new Pushpin();
            pushpin.Content = "Location";
            pushpin.Background = new SolidColorBrush(Colors.Red);

            // Create a MapOverlay and add marker.
            MapOverlay overlay = new MapOverlay();
            overlay.Content = pushpin;
            overlay.GeoCoordinate = new GeoCoordinate(MyMap.Center.Latitude, MyMap.Center.Longitude);
            overlay.PositionOrigin = new Point(0.0, 1.0);
            mapLayer.Add(overlay);
            MyMap.Layers.Add(mapLayer);

            // Adding Child Element to Map
            MapView.Children.Add(MyMap);
        }
    }
}