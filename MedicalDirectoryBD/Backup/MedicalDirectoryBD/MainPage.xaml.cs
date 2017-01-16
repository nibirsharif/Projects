using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace MedicalDirectoryBD
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnSpecialistDoctorsList(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SpecialistDoctorsList.xaml", UriKind.Relative));
        }

        private void btnHospitalsList(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/HospitalsList.xaml", UriKind.Relative));
        }

        private void btnAmbulanceService(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AmbulanceService.xaml", UriKind.Relative));
        }

        private void btnNearestHospitals(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NearestHospitals.xaml", UriKind.Relative));
        }

        private void btnAbout(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SpecialistDoctorsList/Cancer.xaml?goto=1", UriKind.Relative));
        }
    }
}