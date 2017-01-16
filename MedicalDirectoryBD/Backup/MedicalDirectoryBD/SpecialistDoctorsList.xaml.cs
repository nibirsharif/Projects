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
    public partial class SpecialistDoctorsList : PhoneApplicationPage
    {
        public SpecialistDoctorsList()
        {
            InitializeComponent();
        }

        private void btnENT(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SpecialistDoctorsList/ENT.xaml", UriKind.Relative));
        }

        private void btnCAM(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SpecialistDoctorsList/ChestAsthmaMedicine.xaml", UriKind.Relative));
        }

        private void btnCardiology(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SpecialistDoctorsList/Cardiology.xaml", UriKind.Relative));
        }

        private void btnCS(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SpecialistDoctorsList/CardiacSurgeons.xaml", UriKind.Relative));
        }

        private void btnES(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SpecialistDoctorsList/EyeSpecialist.xaml", UriKind.Relative));
        }

        private void btnLG(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SpecialistDoctorsList/LiverGastroenterology.xaml", UriKind.Relative));
        }

        private void btnCancer(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SpecialistDoctorsList/Cancer.xaml", UriKind.Relative));
        }

        private void btnOS(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SpecialistDoctorsList/Orthopedic.xaml", UriKind.Relative));
        }

        private void btnChild(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SpecialistDoctorsList/Child.xaml", UriKind.Relative));
        }

        private void btnDiabetes(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SpecialistDoctorsList/Diabetes.xaml", UriKind.Relative));
        }
    }
}