﻿using System;
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
    public partial class Page56 : PhoneApplicationPage
    {
        public Page56()
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

            //এম. ভি সুন্দরবন ২
            launchtList.Add(new Launch() { Name = "রুট", Route = "ঢাকা - বরিশাল দপদপিয়া", Fare = "" });
            launchtList.Add(new Launch() { Name = "রুট", Route = "ঢাকা - নলছিটি ঝালকাঠি", Fare = "" });

            launchtList.Add(new Launch() { Name = "ছাড়ার স্থান ও সময়সূচী", Route = "ঢাকা-নলছিটি ঝালকাঠি", Fare = "ঢাকা থেকে সন্ধ্যা ৭.৩০ মিনিটে ঝালকাঠির উদ্দেশ্যে ছেড়ে যায় এবং ঝালকাঠি থেকে পরদিন সন্ধ্যা ৬.০০ মিনিটে ঢাকার উদ্দেশ্যে ছেড়ে আসে।" });
            launchtList.Add(new Launch() { Name = "যোগাযোগ ও অগ্রিম বুকিং", Route = "সদরঘাট লঞ্চ টার্মিনালে গিয়ে সরাসরি যোগাযোগ করা যায়।", Fare = "মোবাইল নম্বর:  ০১৭১১-৪৪১০২৮" });


            launchtList.Add(new Launch() { Name = "ঢাকা - বরিশাল দপদপিয়া", Route = "ভিআইপি - ২৫০০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - বরিশাল দপদপিয়া", Route = "ডাবল কেবিন - ১৬০০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - বরিশাল দপদপিয়া", Route = "সিঙ্গেল কেবিন - ৮৫০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - বরিশাল দপদপিয়া", Route = "৩য় শ্রেণী - ২০০ টাকা", Fare = "" });

            launchtList.Add(new Launch() { Name = "ঢাকা - নলছিটি ঝালকাঠি", Route = "ভিআইপি - ২৫০০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - নলছিটি ঝালকাঠি", Route = "ডাবল কেবিন - ১৬০০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - নলছিটি ঝালকাঠি", Route = "সিঙ্গেল কেবিন - ৮৫০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা - নলছিটি ঝালকাঠি", Route = "৩য় শ্রেণী - ২০০ টাকা", Fare = "" });

            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "বিভিন্ন ধরনের লাগেজ / ব্যাগেজ - ৩০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "স্টীল/কাঠের আলমারী - ১০০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "কাপড়ের গাইট - ৫০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "কাঠের/স্টীলের খাট সাকুল্যে - ১০০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "কাঠের /স্টীলের/বেতের টেবিল/চেয়ার - ২০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "ফ্রিজ (সকল আয়তনের) - ৫০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "টেলিভিশন (সকল ধরনের) - ২০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "মালামালের বিবরণ", Route = "হার্ডওয়্যার মালামাল/ অন্যান্য মালামাল - ৪০ টাকা", Fare = "" });
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