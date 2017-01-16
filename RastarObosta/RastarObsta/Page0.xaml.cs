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

namespace RastarObsta
{
    public partial class Page0 : PhoneApplicationPage
    {
        public Page0()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            System.IO.IsolatedStorage.IsolatedStorageSettings settings = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;
            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains("IsFirstTimeLaunched") && (string)settings["IsFirstTimeLaunched"] == "true")
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            else
            {
                DateTime start = DateTime.Now;

                settings["IsFirstTimeLaunched"] = "true";
                settings.Add("date", start.Date.ToString());
                settings.Add("userData", "2".ToString());
                settings.Save();
                await Task.Delay(TimeSpan.FromSeconds(3));
                NavigationService.Navigate(new Uri("/PivotPage1.xaml", UriKind.Relative));
            }
        }
    }
}