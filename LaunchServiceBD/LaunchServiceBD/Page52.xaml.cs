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
    public partial class Page52 : PhoneApplicationPage
    {
        public Page52()
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

            //এম.ভি. বাঙ্গালী

            launchtList.Add(new Launch() { Name = "রুট", Route = "ঢাকা টু বরিশাল", Fare = "" });

            launchtList.Add(new Launch() { Name = "ছাড়ার স্থান ও সময়সূচী", Route = "ঢাকা থেকে মাত্র পাঁচ ঘণ্টায় জাহাজটি চাঁদপুর হয়ে বরিশাল পৌঁছাতে পারবে।", Fare = "" });
            launchtList.Add(new Launch() { Name = "যোগাযোগ ও অগ্রিম বুকিং", Route = "ঢাকা সদরঘাট লঞ্চ টার্মিনালে গিয়ে সরাসরি যোগাযোগ করা যায়।", Fare = "মোবাইল:+৮৮-০১৭১২-৫৮৬০৬৯" });


            launchtList.Add(new Launch() { Name = "ঢাকা - বরিশাল", Route = "সুলভ - ১৭০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - বরিশাল", Route = "প্রথম শ্রেণীর কেবিন - ১০৫০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - বরিশাল", Route = "ডাবল - ৭৪০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - বরিশাল", Route = "দ্বিতীয় শ্রেণী - ৬৩০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - বরিশাল", Route = "ভিআইপি কেবিন - ২০০০ টাকা", Fare = "" });
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

        //private void Click_Me(object sender, System.Windows.Input.GestureEventArgs e)
        //{
        //    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        //}
    }
}