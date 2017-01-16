using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LaunchServiceBD
{
    public partial class Page39 : PhoneApplicationPage
    {
        public Page39()
        {
            InitializeComponent();
            this.launchListGropus.ItemsSource = this.GetLaunchGroups();
            this.MenuListGropus.ItemsSource = this.GetMenuGroups();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            if (sidebarControl.IsOpen)
            {
                sidebarControl.IsOpen = false;
                e.Cancel = true;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            sidebarControl.IsOpen = false;
        }

        private static IEnumerable<MainMenu> GetMenuList()
        {
            List<MainMenu> menuList = new List<MainMenu>();

            //launchtList.Add(new MainMenu() { Name = "", Route = "", Fare = ""});

            menuList.Add(new MainMenu() { Name = "ম্যাপ", PageName = "Map39" });
            menuList.Add(new MainMenu() { Name = "যাতায়াতকারী লঞ্চ", PageName = "LaunchList" });
            menuList.Add(new MainMenu() { Name = "প্রধান মেনু", PageName = "MainPage" });
            return menuList;
        }

        private List<_Group<MainMenu>> GetMenuGroups()
        {
            IEnumerable<MainMenu> menuList = GetMenuList();
            return GetMenuItemGroups(menuList, c => c.Name);
        }

        private static List<_Group<T>> GetMenuItemGroups<T>(IEnumerable<T> itemList, Func<T, string> getKeyFunc)
        {
            IEnumerable<_Group<T>> groupList = from item in itemList
                                               group item by getKeyFunc(item) into g
                                               select new _Group<T>(g.Key, g);

            return groupList.ToList();
        }

        public class _Group<T> : List<T>
        {
            public _Group(string name, IEnumerable<T> items)
                : base(items)
            {
                this.Title = name;
            }

            public string Title { get; set; }
        }

        private void OnMenuTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                LongListSelector selector = sender as LongListSelector;
                MainMenu data = selector.SelectedItem as MainMenu;

                string go = "/" + data.PageName + ".xaml";

                NavigationService.Navigate(new Uri(go, UriKind.Relative));
            }
            catch (NullReferenceException) { }
        }

        private static IEnumerable<Launch> GetLaunchList()
        {
            List<Launch> launchtList = new List<Launch>();

            launchtList.Add(new Launch() { Name = "ঢাকা থেকে বেতাগী", Route = "ঢাকা থেকে গন্তব্যে ছাড়ার সময়", Fare = "বিকাল-৫টা\nবিকাল-৬টা" });
            launchtList.Add(new Launch() { Name = "বেতাগী থেকে সদরঘাট, ঢাকা", Route = "গন্তব্য থেকে ঢাকায় ফেরার সময়", Fare = "বিকাল-৩.৩০মিনিট\nসন্ধ্যা-৭টা" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি যুবরাজ–২", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি যুবরাজ-৪", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি সৈকত-৯", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি নুসরাত-২", Fare = "" });
            return launchtList;
        }

        private List<Group<Launch>> GetLaunchGroups()
        {
            IEnumerable<Launch> launchList = GetLaunchList();
            return GetItemGroups(launchList, c => c.Name);
        }

        private static List<Group<T>> GetItemGroups<T>(IEnumerable<T> itemList, Func<T, string> getKeyFunc)
        {
            IEnumerable<Group<T>> groupList = from item in itemList
                                              group item by getKeyFunc(item) into g
                                              select new Group<T>(g.Key, g);

            return groupList.ToList();
        }

        public class Group<T> : List<T>
        {
            public Group(string route, IEnumerable<T> items)
                : base(items)
            {
                this.Title = route;
            }

            public string Title { get; set; }
        }

        private void Click_Me(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void LLS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            // If selected item is null, do nothing
            if (launchListGropus.SelectedItem == null)
                return;

            // Navigate to the next page
            try
            {
                LongListSelector selector = sender as LongListSelector;
                Launch data = selector.SelectedItem as Launch;

                if (data.Route == "এম ভি যুবরাজ–২")
                {
                    string go = "/" + "Page65" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি যুবরাজ-৪")
                {
                    string go = "/" + "Page66" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি সৈকত-৯")
                {
                    string go = "/" + "Page91" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি নুসরাত-২")
                {
                    string go = "/" + "Page92" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
            }
            catch (NullReferenceException) { }

            // Reset selected item to null
            launchListGropus.SelectedItem = null;
        }
    }
}