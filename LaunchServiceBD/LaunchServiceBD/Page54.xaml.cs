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
    public partial class Page54 : PhoneApplicationPage
    {
        public Page54()
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

            //বে-ক্রুজার
            launchtList.Add(new Launch() { Name = "রুট", Route = "বরিশাল - ঢাকা", Fare = "" });

            launchtList.Add(new Launch() { Name = "ছাড়ার স্থান ও সময়সূচী", Route = "ঢাকা থেকে বরিশাল", Fare = "ঢাকা থেকে ছেড়ে যায় দুপুর ২:০০ টায়| পৌঁছার সময় ৬:০০ টায় (সন্ধা)| " });
            launchtList.Add(new Launch() { Name = "ছাড়ার স্থান ও সময়সূচী", Route = "বরিশাল থেকে  ঢাকা", Fare = "ঢাকা থেকে ছেড়ে যায়  সকাল ৭:০০ টায়| পৌঁছার সময়  ১১:০০ টায় (সকাল) |" });
            launchtList.Add(new Launch() { Name = "যোগাযোগ ও অগ্রিম বুকিং", Route = "সদরঘাট", Fare = "মোবাইল:  +৮৮-০১৭৫৫-৬১৯২২৬,  ০১৭৫৫-৬১৯২২৭" });
            launchtList.Add(new Launch() { Name = "যোগাযোগ ও অগ্রিম বুকিং", Route = "বরিশাল টার্মিনাল", Fare = "মোবাইল:  +৮৮-০১৭৫৫-৬১৯২৩০,  ০১৭৫৫-৬১৯২৩১" });


            launchtList.Add(new Launch() { Name = "বরিশাল - ঢাকা", Route = "কৃষ্ণচূড়া (প্রথম শ্রেণী) - ১২০০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "বরিশাল - ঢাকা", Route = "হাসনা হেনা (দ্বিতীয় শ্রেণী) - ১০০০ টাকা", Fare = "" });
            launchtList.Add(new Launch() { Name = "বরিশাল - ঢাকা", Route = "রজনীগন্ধা (তৃতীয় শ্রেণী) - ৮০০ টাকা", Fare = "" });

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