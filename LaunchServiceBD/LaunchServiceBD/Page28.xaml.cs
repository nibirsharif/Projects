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
    public partial class Page28 : PhoneApplicationPage
    {
        public Page28()
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

            menuList.Add(new MainMenu() { Name = "ম্যাপ", PageName = "Map28" });
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

            launchtList.Add(new Launch() { Name = "ঢাকা থেকে বরিশাল", Route = "ঢাকা থেকে গন্তব্যে ছাড়ার সময়", Fare = "ভোর-৬.১৫মিনিট\nবিকাল-৫টা\nবিকাল-৫.৩০মিনিট\nসন্ধ্যা-৬টা\nসন্ধ্যা-৬.১৫মিনিট\nরাত-৭.৩০মিনিট\nরাত-৯.৪৫মিনিট" });
            launchtList.Add(new Launch() { Name = "বরিশাল থেকে সদরঘাট, ঢাকা", Route = "গন্তব্য থেকে ঢাকায় ফেরার সময়", Fare = "সকাল-৯.১৫মিনিট\nদুপুর-১২টা\nবিকাল-৩টা\nসন্ধ্যা-৬.৩০মিনিট\nরাত-৭.৩০মিনিট\nরাত-৯.১৫মিনিট" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি বাঙ্গালী", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "রকেট সার্ভিস", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "বে-ক্রুজার", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি সুরভী-৮", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি পারাবাত-২", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি সুন্দরবন-৭", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি পারাবত-৭", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি পারাবত-৯", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি পারাবত ১১", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি কালাম খান-১", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি যুবরাজ-১", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি কোকো-১", Fare = "" });
            launchtList.Add(new Launch() { Name = "যাতায়াতকারী লঞ্চ", Route = "এম ভি রাজহংস-১", Fare = "" });
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

                if (data.Route == "এম ভি বাঙ্গালী")
                {
                    string go = "/" + "Page52" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "রকেট সার্ভিস")
                {
                    string go = "/" + "Page53" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "বে-ক্রুজার")
                {
                    string go = "/" + "Page54" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি সুরভী-৮")
                {
                    string go = "/" + "Page55" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি পারাবাত-২")
                {
                    string go = "/" + "Page58" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি সুন্দরবন-৭")
                {
                    string go = "/" + "Page59" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি পারাবত-৭")
                {
                    string go = "/" + "Page60" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি পারাবত-৯")
                {
                    string go = "/" + "Page61" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি পারাবত ১১")
                {
                    string go = "/" + "Page62" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি কালাম খান-১")
                {
                    string go = "/" + "Page63" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি যুবরাজ-১")
                {
                    string go = "/" + "Page64" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি কোকো-১")
                {
                    string go = "/" + "Page75" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
                else if (data.Route == "এম ভি রাজহংস-১")
                {
                    string go = "/" + "Page85" + ".xaml";
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
            }
            catch (NullReferenceException) { }

            // Reset selected item to null
            launchListGropus.SelectedItem = null;
        }
    }
}