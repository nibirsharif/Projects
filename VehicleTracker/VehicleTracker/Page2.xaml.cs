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
    public partial class Page2 : PhoneApplicationPage
    {
        /// <summary>
        /// Mobile Service
        /// </summary>
        private MobileServiceCollection<VisitedPlace, VisitedPlace> items;
        private IMobileServiceTable<VisitedPlace> itemTable = App.MobileService.GetTable<VisitedPlace>();

        // Network Interface Key
        bool isNetwork = NetworkInterface.GetIsNetworkAvailable();

        /// <summary>
        /// Constructor
        /// </summary>
        public Page2()
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
                items = await itemTable.ToCollectionAsync();
                this.placeListGropus.ItemsSource = this.GetPlaceGroups();
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
        /// Get Visited Place List
        /// </summary>
        /// <returns></returns>
        private IEnumerable<NewVisitedPlace> GetPlaceList()
        {
            List<NewVisitedPlace> placeList = new List<NewVisitedPlace>();

            foreach (var item in items)
            {
                placeList.Add(new NewVisitedPlace() { StartLocation = item.StartLocation, EndLocation = item.EndLocation, Date = item.Date });
            }

            return placeList;
        }

        /// <summary>
        /// Get Visited Place Group
        /// </summary>
        /// <returns></returns>
        private List<Group<NewVisitedPlace>> GetPlaceGroups()
        {
            IEnumerable<NewVisitedPlace> placeList = GetPlaceList();
            return GetItemGroups(placeList, c => c.Date.Date.ToShortDateString());
        }

        /// <summary>
        /// Group By Item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList"></param>
        /// <param name="getKeyFunc"></param>
        /// <returns></returns>
        private static List<Group<T>> GetItemGroups<T>(IEnumerable<T> itemList, Func<T, string> getKeyFunc)
        {
            IEnumerable<Group<T>> groupList = from item in itemList
                                              group item by getKeyFunc(item) into g
                                              orderby g.Key
                                              select new Group<T>(g.Key, g);

            return groupList.ToList();
        }

        /// <summary>
        /// Retuen Group Item Title
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
        /// Refresh Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Refresh_Click(object sender, EventArgs e)
        {
            a.IsVisible = true;
            try
            {
                items = await itemTable.ToCollectionAsync();
                this.placeListGropus.ItemsSource = this.GetPlaceGroups();
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
    }
}