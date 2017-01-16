using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Net.NetworkInformation;

namespace VehicleTracker
{
    public partial class SignIn : PhoneApplicationPage
    {
        // Network Interface Key
        bool isNetwork = NetworkInterface.GetIsNetworkAvailable();

        /// <summary>
        /// Constructor
        /// </summary>
        public SignIn()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        /// <summary>
        /// Main Page Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isNetwork)
            {
                MessageBox.Show("Please check your internet connectivity.");
            }
        }

        /// <summary>
        /// Facebook Sing In Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSignin_Click(object sender, RoutedEventArgs e)
        {
            if (!isNetwork)
            {
                MessageBox.Show("Please check your internet connectivity.");
            }

            if (isNetwork)
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml?parameter=Facebook", UriKind.Relative));
            }
        }

        /// <summary>
        /// Twitter Sing In Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSignin1_Click(object sender, RoutedEventArgs e)
        {
            if (!isNetwork)
            {
                MessageBox.Show("Please check your internet connectivity.");
            }

            if (isNetwork)
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml?parameter=Twitter", UriKind.Relative));
            }
        }

        /// <summary>
        /// On Navigate Method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Check if ExtendedSplashscreen.xaml is on the backstack  and remove 
            if (NavigationService.BackStack.Count() == 1)
            {
                NavigationService.RemoveBackEntry();
            }
        }
    }
}