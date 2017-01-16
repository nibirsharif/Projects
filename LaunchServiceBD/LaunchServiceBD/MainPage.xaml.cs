using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LaunchServiceBD.Resources;
using Telerik.Windows.Controls;
using Microsoft.Phone.Tasks;

namespace LaunchServiceBD
{
    public class TiltableControl : Grid
    {
    }

    public partial class MainPage : PhoneApplicationPage
    {
        public List<string> launchtList { get; set; }
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            TiltEffect.TiltableItems.Add(typeof(TiltableControl));
            this.MenuListGropus.ItemsSource = this.GetMenuGroups();
            this.Loaded += new RoutedEventHandler(_Loaded);

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
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

            // Check if ExtendedSplashscreen.xaml is on the backstack  and remove 
            if (NavigationService.BackStack.Count() == 1)
            {
                NavigationService.RemoveBackEntry();
            }
        }

        private static IEnumerable<MainMenu> GetMenuList()
        {
            List<MainMenu> menuList = new List<MainMenu>();

            //launchtList.Add(new MainMenu() { Name = "", Route = "", Fare = ""});

            menuList.Add(new MainMenu() { Name = "লঞ্চ তালিকা", PageName = "LaunchList" });
            menuList.Add(new MainMenu() { Name = "ম্যাপ", PageName = "Map0" });
            menuList.Add(new MainMenu() { Name = "আমাদের কথা", PageName = "AboutUs" });
            menuList.Add(new MainMenu() { Name = "কৃতজ্ঞতায়", PageName = "Thanksgiving" });
            menuList.Add(new MainMenu() { Name = "মূল্যায়ন করুন", PageName = "Review" });
            menuList.Add(new MainMenu() { Name = "বাহির", PageName = "Exit" });
            
            return menuList;
        }

        private List<Group<MainMenu>> GetMenuGroups()
        {
            IEnumerable<MainMenu> menuList = GetMenuList();
            return GetItemGroups(menuList, c => c.Name);
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
            public Group(string name, IEnumerable<T> items)
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

                if (data.PageName == "Exit")
                {
                    Application.Current.Terminate();
                }
                else if (data.PageName == "Review")
                {
                    MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
                    marketplaceReviewTask.Show();
                }
                else
                {
                    string go = "/" + data.PageName + ".xaml";

                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
            }
            catch (NullReferenceException) { }
        }

        private void _Loaded(object sender, RoutedEventArgs e)
        {
            launchtList = LoadLaunch();

            radAutoComplete.SuggestionsSource = launchtList;
            radListBox.ItemsSource = launchtList;
        }

        private List<string> LoadLaunch()
        {
            List<string> temptList = new List<string>();
            //temptList.Add("");
            temptList.Add("বাগেরহাট, খুলনা");
            temptList.Add("বানরীপাড়া, বরিশাল");
            temptList.Add("বকশিরহাট, চট্রগ্রাম");
            temptList.Add("চৌমোহনী, কুমিল্লা");
            temptList.Add("চন্দনপুর, সাতক্ষিরা");
            temptList.Add("চরদোয়ানী, বরগুনা");
            temptList.Add("চরবোরহান, পটুয়াখালি");
            temptList.Add("চরকাজল, পটুয়াখালি");
            temptList.Add("চরবিশ্বাস, পটুয়াখালি");
            temptList.Add("চরমন্তাজ, পটুয়াখালি");
            temptList.Add("চৌধুরীহাট, চট্রগ্রাম");
            temptList.Add("চরকলমী, ভোলা");
            temptList.Add("চাঁদপুর, চাঁদপুর");
            temptList.Add("চাঁন্দপুর, চাঁদপুর");
            temptList.Add("চরখালী, পটুয়াখালি");
            temptList.Add("চৌখালী, খুলনা");
            temptList.Add("চাঁদখালী, খুলনা");
            temptList.Add("চরভৈরবী, চাঁদপুর");
            temptList.Add("বাহেরচরবাজার, পটুয়াখালি");
            temptList.Add("আবুপুর, ফেনী");
            temptList.Add("বাবুগঞ্জ, বরিশাল");
            temptList.Add("বাঙ্গিরচর, বরিশাল");
            temptList.Add("বন্যাতলী, পটুয়াখালি");
            temptList.Add("বড়বাশদিয়া, পটুয়াখালি");
            temptList.Add("বাংলাবাজার, চট্রগ্রাম");
            temptList.Add("বাকুটিয়া, খুলনা");
            //temptList.Add("বাদশানগর");
            temptList.Add("বরিশাল, বরিশাল");
            temptList.Add("আমখোলা, পটুয়াখালি");
            temptList.Add("বাশবাড়িয়া, পিরোজপুর");
            temptList.Add("আওলিয়াপুর, মাদারীপুর");
            temptList.Add("আমুয়া, ঝালকাঠি");
            //temptList.Add("আনন্দবাজার");
            temptList.Add("আলিমাবাদ, বরিশাল");
            temptList.Add("আমতলী, বরগুনা");
            temptList.Add("আন্ডারচর, নোয়াখালী");
            temptList.Add("বগা, পটুয়াখালি");
            temptList.Add("বাগদা, যশোর");
            temptList.Add("বেতাগী, বরগুনা");
            temptList.Add("বামনা, বরগুনা");
            temptList.Add("বদরপুর, পটুয়াখালি");
            temptList.Add("বালিয়াতলী, বরগুনা");
            temptList.Add("বাগমারা, কুমিল্লা");
            temptList.Add("বোরহানউদ্দিন, ভোলা");
            temptList.Add("বিশ্বাসেরহাট, পটুয়াখালি");
            temptList.Add("বোয়ালিয়া, চাঁপাইনাওয়াবগঞ্জ");
            //temptList.Add("বন্দারহাট");
            //temptList.Add("বাশবাড়ি, মুন্সীগঞ্জ");
            temptList.Add("বাদলপাড়া, বরিশাল");
            temptList.Add("বাঞ্চারামপুর, মাদারীপুর");
            //temptList.Add("বোদারবাজার");

            return temptList;
        }

        private void radAutoComplete_Suggestion(object sender, SuggestionSelectedEventArgs e)
        {
            radListBox.BringIntoView(e.SelectedSuggestion);
            radListBox.SelectedItem = e.SelectedSuggestion;
        }

        private void OnTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                RadDataBoundListBox ra = sender as RadDataBoundListBox;
                string data = ra.SelectedItem as string;

                switch (data)
                {
                    case "বাগেরহাট, খুলনা":
                        data = "Page1";
                        break;
                    case "বানরীপাড়া, বরিশাল":
                        data = "Page2";
                        break;
                    case "বকশিরহাট, চট্রগ্রাম":
                        data = "Page3";
                        break;
                    case "চৌমোহনী, কুমিল্লা":
                        data = "Page4";
                        break;
                    case "চন্দনপুর, সাতক্ষিরা":
                        data = "Page5";
                        break;
                    case "চরদোয়ানী, বরগুনা":
                        data = "Page6";
                        break;
                    case "চরবোরহান, পটুয়াখালি":
                        data = "Page7";
                        break;
                    case "চরকাজল, পটুয়াখালি":
                        data = "Page8";
                        break;
                    case "চরবিশ্বাস, পটুয়াখালি":
                        data = "Page9";
                        break;
                    case "চরমন্তাজ, পটুয়াখালি":
                        data = "Page10";
                        break;
                    case "চৌধুরীহাট, চট্রগ্রাম":
                        data = "Page11";
                        break;
                    case "চরকলমী, ভোলা":
                        data = "Page12";
                        break;
                    case "চাঁদপুর, চাঁদপুর":
                        data = "Page13";
                        break;
                    case "চাঁন্দপুর, চাঁদপুর":
                        data = "Page14";
                        break;
                    case "চরখালী, পটুয়াখালি":
                        data = "Page15";
                        break;
                    case "চৌখালী, খুলনা":
                        data = "Page16";
                        break;
                    case "চাঁদখালী, খুলনা":
                        data = "Page17";
                        break;
                    case "চরভৈরবী, চাঁদপুর":
                        data = "Page18";
                        break;
                    case "বাহেরচরবাজার, পটুয়াখালি":
                        data = "Page19";
                        break;
                    case "আবুপুর, ফেনী":
                        data = "Page20";
                        break;
                    case "বাবুগঞ্জ, বরিশাল":
                        data = "Page21";
                        break;
                    case "বাঙ্গিরচর, বরিশাল": // launch list not defined
                        data = "Page22";
                        break;
                    case "বন্যাতলী, পটুয়াখালি":
                        data = "Page23";
                        break;
                    case "বড়বাশদিয়া, পটুয়াখালি": // launch list not defined
                        data = "Page24";
                        break;
                    case "বাংলাবাজার, চট্রগ্রাম": // launch list not defined
                        data = "Page25";
                        break;
                    case "বাকুটিয়া, খুলনা": // launch list not defined
                        data = "Page26";
                        break;
                    //case "বাদশানগর":
                    //    data = "Page27";
                    //    break;
                    case "বরিশাল, বরিশাল":
                        data = "Page28";
                        break;
                    case "আমখোলা, পটুয়াখালি":
                        data = "Page29";
                        break;
                    case "বাশবাড়িয়া, পিরোজপুর":
                        data = "Page30";
                        break;
                    case "আওলিয়াপুর, মাদারীপুর":
                        data = "Page31";
                        break;
                    case "আমুয়া, ঝালকাঠি":
                        data = "Page32";
                        break;
                    //case "আনন্দবাজার":
                    //    data = "Page33";
                    //    break;
                    case "আলিমাবাদ, বরিশাল":
                        data = "Page34";
                        break;
                    case "আমতলী, বরগুনা":
                        data = "Page35";
                        break;
                    case "আন্ডারচর, নোয়াখালী":
                        data = "Page36";
                        break;
                    case "বগা, পটুয়াখালি":
                        data = "Page37";
                        break;
                    case "বাগদা, যশোর":
                        data = "Page38";
                        break;
                    case "বেতাগী, বরগুনা":
                        data = "Page39";
                        break;
                    case "বামনা, বরগুনা":
                        data = "Page40";
                        break;
                    case "বদরপুর, পটুয়াখালি":
                        data = "Page41";
                        break;
                    case "বালিয়াতলী, বরগুনা":
                        data = "Page42";
                        break;
                    case "বাগমারা, কুমিল্লা":
                        data = "Page43";
                        break;
                    case "বোরহানউদ্দিন, ভোলা":
                        data = "Page44";
                        break;
                    case "বিশ্বাসেরহাট, পটুয়াখালি":
                        data = "Page45";
                        break;
                    case "বোয়ালিয়া, চাঁপাইনাওয়াবগঞ্জ":
                        data = "Page46";
                        break;
                    //case "বন্দারহাট":
                    //    data = "Page47";
                    //    break;
                    //case "বাশবাড়ি, মুন্সীগঞ্জ": // launch list not defined
                    //    data = "Page48";
                    //    break;
                    case "বাদলপাড়া, বরিশাল":
                        data = "Page49";
                        break;
                    case "বাঞ্চারামপুর, মাদারীপুর": // launch list not defined
                        data = "Page50";
                        break;
                    //case "বোদারবাজার":
                    //    data = "Page51";
                    //    break;
                    default:
                        break;
                }
                string go = "/" + data + ".xaml";
                if (go != "/.xaml")
                {
                    NavigationService.Navigate(new Uri(go, UriKind.Relative));
                }
            }
            catch (NullReferenceException) { }
        }

        //The foreground color of the text in WatermarkTB is set to Magenta when WatermarkTB
        //gets focus.
        private void WatermarkTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (radAutoComplete.Text == "খোঁজ করুন, ঢাকা থেকে")
            {
                radAutoComplete.Text = "";
            }
        }
        //The foreground color of the text in WatermarkTB is set to Blue when WatermarkTB
        //loses focus. Also, if SearchTB loses focus and no text is entered, the
        //text "Search" is displayed.
        private void WatermarkTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (radAutoComplete.Text == String.Empty)
            {
                radAutoComplete.Text = "খোঁজ করুন, ঢাকা থেকে";
            }
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}