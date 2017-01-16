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
using Microsoft.Phone.Net.NetworkInformation;

namespace VehicleTracker
{
    public partial class Page3 : PhoneApplicationPage
    {
        /// <summary>
        /// Mobile Service
        /// </summary>
        private MobileServiceCollection<Item, Item> items;
        private IMobileServiceTable<Item> itemTable = App.MobileService.GetTable<Item>();

        // Network Interface Key
        bool isNetwork = NetworkInterface.GetIsNetworkAvailable();

        /// <summary>
        /// Constructor
        /// </summary>
        public Page3()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        /// <summary>
        /// Main Page Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            a.IsVisible = true;
            try
            {
                Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));

                items = await itemTable
                    .Where(item => (item.Lat <= (currentPosition.Coordinate.Point.Position.Latitude + 0.054) && item.Lat >= (currentPosition.Coordinate.Point.Position.Latitude - 0.054)
                                    && item.Long <= (currentPosition.Coordinate.Point.Position.Longitude + 0.054) && item.Long >= (currentPosition.Coordinate.Point.Position.Longitude - 0.054)
                                    && item.Message == "Traffic Jam"))
                    .ToCollectionAsync();
                this.trafficListGropus.ItemsSource = this.GetTrafficGroups();
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
            a.IsVisible = false;
        }

        /// <summary>
        /// Get Traffic List
        /// </summary>
        /// <returns></returns>
        private IEnumerable<NewItem> GetTrafficList()
        {
            List<NewItem> trafficList = new List<NewItem>();

            foreach (var item in items)
            {
                double count = items.Where(i => (i.Location == item.Location)).Count();
                trafficList.Add(new NewItem() { Message = item.Message, Lat = item.Lat, Long = item.Long, Location = item.Location, Density = item.Density, Count=count, Date = item.Date});
            }

            return trafficList;
        }

        /// <summary>
        /// Get Traffic Group
        /// </summary>
        /// <returns></returns>
        private List<Group<NewItem>> GetTrafficGroups()
        {
            IEnumerable<NewItem> trafficList = GetTrafficList();
            return GetItemGroups(trafficList, c => c.Location);
        }

        /// <summary>
        /// Get Item Group
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList"></param>
        /// <param name="getKeyFunc"></param>
        /// <returns></returns>
        private static List<Group<T>> GetItemGroups<T>(IEnumerable<T> itemList, Func<T, string> getKeyFunc)
        {
            IEnumerable<Group<T>> groupList = from item in itemList
                                              group item by getKeyFunc(item) into g
                                              select new Group<T>(g.Key, g);

            return groupList.ToList();
        }

        /// <summary>
        /// Get Group
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class Group<T> : List<T>
        {
            public Group(string route, IEnumerable<T> items)
                : base(items)
            {
                this.Title = route;
            }

            public string Title { get; set; }
        }

        /// <summary>
        /// Regresh Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Refresh_Click(object sender, EventArgs e)
        {
            a.IsVisible = true;
            try
            {
                Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));

                items = await itemTable
                    .Where(item => (item.Lat <= (currentPosition.Coordinate.Point.Position.Latitude + 0.054) && item.Lat >= (currentPosition.Coordinate.Point.Position.Latitude - 0.054)
                                    && item.Long <= (currentPosition.Coordinate.Point.Position.Longitude + 0.054) && item.Long >= (currentPosition.Coordinate.Point.Position.Longitude - 0.054)
                                    && item.Message == "Traffic Jam"))
                    .ToCollectionAsync();
                this.trafficListGropus.ItemsSource = this.GetTrafficGroups();
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
            a.IsVisible = false;
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

        /// <summary>
        /// List Item Selection Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItemsToday_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = trafficListGropus.SelectedItem as NewItem;
                double _lat = item.Lat;
                double _long = item.Long;
                string _message = item.Message;

                NavigationService.Navigate(new Uri("/Page6.xaml?_lat=" + _lat + "&_long=" + _long + "&_message=" + _message, UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Geolocator
        Geolocator geolocator = new Geolocator();
    }
}