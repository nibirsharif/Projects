using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;

namespace VehicleTracker
{
    public partial class ExtendedSplash : PhoneApplicationPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ExtendedSplash()
        {
            InitializeComponent();

            //Call MainPage from ExtendedSplashScreen after some delay
            ExtendeSplashScreen();
        }

        /// <summary>
        /// Extended Splash Screen
        /// </summary>
        async void ExtendeSplashScreen()
        {
            await Task.Delay(TimeSpan.FromSeconds(3)); // set your desired delay
            NavigationService.Navigate(new Uri("/SignIn.xaml", UriKind.Relative)); // call MainPage
        }
    }
}