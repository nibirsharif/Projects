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
    public partial class Page53 : PhoneApplicationPage
    {
        public Page53()
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

            //রকেট সার্ভিস

            launchtList.Add(new Launch() { Name = "রুট", Route = "ঢাকা – খুলনা" });
            launchtList.Add(new Launch() { Name = "রুট", Route = "ঢাকা – ঝালকাঠি" });
            launchtList.Add(new Launch() { Name = "রুট", Route = "ঢাকা – চাঁদপুর"});
            launchtList.Add(new Launch() { Name = "রুট", Route = "ঢাকা – বরিশাল" });

            launchtList.Add(new Launch() { Name = "ছাড়ার স্থান ও সময়সূচী", Route = "বাদামতলী ঘাট হতে ছেড়ে সদরঘাট হয়ে গন্তব্যের উদ্দেশ্যে যাত্রা শুরু করে।" });
            launchtList.Add(new Launch() { Name = "ছাড়ার স্থান ও সময়সূচী", Route = "বাদামতলী থেকে ছাড়ে বিকাল ৪:০০ টায় এবং সদরঘাট হতে ছাড়ে সন্ধ্যা ৭:৩০ টায়।" });

            launchtList.Add(new Launch() { Name = "যোগাযোগ ও অগ্রিম বুকিং", Route = "বিআইডব্লিউটিসি প্রধান অফিস", Fare = "ফোন: ৯৫৫৯৭৭৯, পিএবিএক্স: ৯৫৫৫০৩২-৩৩, ৯৫৫০৪৬৬" });
            launchtList.Add(new Launch() { Name = "যোগাযোগ ও অগ্রিম বুকিং", Route = "বাদামতলী ঘাট অফিস", Fare = "ফোন: ৭৩৯০৬৯১" });

            launchtList.Add(new Launch() { Name = "ভাড়া (ঢাকা থেকে)", Route = "ঢাকা থেকে দক্ষিণাঞ্চলের বিভিন্ন বিআইডব্লিউটিসি’র রকেট সার্ভিস রয়েছে।", Fare = "সপ্তাহের ৬ দিন এই সার্ভিস পরিচালনা করা হয়। শুক্রবার বন্ধ থাকে।" });

            launchtList.Add(new Launch() { Name = "ঢাকা – চাঁদপুর", Route = "১ম শ্রেণি - ৪২০/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা – চাঁদপুর", Route = "২য় শ্রেণি - ২৪০/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা – চাঁদপুর", Route = "৩য়/সুলভ শ্রেণি - ১০০/-", Fare = "" });

            launchtList.Add(new Launch() { Name = "ঢাকা – বরিশাল", Route = "১ম শ্রেণি - ১,০৫০/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা – বরিশাল", Route = "২য় শ্রেণি - ৬৩০/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা – বরিশাল", Route = "৩য়/সুলভ শ্রেণি - ১৭০/-", Fare = "" });

            launchtList.Add(new Launch() { Name = "ঢাকা – ঝালকাঠি", Route = "১ম শ্রেণি - ১,২৫০/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা – ঝালকাঠি", Route = "২য় শ্রেণি - ৭৬০/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা – ঝালকাঠি", Route = "৩য়/সুলভ শ্রেণি - ১৯০/-", Fare = "" });

            launchtList.Add(new Launch() { Name = "ঢাকা – চরখালী", Route = "১ম শ্রেণি - ১,৪৭০/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা – চরখালী", Route = "২য় শ্রেণি - ৮৮৫/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা – চরখালী", Route = "৩য়/সুলভ শ্রেণি - ২৪০/-", Fare = "" });

            launchtList.Add(new Launch() { Name = "ঢাকা – মংলাবন্দর", Route = "১ম শ্রেণি - ১,৯৬০/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা – মংলাবন্দর", Route = "২য় শ্রেণি - ১,১৯০/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা – মংলাবন্দর", Route = "৩য়/সুলভ শ্রেণি - ২৯৫/-", Fare = "" });

            launchtList.Add(new Launch() { Name = "ঢাকা – খুলনাঘাট", Route = "১ম শ্রেণি - ২,১০০/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা – খুলনাঘাট", Route = "২য় শ্রেণি - ১,২৬০/-", Fare = "" });
            launchtList.Add(new Launch() { Name = "ঢাকা – খুলনাঘাট", Route = "৩য়/সুলভ শ্রেণি - ৩১০/-", Fare = "" });
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