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
    public partial class Page80 : PhoneApplicationPage
    {
        public Page80()
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

            //এম ভি বালিয়া
            launchtList.Add(new Launch() { Name = "রুট", Route = "ঢাকা  -ভোলা", Fare = "" });

            launchtList.Add(new Launch() { Name = "ছাড়ার স্থান ও সময়সূচী", Route = "ঢাকা-ভোলা", Fare = "সন্ধ্যা ০৭.০০/০৮.৩০ মিনিটে ঢাকার সদরঘাট থেকে লঞ্চটি ভোলার উদ্দেশ্যে ছেড়ে যায়। ভোর ০৪.০০টা থেকে ০৫.০০ টার মধ্যে  ভোলা পৌঁছে যায়। ভোলা থেকে সন্ধ্যা ০৭.০০/০৮.৩০ মিনিটে  সদরঘাটের উদ্দেশ্যে  ছেড়ে আসে এবং  সকাল ০৫.৩০ টা থেকে ০৬.০০ টার মধ্যে ঢাকা পৌঁছে।" });
            launchtList.Add(new Launch() { Name = "যোগাযোগ ও অগ্রিম বুকিং", Route = "যে কোন তথ্যের জন্য সদরঘাট লঞ্চ টার্মিনালে যোগাযোগ করা যায়।", Fare = "ফোন নম্বর: +৮৮০১৭১২-১৮২২৯৩" });


            launchtList.Add(new Launch() { Name = "ঢাকা - ভোলা", Route = "ডেক - ২৬৪/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - ভোলা", Route = "সিংগেল কেবিন - ১০৫৬/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - ভোলা", Route = "ডাবল কেবিন - ২১১২/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - ভোলা", Route = "ভিআইপি - ৩০০০/-", Fare = "" });

            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "বিভিন্ন ধরনের লাগেজ, ব্যাগ রাস্তা থেকে স্টিমার বা লঞ্চ পর্যন্ত (একজন শ্রমিক) - ৩০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "ষ্টীল বা কাঠের আলমারি (একাধিক শ্রমিকের ক্ষেত্রে) - ১০০/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "কাপড়ের গাইড (একাধিক শ্রমিক) - ৫০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "কাঠের বা ষ্টীলের খাট - ১০০/- টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "কাঠের, ষ্টিলের, বেতের চেয়ার, টেবিল - ২০/-টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "ফ্রিজ সকল আয়তনের - ৫০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "টেলিভিশন সকল ধরনের - ২০/- টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "হাডওয়ার/ অন্যান্য মালামাল/ কার্টুন/ ফ্যান/ ঝুড়ি - ৪০/-টাকা", Fare = "" });

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
    }
}