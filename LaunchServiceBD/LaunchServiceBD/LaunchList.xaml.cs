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
    public partial class LaunchList : PhoneApplicationPage
    {
        public LaunchList()
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
            //launchtList.Add(new Launch() { Name = "", Route = "", Fare = "" });
            launchtList.Add(new Launch() { Name = "এম ভি বাঙ্গালী", Route = "ঢাকা - বরিশাল রুটে চলাচলকারী সরকারি লঞ্চ সার্ভিস", Fare = "Page52" });
            launchtList.Add(new Launch() { Name = "রকেট সার্ভিস", Route = "ঢাকা থেকে দক্ষিণাঞ্চলের বিভিন্ন বিআইডব্লিউটিসি’র রকেট সার্ভিস রয়েছে।", Fare = "Page53" });          
            launchtList.Add(new Launch() { Name = "বে-ক্রুজার", Route = "ঢাকা টু বরিশাল", Fare = "Page54" });
            launchtList.Add(new Launch() { Name = "এম ভি সুরভী-৮", Route = "ঢাকা - বরিশাল রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page55" });
            launchtList.Add(new Launch() { Name = "এম ভি সুন্দরবন ২", Route = "ঢাকা - ঝালকাঠি রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page56" });
            launchtList.Add(new Launch() { Name = "এম ভি সুন্দরবন ৫", Route = "ঢাকা – পটুয়াখালী রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page57" });
            launchtList.Add(new Launch() { Name = "এম ভি পারাবাত-২", Route = "ঢাকা - বরিশাল রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page58" });
            launchtList.Add(new Launch() { Name = "এম ভি সুন্দরবন-৭", Route = "ঢাকা - বরিশাল রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page59" });
            launchtList.Add(new Launch() { Name = "এম ভি পারাবত-৭", Route = "ঢাকা - বরিশাল রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page60" });
            launchtList.Add(new Launch() { Name = "এম ভি পারাবত-৯", Route = "ঢাকা - বরিশাল রুটে চলাচলকারী বাস সার্ভিস", Fare = "Page61" });
            launchtList.Add(new Launch() { Name = "এম ভি পারাবত ১১", Route = "ঢাকা - বরিশাল রুটে চলাচলকারী বাস সার্ভিস", Fare = "Page62" });
            launchtList.Add(new Launch() { Name = "এম ভি কালাম খান-১", Route = "ঢাকা - বরিশাল রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page63" });
            launchtList.Add(new Launch() { Name = "এম ভি যুবরাজ-১", Route = "ঢাকা - ভাষানচর রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page64" });
            launchtList.Add(new Launch() { Name = "এম ভি যুবরাজ–২", Route = "ঢাকা - বরগুনা রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page65" });
            launchtList.Add(new Launch() { Name = "এম ভি যুবরাজ-৪", Route = "ঢাকা – বরগুনা রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page66" });
            launchtList.Add(new Launch() { Name = "এম ভি টিপু", Route = "ঢাকা – ঝালকাঠি রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page67" });
            launchtList.Add(new Launch() { Name = "এম ভি টিপু-০", Route = "ঢাকা - ঝালকাঠি রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page68" });
            launchtList.Add(new Launch() { Name = "এম ভি টিপু-১", Route = "ঢাকা - ভান্ডারিয়া রুটে চলাচলকারী বাস সার্ভিস", Fare = "Page69" });
            launchtList.Add(new Launch() { Name = "এম ভি টিপু-২", Route = "ঢাকা – দৌলতখান রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page70" });
            launchtList.Add(new Launch() { Name = "এম ভি টিপু-৪", Route = "ঢাকা - চরফ্যাশন রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page71" });
            launchtList.Add(new Launch() { Name = "এম ভি টিপু-৫", Route = "ঢাকা - হাতিয়া রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page72" });
            launchtList.Add(new Launch() { Name = "এম ভি টিপু-৬", Route = "ঢাকা - হুলারহাট রুটে চলাচলকারী লঞ্চ", Fare = "Page73" });
            launchtList.Add(new Launch() { Name = "এম ভি সৈকত-১", Route = "ঢাকা – পটুয়াখালী রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page74" });
            launchtList.Add(new Launch() { Name = "এম ভি কোকো-১", Route = "ঢাকা - পাতারহাট রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page75" });
            launchtList.Add(new Launch() { Name = "এম ভি পাতারহাট-৫", Route = "ঢাকা – লালমোহন রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page76" });
            launchtList.Add(new Launch() { Name = "এম ভি সালাউদ্দীন–১", Route = "ঢাকা - লালমোহন রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page77" });
            launchtList.Add(new Launch() { Name = "এম ভি পানামা", Route = "ঢাকা – হাতিয়া রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page78" });
            launchtList.Add(new Launch() { Name = "এম ভি সম্পদ", Route = "ঢাকা – ভোলা রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page79" });
            launchtList.Add(new Launch() { Name = "এম ভি বালিয়া", Route = "ঢাকা-ভোলা রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page80" });
            launchtList.Add(new Launch() { Name = "এম ভি গাজী-৪", Route = "ঢাকা – বোরহানউদ্দীন রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page81" });
            launchtList.Add(new Launch() { Name = "এম ভি বন্ধন", Route = "ঢাকা - বোরহানউদ্দিন রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page82" });
            launchtList.Add(new Launch() { Name = "এম ভি পূবালী", Route = "ঢাকা – গলাচিপা রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page83" });
            launchtList.Add(new Launch() { Name = "এম ভি মানিক–৮", Route = "ঢাকা - দুবলারচর রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page84" });
            launchtList.Add(new Launch() { Name = "এম ভি রাজহংস-১", Route = "রাজহংস-১ লঞ্চটি ঢাকা-ভাষানচর রুটে চলাচল করে।", Fare = "Page85" });
            launchtList.Add(new Launch() { Name = "এম ভি রফরফ", Route = "ঢাকা - চাঁদপুর - ইচলী রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page86" });
            launchtList.Add(new Launch() { Name = "এম ভি প্রিন্স সোহাগ", Route = "প্রিন্স সোহাগ লঞ্চটি ঢাকা পাতাবুনিয়া রুটে চলাচল করে।", Fare = "Page87" });
            launchtList.Add(new Launch() { Name = "এম ভি জাহিদ-৩", Route = "ঢাকা সদরঘাট থেকে কালাইয়া, কুয়াকাটা রুটে চলাচল করে।", Fare = "Page88" });
            launchtList.Add(new Launch() { Name = "এম ভি আঁচল", Route = "ঢাকা - ভান্ডারিয়া রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page89" });
            launchtList.Add(new Launch() { Name = "এম ভি স্বর্নদ্বীপ প্লাস", Route = "ঢাকা - কালাইয়া রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page90" });
            launchtList.Add(new Launch() { Name = "এম ভি সৈকত-৯", Route = "ঢাকা - আমতলী রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page91" });
            launchtList.Add(new Launch() { Name = "এম ভি নুসরাত-২", Route = "ঢাকা - বরগুনা রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page92" });
            launchtList.Add(new Launch() { Name = "এম ভি তরীকা-৭", Route = "ঢাকা - লেতড়া রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page93" });
            launchtList.Add(new Launch() { Name = "এম ভি তরীকা-২", Route = "ঢাকা - মাদারীপুর রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page94" });
            launchtList.Add(new Launch() { Name = "এম ভি দ্বীপরাজ ৪", Route = "ঢাকা – মাদারীপুর রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page95" });
            launchtList.Add(new Launch() { Name = "এম ভি পূবালী-০", Route = "ঢাকা - গলাচিপা (পটুয়াখালী) রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page96" });
            launchtList.Add(new Launch() { Name = "এম ভি সেলা", Route = "ঢাকা - খুলনা রুটে চলাচলকারী স্টীমার সার্ভিস", Fare = "Page97" });
            launchtList.Add(new Launch() { Name = "এম ভি ফরহান-১", Route = "এম. ভি ফরহান-১ ও ২ লঞ্চটি ঢাকা -চরফ্যাশন রুটে চলাচল করে", Fare = "Page98" });
            launchtList.Add(new Launch() { Name = "এম ভি গ্লোরী অব শ্রীনগর-২", Route = "ঢাকা – শ্রীনগর রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page99" });
            launchtList.Add(new Launch() { Name = "এম ভি সুরেশ্বর-১", Route = "ঢাকা - শরীয়তপুর রুটে চলাচলকারী লঞ্চ সার্ভিস", Fare = "Page100" });
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

        private void OnTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                LongListSelector selector = sender as LongListSelector;

                Launch data = selector.SelectedItem as Launch;

                string go = "/" + data.Fare + ".xaml";

                NavigationService.Navigate(new Uri(go, UriKind.Relative));
            }
            catch (NullReferenceException) { }
        }
    }
}