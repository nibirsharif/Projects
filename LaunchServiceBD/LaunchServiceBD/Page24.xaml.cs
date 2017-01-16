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
    public partial class Page24 : PhoneApplicationPage
    {
        public Page24()
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

            menuList.Add(new MainMenu() { Name = "ম্যাপ", PageName = "Map24" });
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

            launchtList.Add(new Launch() { Name = "ঢাকা থেকে বড়বাশদিয়া", Route = "ঢাকা থেকে গন্তব্যে ছাড়ার সময়", Fare = "সন্ধ্যা-৭টা\nরাত-৯.৩০মিনিট" });
            launchtList.Add(new Launch() { Name = "বড়বাশদিয়া থেকে সদরঘাট, ঢাকা", Route = "গন্তব্য থেকে ঢাকায় ফেরার সময়", Fare = "সকাল-১০টা\nদুপুর-১.২০মিনিট\nরাত-১২.২০মিনিট" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি সুন্দরবন ৫", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি সৈকত-১", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি পূবালী", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি প্রিন্স সোহাগ", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি জাহিদ-৩", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি স্বর্নদ্বীপ প্লাস", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি পূবালী-০", Fare = "" });
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

                if (data.Route == "এম ভি সুন্দরবন ৫")
                {
                    string go = "/" + "Page57" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি সৈকত-১")
                {
                    string go = "/" + "Page74" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি পূবালী")
                {
                    string go = "/" + "Page83" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি প্রিন্স সোহাগ")
                {
                    string go = "/" + "Page87" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি জাহিদ-৩")
                {
                    string go = "/" + "Page88" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি স্বর্নদ্বীপ প্লাস")
                {
                    string go = "/" + "Page90" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি পূবালী-০")
                {
                    string go = "/" + "Page96" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
            }
            catch (NullReferenceException) { }

            // Reset selected item to null
            launchListGropus.SelectedItem = null;
        }
    }
}