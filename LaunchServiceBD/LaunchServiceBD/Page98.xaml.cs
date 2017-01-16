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
    public partial class Page98 : PhoneApplicationPage
    {
        public Page98()
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

            //এম.ভি ফরহান-১

            launchtList.Add(new Launch() { Name = "রুট", Route = "ঢাকা - চরফ্যাশন ", Fare = "" });

            launchtList.Add(new Launch() { Name = "ছাড়ার স্থান ও সময়সূচী", Route = "সন্ধ্যা ০৬.৪৫ মিনিটে ঢাকার সদরঘাট থেকে লঞ্চটি চরফ্যাশনের উদ্দেশ্যে ছেড়ে যায়। সকাল ০৬.০০ টায় চরফ্যাশনে পৌঁছে যায়। চরফ্যাশন থেকে বিকাল ০৪.০০টায় ঢাকার সদরঘাটের উদ্দেশ্যে ছেড়ে আসে এবং সকাল ০৬.০০ টায়  ঢাকা পৌঁছে। ঢাকার সদরঘাট টার্মিনাল থেকে একদিন পর পর চরফ্যাশনের উদ্দেশ্য ছেড়ে যায়।", Fare = "" });

            launchtList.Add(new Launch() { Name = "যোগাযোগ ও অগ্রিম বুকিং", Route = "সদরঘাট লঞ্চ টার্মিনালে গিয়ে অথবা মোবাইলের মাধ্যমে যে কোন তথ্যর জন্য যোগাযোগ করা যায়।", Fare = " যোগাযোগের ফোন নম্বর: +৮৮-০১৭৩২-১৮৪১৩২" });

            launchtList.Add(new Launch() { Name = "ঢাকা - চরফ্যাশন", Route = "ডাবল কেবিন - ১৫০০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - চরফ্যাশন", Route = "সিঙ্গেল কেবিন - ৭৫০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - চরফ্যাশন", Route = "ডেক - ২০০ টাকা", Fare = "" });

            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "বিভিন্ন ধরনের লাগেজ, ব্যাগ রাস্তা থেকে স্টিমার বা লঞ্চ পর্যন্ত (একজন শ্রমিক) - ৩০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "ষ্টীল বা কাঠের আলমারি (একাধিক শ্রমিকের ক্ষেত্রে) - ৩০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "কাপড়ের গাইড (একাধিক শ্রমিক) - ১৫০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "কাঠের বা ষ্টীলের খাট - ৩০০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "কাঠের, ষ্টিলের, বেতের চেয়ার, টেবিল - ১৫০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "ফ্রিজ সকল আয়তনের - ২৫০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "টেলিভিশন সকল ধরনের - ১০০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "হাডওয়ার/ অন্যান্য মালামাল/ কার্টুন/ ফ্যান/ ঝুড়ি - ২০০ টাকা", Fare = "" });
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