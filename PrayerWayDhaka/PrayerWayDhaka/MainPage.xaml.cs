using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PrayerWayDhaka.Resources;
using Coding4Fun.Toolkit.Controls;
using System.Windows.Media;
using Microsoft.Phone.Tasks;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Threading;

namespace PrayerWayDhaka
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            BuildApplicationBar();

            PrayTime p = new PrayTime();
            double lo = 23.7;
            double la = 90.4;
            int y = 0, m = 0, d = 0, tz = 0;

            DateTime cc = DateTime.Now;
            y = cc.Year;
            m = cc.Month;
            d = cc.Day;
            tz = TimeZoneInfo.Local.GetUtcOffset(new DateTime(y, m, d)).Hours;
            String[] s;

            p.setCalcMethod(1);
            p.setAsrMethod(0);

            s = p.getDatePrayerTimes(y, m, d, lo, la, tz);

            int resultFajr = DateTime.Compare(DateTime.Now, DateTime.Parse(s[0]));
            int resultDhuhr = DateTime.Compare(DateTime.Now, DateTime.Parse(s[2]));
            int resultAsr = DateTime.Compare(DateTime.Now, DateTime.Parse(s[3]));
            int resultMaghrib = DateTime.Compare(DateTime.Now, DateTime.Parse(s[5]));
            int resultIsha = DateTime.Compare(DateTime.Now, DateTime.Parse(s[6]));

            if (resultFajr < 0)
            {
                nptTextBlock.Text = "next prayer: " + "Fajr";
                nextPrayTime.Text = s[0];
            }
            else if (resultDhuhr < 0)
            {
                nptTextBlock.Text = "next prayer: " + "Dhuhr";
                nextPrayTime.Text = s[2];
            }
            else if (resultAsr < 0)
            {
                nptTextBlock.Text = "next prayer: " + "Asr";
                nextPrayTime.Text = s[3];
            }
            else if (resultMaghrib < 0)
            {
                nptTextBlock.Text = "next prayer: " + "Maghrib";
                nextPrayTime.Text = s[5];
            }
            else if (resultIsha < 0)
            {
                nptTextBlock.Text = "next prayer: " + "Isha";
                nextPrayTime.Text = s[6];
            }
            else
            {
                nptTextBlock.Text = "next prayer: " + "Fajr";
                nextPrayTime.Text = s[0];
            }

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void btnPrayerTimeTable(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PrayerTimeTable.xaml", UriKind.Relative));
        }

        private void btnHowToPray(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/HowToPray.xaml", UriKind.Relative));
        }

        private void btnFindQibla(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/FindQibla.xaml", UriKind.Relative));
        }

        private void btnNearestMosque(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NearestMosque.xaml", UriKind.Relative));
        }

        //private void about_click(object sender, EventArgs e)
        //{
        //    var aboutprompt = new AboutPrompt { Body = new AboutControl() };
        //    aboutprompt.Show();
        //}

        //private void review_click(object sender, EventArgs e)
        //{
        //    MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
        //    marketplaceReviewTask.Show();
        //}

        /// <summary>
        /// Helper method to build a localized ApplicationBar
        /// </summary>
        private void BuildApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.    
            ApplicationBar = new ApplicationBar();

            ApplicationBar.Mode = ApplicationBarMode.Minimized;
            ApplicationBar.BackgroundColor = Color.FromArgb(240, 70, 130, 180);
            ApplicationBar.IsVisible = true;
            ApplicationBar.IsMenuEnabled = true;


            // Create menu items with localized strings from AppResources.
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarAboutUsMenuItemText);
            appBarMenuItem.Click += AboutUs;
            ApplicationBar.MenuItems.Add(appBarMenuItem);

            appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarHelpMenuItemText);
            appBarMenuItem.Click += Help;
            ApplicationBar.MenuItems.Add(appBarMenuItem);

            appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarDisclaimerMenuItemText);
            appBarMenuItem.Click += Disclaimer;
            ApplicationBar.MenuItems.Add(appBarMenuItem);

            appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarPrivacyPolicyMenuItemText);
            appBarMenuItem.Click += PrivacyPolicy;
            ApplicationBar.MenuItems.Add(appBarMenuItem);

            appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarReviewMenuItemText);
            appBarMenuItem.Click += Review;
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        private void Review(object sender, EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private void PrivacyPolicy(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PrivacyPolicy.xaml", UriKind.Relative));
        }

        private void Disclaimer(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Disclaimer.xaml", UriKind.Relative));
        }

        private void Help(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Help.xaml", UriKind.Relative));
        }

        private void AboutUs(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutUs.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Check if ExtendedSplashscreen.xaml is on the backstack  and remove 
            if (NavigationService.BackStack.Count() == 1)
            {
                NavigationService.RemoveBackEntry();
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
            base.OnBackKeyPress(e);
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