using Microsoft.Devices.Sensors;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MedicalDirectoryBD
{
    public partial class NearestHospitals : PhoneApplicationPage
    {
        GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
        public string longitude, latitude, x, y, z;
        static public double lo, la;

        // Constructor
        public NearestHospitals()
        {
            InitializeComponent();

            watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);

            map1.CredentialsProvider = new ApplicationIdCredentialsProvider("AuOMJmkOxSRMF-gBXGYeemwcYJCsqan8OUIJIX9n1RAdJSeSxS3o_QkZS-Y2NZUX");
            pushPin();
        }

        public void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (watcher.Position.Location.IsUnknown != false)
            {
                x = "System Can not detect you location";
            }
            else
            {
                longitude = watcher.Position.Location.Longitude.ToString();
                latitude = watcher.Position.Location.Latitude.ToString();

                MapLayer m_PushpinLayer = new MapLayer();
                map1.Children.Add(m_PushpinLayer);

                // Reasonable stab at center of Seattle metro area
                //map1.SetView(new GeoCoordinate(23.725214300000000000, 90.397499599999950000), 15);
                map1.SetView(new GeoCoordinate(23.753203, 90.411708), 15);

                lo = Convert.ToDouble(longitude);
                la = Convert.ToDouble(latitude);

                Pushpin pushpin0 = new Pushpin();
                pushpin0.Content = "MY POSITION";
                pushpin0.Background = new SolidColorBrush(Colors.Green);
                //pushpin0.Location = new GeoCoordinate(la, lo);
                pushpin0.Location = new GeoCoordinate(23.753203, 90.411708);
                pushpin0.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin0_MouseLeftButtonUp);

                m_PushpinLayer.AddChild(pushpin0, pushpin0.Location);
            }
        }

        public void pushPin()
        {

            MapLayer m_PushpinLayer = new MapLayer();
            map1.Children.Add(m_PushpinLayer);

            Pushpin pushpin1 = new Pushpin();
            pushpin1.Content = "PG Hospital";
            pushpin1.Background = new SolidColorBrush(Colors.Blue);
            pushpin1.Location = new GeoCoordinate(23.709921, 90.40714300000002);       // PG Hospital
            pushpin1.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin1_MouseLeftButtonUp);

            Pushpin pushpin2 = new Pushpin();
            pushpin2.Content = "Dhaka Medical College Hospital";
            pushpin2.Background = new SolidColorBrush(Colors.Brown);
            pushpin2.Location = new GeoCoordinate(23.725214300000000000, 90.397499599999950000);       // Dhaka Medical College Hospital
            pushpin2.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin2_MouseLeftButtonUp);

            Pushpin pushpin3 = new Pushpin();
            pushpin3.Content = "Sir Salimullah Medical College Hospital (Midford)";
            pushpin3.Background = new SolidColorBrush(Colors.Blue);
            pushpin3.Location = new GeoCoordinate(23.7111322, 90.40113500000007);       //Sir Salimullah Medical College Hospital
            pushpin3.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin3_MouseLeftButtonUp);

            Pushpin pushpin4 = new Pushpin();
            pushpin4.Content = "Holy Family Hospital";
            pushpin4.Background = new SolidColorBrush(Colors.DarkGray);
            pushpin4.Location = new GeoCoordinate(23.7467686, 90.4033885);    // Holy Family Hospital
            pushpin4.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin4_MouseLeftButtonUp);

            Pushpin pushpin5 = new Pushpin();
            pushpin5.Content = "Islamic Eye Hospital";
            pushpin5.Background = new SolidColorBrush(Colors.Gray);
            pushpin5.Location = new GeoCoordinate(23.709921, 90.40714300000002);    // Islamic Eye Hospital
            pushpin5.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin5_MouseLeftButtonUp);

            Pushpin pushpin6 = new Pushpin();
            pushpin6.Content = "Shishu Haspatal";
            pushpin6.Background = new SolidColorBrush(Colors.Red);
            pushpin6.Location = new GeoCoordinate(23.7730685, 90.3699805);    // Shishu Haspatal
            pushpin6.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin6_MouseLeftButtonUp);

            Pushpin pushpin7 = new Pushpin();
            pushpin7.Content = "BIRDEM";
            pushpin7.Background = new SolidColorBrush(Colors.LightGray);
            pushpin7.Location = new GeoCoordinate(23.739862, 90.396531);    // Birdem
            pushpin7.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin7_MouseLeftButtonUp);

            Pushpin pushpin8 = new Pushpin();
            pushpin8.Content = "Lion Eye Hospital";
            pushpin8.Background = new SolidColorBrush(Colors.Red);
            pushpin8.Location = new GeoCoordinate(23.9277926, 90.70273780000002);    // Lion Eye Hospital
            pushpin8.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin8_MouseLeftButtonUp);

            Pushpin pushpin9 = new Pushpin();
            pushpin9.Content = "Oethopadic Hospital";
            pushpin9.Background = new SolidColorBrush(Colors.Orange);
            pushpin9.Location = new GeoCoordinate(23.7198695, 90.42879040000003);    // Oethopadic Hospital
            pushpin9.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin9_MouseLeftButtonUp);

            Pushpin pushpin10 = new Pushpin();
            pushpin10.Content = "Shahid Suhrawardy Hospital";
            pushpin10.Background = new SolidColorBrush(Colors.Purple);
            pushpin10.Location = new GeoCoordinate(23.7691615, 90.37101029999997);    // Shahid Suhrawardy Hospital
            pushpin10.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin10_MouseLeftButtonUp);

            Pushpin pushpin11 = new Pushpin();
            pushpin11.Content = "Bangabandhu Sheikh Mujib Medical University";
            pushpin11.Background = new SolidColorBrush(Colors.Orange);
            pushpin11.Location = new GeoCoordinate(23.7389142, 90.39477829999998);    // Bangabandhu Sheikh Mujib Medical University
            pushpin11.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin11_MouseLeftButtonUp);

            //Pushpin pushpin12 = new Pushpin();
            //pushpin12.Content = "Tahirunnesa Memorial Medical Center";
            //pushpin12.Background = new SolidColorBrush(Colors.Green);
            //pushpin12.Location = new GeoCoordinate(23.709921, 90.40714300000002);    // Tahirunnesa Memorial Medical Center
            //pushpin12.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin12_MouseLeftButtonUp);

            Pushpin pushpin13 = new Pushpin();
            pushpin13.Content = "National Heart Foundation Hospital";
            pushpin13.Background = new SolidColorBrush(Colors.Blue);
            pushpin13.Location = new GeoCoordinate(23.8037449, 90.36195509999993);    // National Heart Foundation Hospital
            pushpin13.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin13_MouseLeftButtonUp);

            Pushpin pushpin14 = new Pushpin();
            pushpin14.Content = "Ahsania Mission Cancer & General Hospital";
            pushpin14.Background = new SolidColorBrush(Colors.Brown);
            pushpin14.Location = new GeoCoordinate(23.8034963, 90.37722629999996);    // Ahsania Mission Cancer & General Hospital
            pushpin14.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin14_MouseLeftButtonUp);

            Pushpin pushpin15 = new Pushpin();
            pushpin15.Content = "Appolo Hospitals Network";
            pushpin15.Background = new SolidColorBrush(Colors.Blue);
            pushpin15.Location = new GeoCoordinate(23.810122, 90.43118);    // Appolo Hospitals Network
            pushpin15.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin15_MouseLeftButtonUp);

            Pushpin pushpin16 = new Pushpin();
            pushpin16.Content = "Central Hospital";
            pushpin16.Background = new SolidColorBrush(Colors.DarkGray);
            pushpin16.Location = new GeoCoordinate(23.7433142, 90.38413509999998);    // Central Hospital
            pushpin16.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin16_MouseLeftButtonUp);

            //Pushpin pushpin17 = new Pushpin();
            //pushpin17.Content = "Gulshan Mother & Child Clinic";
            //pushpin17.Background = new SolidColorBrush(Colors.Gray);
            //pushpin17.Location = new GeoCoordinate(23.709921, 90.40714300000002);    // Gulshan Mother & Child Clinic
            //pushpin17.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin17_MouseLeftButtonUp);

            Pushpin pushpin18 = new Pushpin();
            pushpin18.Content = "Labaid Cardiac Hospital";
            pushpin18.Background = new SolidColorBrush(Colors.Green);
            pushpin18.Location = new GeoCoordinate(23.74161, 90.38347539999995);    // Labaid Cardiac Hospital
            pushpin18.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin18_MouseLeftButtonUp);

            Pushpin pushpin19 = new Pushpin();
            pushpin19.Content = "Medinova Medical Services Ltd.";
            pushpin19.Background = new SolidColorBrush(Colors.Orange);
            pushpin19.Location = new GeoCoordinate(23.7415303, 90.37501440000005);    // Medinova Medical Services Ltd.
            pushpin19.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin19_MouseLeftButtonUp);

            Pushpin pushpin20 = new Pushpin();
            pushpin20.Content = "Samorita Hospital Ltd.";
            pushpin20.Background = new SolidColorBrush(Colors.Gray);
            pushpin20.Location = new GeoCoordinate(23.7525608, 90.38510489999999);    // Samorita Hospital Ltd.
            pushpin20.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin20_MouseLeftButtonUp);

            Pushpin pushpin21 = new Pushpin();
            pushpin21.Content = "United Hospital Limited";
            pushpin21.Background = new SolidColorBrush(Colors.Blue);
            pushpin21.Location = new GeoCoordinate(23.804959, 90.415718);       // United Hospital Limited
            pushpin21.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin21_MouseLeftButtonUp);

            Pushpin pushpin22 = new Pushpin();
            pushpin22.Content = "Square Hospital Ltd.";
            pushpin22.Background = new SolidColorBrush(Colors.Brown);
            pushpin22.Location = new GeoCoordinate(23.773406, 90.382354);       // Square Hospital Ltd.
            pushpin22.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin22_MouseLeftButtonUp);

            Pushpin pushpin23 = new Pushpin();
            pushpin23.Content = "Harun Eye Foundation & Green Hospital";
            pushpin23.Background = new SolidColorBrush(Colors.Blue);
            pushpin23.Location = new GeoCoordinate(23.743171, 90.3827546);       //Harun Eye Foundation & Green Hospital
            pushpin23.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin23_MouseLeftButtonUp);

            Pushpin pushpin24 = new Pushpin();
            pushpin24.Content = "Ibn Sina Hospital";
            pushpin24.Background = new SolidColorBrush(Colors.DarkGray);
            pushpin24.Location = new GeoCoordinate(23.7515336, 90.36907199999996);    // Ibn Sina Hospital
            pushpin24.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin24_MouseLeftButtonUp);

            Pushpin pushpin25 = new Pushpin();
            pushpin25.Content = "Ahsania Mission Cancer Hospital";
            pushpin25.Background = new SolidColorBrush(Colors.Gray);
            pushpin25.Location = new GeoCoordinate(23.8034963, 90.37722629999996);    // Ahsania Mission Cancer Hospital
            pushpin25.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin25_MouseLeftButtonUp);

            Pushpin pushpin26 = new Pushpin();
            pushpin26.Content = "Al-Rajhi Hospital Pvt. Ltd.";
            pushpin26.Background = new SolidColorBrush(Colors.Red);
            pushpin26.Location = new GeoCoordinate(23.7591158, 90.39022380000006);    // Al-Rajhi Hospital Pvt. Ltd.
            pushpin26.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin26_MouseLeftButtonUp);

            Pushpin pushpin27 = new Pushpin();
            pushpin27.Content = "New Al-Rajhi Hospital";
            pushpin27.Background = new SolidColorBrush(Colors.LightGray);
            pushpin27.Location = new GeoCoordinate(23.7443082, 90.38467820000005);    // New Al-Rajhi Hospital
            pushpin27.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin27_MouseLeftButtonUp);

            Pushpin pushpin28 = new Pushpin();
            pushpin28.Content = "Crescent Gastroliver & General Hospital Ltd.";
            pushpin28.Background = new SolidColorBrush(Colors.Red);
            pushpin28.Location = new GeoCoordinate(23.7458412, 90.38521530000003);    // Crescent Gastroliver & General Hospital Ltd.
            pushpin28.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin28_MouseLeftButtonUp);

            Pushpin pushpin29 = new Pushpin();
            pushpin29.Content = "Farida Clinic";
            pushpin29.Background = new SolidColorBrush(Colors.Orange);
            pushpin29.Location = new GeoCoordinate(23.7498138, 90.42903009999998);    // Farida Clinic
            pushpin29.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin29_MouseLeftButtonUp);

            Pushpin pushpin30 = new Pushpin();
            pushpin30.Content = "Islami Bank Central Hospital";
            pushpin30.Background = new SolidColorBrush(Colors.Purple);
            pushpin30.Location = new GeoCoordinate(23.7375737, 90.40982080000003);    // Islami Bank Central Hospital
            pushpin30.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin30_MouseLeftButtonUp);

            Pushpin pushpin31 = new Pushpin();
            pushpin31.Content = "Islami Bank Hospital";
            pushpin31.Background = new SolidColorBrush(Colors.LightGray);
            pushpin31.Location = new GeoCoordinate(23.7378579, 90.42229670000006);    // Islami Bank Hospital
            pushpin31.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin31_MouseLeftButtonUp);

            Pushpin pushpin32 = new Pushpin();
            pushpin32.Content = "Japan Bangladesh Friendship Hospital";
            pushpin32.Background = new SolidColorBrush(Colors.Green);
            pushpin32.Location = new GeoCoordinate(23.7893397, 90.41774429999998);    // Japan Bangladesh Friendship Hospital
            pushpin32.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin32_MouseLeftButtonUp);

            Pushpin pushpin33 = new Pushpin();
            pushpin33.Content = "Monowara Hospital (Pvt.) Ltd.";
            pushpin33.Background = new SolidColorBrush(Colors.Blue);
            pushpin33.Location = new GeoCoordinate(23.7438176, 90.41131940000002);    // Monowara Hospital (Pvt.) Ltd.
            pushpin33.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin33_MouseLeftButtonUp);

            Pushpin pushpin34 = new Pushpin();
            pushpin34.Content = "Ahmad Medical Centre";
            pushpin34.Background = new SolidColorBrush(Colors.Brown);
            pushpin34.Location = new GeoCoordinate(23.7390022, 90.38506080000002);    // Ahmad Medical Centre
            pushpin34.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin34_MouseLeftButtonUp);

            Pushpin pushpin35 = new Pushpin();
            pushpin35.Content = "Dhaka Hospital";
            pushpin35.Background = new SolidColorBrush(Colors.Blue);
            pushpin35.Location = new GeoCoordinate(23.4893342, 90.44221040000002);    // Dhaka Hospital
            pushpin35.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin35_MouseLeftButtonUp);

            Pushpin pushpin36 = new Pushpin();
            pushpin36.Content = "Ad-din Hospital";
            pushpin36.Background = new SolidColorBrush(Colors.DarkGray);
            pushpin36.Location = new GeoCoordinate(23.7483736, 90.40521030000002);    // Ad-din Hospital
            pushpin36.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin36_MouseLeftButtonUp);

            Pushpin pushpin37 = new Pushpin();
            pushpin37.Content = "Al-Baraka Hospital Complex (Pvt) Ltd.";
            pushpin37.Background = new SolidColorBrush(Colors.Gray);
            pushpin37.Location = new GeoCoordinate(23.6822209, 90.44211259999997);    // Al-Baraka Hospital Complex (Pvt) Ltd.
            pushpin37.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin37_MouseLeftButtonUp);

            Pushpin pushpin38 = new Pushpin();
            pushpin38.Content = "Al-Barakah Kidney Hospital";
            pushpin38.Background = new SolidColorBrush(Colors.Green);
            pushpin38.Location = new GeoCoordinate(23.7468288, 90.39785729999994);    // Al-Barakah Kidney Hospital
            pushpin38.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin38_MouseLeftButtonUp);

            Pushpin pushpin39 = new Pushpin();
            pushpin39.Content = "Al-Fateh Medical Services Pvt. Ltd.";
            pushpin39.Background = new SolidColorBrush(Colors.Orange);
            pushpin39.Location = new GeoCoordinate(23.7823407, 90.36067309999999);    // Al-Fateh Medical Services Pvt. Ltd.
            pushpin39.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin39_MouseLeftButtonUp);

            Pushpin pushpin40 = new Pushpin();
            pushpin40.Content = "Al-Manar Hospital Ltd.";
            pushpin40.Background = new SolidColorBrush(Colors.Gray);
            pushpin40.Location = new GeoCoordinate(23.7551788, 90.36729590000004);    // Al-Manar Hospital Ltd.
            pushpin40.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin40_MouseLeftButtonUp);

            Pushpin pushpin41 = new Pushpin();
            pushpin41.Content = "Al-Markajul Islami Hospital";
            pushpin41.Background = new SolidColorBrush(Colors.Blue);
            pushpin41.Location = new GeoCoordinate(23.7605684, 90.35582260000001);       // Al-Markajul Islami Hospital
            pushpin41.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin41_MouseLeftButtonUp);

            Pushpin pushpin42 = new Pushpin();
            pushpin42.Content = "Al-Noor Eye Hospital";
            pushpin42.Background = new SolidColorBrush(Colors.Brown);
            pushpin42.Location = new GeoCoordinate(23.7538577, 90.36573299999998);       // Al-Noor Eye Hospital
            pushpin42.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin42_MouseLeftButtonUp);

            Pushpin pushpin43 = new Pushpin();
            pushpin43.Content = "Al-Sami Hospital Pvt. Ltd.";
            pushpin43.Background = new SolidColorBrush(Colors.Blue);
            pushpin43.Location = new GeoCoordinate(23.7805462, 90.42665839999995);       // Al-Sami Hospital Pvt. Ltd.
            pushpin43.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin43_MouseLeftButtonUp);

            Pushpin pushpin44 = new Pushpin();
            pushpin44.Content = "Al-Sheefa Diagnostic & Medical Center";
            pushpin44.Background = new SolidColorBrush(Colors.DarkGray);
            pushpin44.Location = new GeoCoordinate(23.7647787, 90.42135759999996);    // Al-Sheefa Diagnostic & Medical Center
            pushpin44.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin44_MouseLeftButtonUp);

            Pushpin pushpin45 = new Pushpin();
            pushpin45.Content = "Arafat Medical (Pvt.) Ltd";
            pushpin45.Background = new SolidColorBrush(Colors.Gray);
            pushpin45.Location = new GeoCoordinate(23.7124644, 90.40046749999999);    // Arafat Medical (Pvt.) Ltd
            pushpin45.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin45_MouseLeftButtonUp);

            Pushpin pushpin46 = new Pushpin();
            pushpin46.Content = "Badda General Hospital Pvt. Ltd.";
            pushpin46.Background = new SolidColorBrush(Colors.Red);
            pushpin46.Location = new GeoCoordinate(23.7805462, 90.42665839999995);    // Badda General Hospital Pvt. Ltd
            pushpin46.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin46_MouseLeftButtonUp);

            Pushpin pushpin47 = new Pushpin();
            pushpin47.Content = "Bangladesh Eye Hospital";
            pushpin47.Background = new SolidColorBrush(Colors.LightGray);
            pushpin47.Location = new GeoCoordinate(23.7436115, 90.38348010000004);    // Bangladesh Eye Hospital
            pushpin47.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin47_MouseLeftButtonUp);

            Pushpin pushpin48 = new Pushpin();
            pushpin48.Content = "Bangladesh Heart & Chest Hospital Ltd.";
            pushpin48.Background = new SolidColorBrush(Colors.Red);
            pushpin48.Location = new GeoCoordinate(23.7436115, 90.38348010000004);    // Bangladesh Heart & Chest Hospital Ltd.
            pushpin48.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin48_MouseLeftButtonUp);

            Pushpin pushpin49 = new Pushpin();
            pushpin49.Content = "Bhuya Poly Clinic";
            pushpin49.Background = new SolidColorBrush(Colors.Orange);
            pushpin49.Location = new GeoCoordinate(23.7480428, 90.41216110000005);    // Bhuya Poly Clinic
            pushpin49.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin49_MouseLeftButtonUp);

            Pushpin pushpin50 = new Pushpin();
            pushpin50.Content = "Brain & Mind Hospital (Pvt) Ltd.";
            pushpin50.Background = new SolidColorBrush(Colors.Purple);
            pushpin50.Location = new GeoCoordinate(23.7697119, 90.38950769999997);    // Brain & Mind Hospital (Pvt) Ltd.
            pushpin50.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin50_MouseLeftButtonUp);

            Pushpin pushpin51 = new Pushpin();
            pushpin51.Content = "Brighton Hospital & Diagnostic Center";
            pushpin51.Background = new SolidColorBrush(Colors.Purple);
            pushpin51.Location = new GeoCoordinate(23.7444028, 90.39226899999994);    // Brighton Hospital & Diagnostic Center
            pushpin51.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin51_MouseLeftButtonUp);

            Pushpin pushpin52 = new Pushpin();
            pushpin52.Content = "City General Hospital";
            pushpin52.Background = new SolidColorBrush(Colors.LightGray);
            pushpin52.Location = new GeoCoordinate(23.7436115, 90.38348010000004);    // City General Hospital
            pushpin52.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin52_MouseLeftButtonUp);

            Pushpin pushpin53 = new Pushpin();
            pushpin53.Content = "Comfort Nursing Home";
            pushpin53.Background = new SolidColorBrush(Colors.Green);
            pushpin53.Location = new GeoCoordinate(23.738915900000000000, 90.390444399999980000);    // Comfort Nursing Home
            pushpin53.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin53_MouseLeftButtonUp);

            Pushpin pushpin54 = new Pushpin();
            pushpin54.Content = "Conscious Health Services Ltd.";
            pushpin54.Background = new SolidColorBrush(Colors.Blue);
            pushpin54.Location = new GeoCoordinate(23.7390022, 90.38506080000002);    // Conscious Health Services Ltd.
            pushpin54.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin54_MouseLeftButtonUp);

            Pushpin pushpin55 = new Pushpin();
            pushpin55.Content = "Crescent Hospital";
            pushpin55.Background = new SolidColorBrush(Colors.Brown);
            pushpin55.Location = new GeoCoordinate(23.7637361, 90.3569685);    // Crescent Hospital
            pushpin55.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin55_MouseLeftButtonUp);

            Pushpin pushpin56 = new Pushpin();
            pushpin56.Content = "Delta Hospital Ltd.";
            pushpin56.Background = new SolidColorBrush(Colors.Blue);
            pushpin56.Location = new GeoCoordinate(23.7881094, 90.3530958);    // Delta Hospital Ltd.
            pushpin56.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin56_MouseLeftButtonUp);

            Pushpin pushpin57 = new Pushpin();
            pushpin57.Content = "Dhaka Community Hospital";
            pushpin57.Background = new SolidColorBrush(Colors.DarkGray);
            pushpin57.Location = new GeoCoordinate(23.7498004, 90.40914420000001);    // Dhaka Community Hospital
            pushpin57.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin57_MouseLeftButtonUp);

            Pushpin pushpin58 = new Pushpin();
            pushpin58.Content = "Dhaka Health Care Ltd.";
            pushpin58.Background = new SolidColorBrush(Colors.Gray);
            pushpin58.Location = new GeoCoordinate(23.7805228, 90.35392130000002);    // Dhaka Health Care Ltd.
            pushpin58.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin58_MouseLeftButtonUp);

            Pushpin pushpin59 = new Pushpin();
            pushpin59.Content = "Dhaka Monorog Clinic";
            pushpin59.Background = new SolidColorBrush(Colors.Green);
            pushpin59.Location = new GeoCoordinate(23.8154178, 90.36773719999997);    // Dhaka Monorog Clinic
            pushpin59.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin59_MouseLeftButtonUp);

            Pushpin pushpin60 = new Pushpin();
            pushpin60.Content = "Dhaka Renal Centre & Poly Clinic";
            pushpin60.Background = new SolidColorBrush(Colors.Orange);
            pushpin60.Location = new GeoCoordinate(23.7457274, 90.38573250000002);    // Dhaka Renal Centre & Poly Clinic
            pushpin60.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin60_MouseLeftButtonUp);

            Pushpin pushpin61 = new Pushpin();
            pushpin61.Content = "Dhanmondi Clinic Pvt. Ltd.";
            pushpin61.Background = new SolidColorBrush(Colors.Gray);
            pushpin61.Location = new GeoCoordinate(23.7390022, 90.38506080000002);    // Dhanmondi Clinic Pvt. Ltd.
            pushpin61.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin61_MouseLeftButtonUp);

            Pushpin pushpin62 = new Pushpin();
            pushpin62.Content = "Dhanmondi Hospital Pvt. Ltd.";
            pushpin62.Background = new SolidColorBrush(Colors.Blue);
            pushpin62.Location = new GeoCoordinate(23.7443082, 90.38467820000005);     // Dhanmondi Hospital Pvt. Ltd.
            pushpin62.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin62_MouseLeftButtonUp);

            Pushpin pushpin63 = new Pushpin();
            pushpin63.Content = "Dhanmondi South East Hospital";
            pushpin63.Background = new SolidColorBrush(Colors.Brown);
            pushpin63.Location = new GeoCoordinate(23.7436115, 90.38348010000004);       // Dhanmondi South East Hospital
            pushpin63.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin63_MouseLeftButtonUp);

            Pushpin pushpin64 = new Pushpin();
            pushpin64.Content = "Dr. Salahuddin Hospital";
            pushpin64.Background = new SolidColorBrush(Colors.Blue);
            pushpin64.Location = new GeoCoordinate(23.7369033, 90.38531540000008);       //Dr. Salahuddin Hospital
            pushpin64.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin64_MouseLeftButtonUp);

            Pushpin pushpin65 = new Pushpin();
            pushpin65.Content = "Farabi General Hospital";
            pushpin65.Background = new SolidColorBrush(Colors.DarkGray);
            pushpin65.Location = new GeoCoordinate(23.7546058, 90.37520310000002);    // Farabi General Hospital
            pushpin65.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin65_MouseLeftButtonUp);

            Pushpin pushpin66 = new Pushpin();
            pushpin66.Content = "Fuad Al-Khatib Hospital";
            pushpin66.Background = new SolidColorBrush(Colors.Gray);
            pushpin66.Location = new GeoCoordinate(23.8045117, 90.36071370000002);    // Fuad Al-Khatib Hospital
            pushpin66.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin66_MouseLeftButtonUp);

            Pushpin pushpin67 = new Pushpin();
            pushpin67.Content = "Gastro-liver Clinic";
            pushpin67.Background = new SolidColorBrush(Colors.Red);
            pushpin67.Location = new GeoCoordinate(23.7501278, 90.38787130000003);    // Gastro-liver Clinic
            pushpin67.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin67_MouseLeftButtonUp);

            Pushpin pushpin68 = new Pushpin();
            pushpin68.Content = "General Medical Hospital";
            pushpin68.Background = new SolidColorBrush(Colors.LightGray);
            pushpin68.Location = new GeoCoordinate(23.7390022, 90.38506080000002);    // General Medical Hospital
            pushpin68.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin68_MouseLeftButtonUp);

            Pushpin pushpin69 = new Pushpin();
            pushpin69.Content = "Gonoshasthya Nagor Hospital";
            pushpin69.Background = new SolidColorBrush(Colors.Red);
            pushpin69.Location = new GeoCoordinate(23.743381, 90.38270749999992);    // Gonoshasthya Nagor Hospital
            pushpin69.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin69_MouseLeftButtonUp);

            Pushpin pushpin70 = new Pushpin();
            pushpin70.Content = "Green Eye & General Hospital";
            pushpin70.Background = new SolidColorBrush(Colors.Orange);
            pushpin70.Location = new GeoCoordinate(23.7436115, 90.38348010000004);    // Green Eye & General Hospital
            pushpin70.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin70_MouseLeftButtonUp);

            Pushpin pushpin71 = new Pushpin();
            pushpin71.Content = "Greenland Hospital";
            pushpin71.Background = new SolidColorBrush(Colors.LightGray);
            pushpin71.Location = new GeoCoordinate(23.8683249, 90.39912690000006);    // Greenland Hospital
            pushpin71.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin71_MouseLeftButtonUp);

            Pushpin pushpin72 = new Pushpin();
            pushpin72.Content = "Gulshan Children's Clinic";
            pushpin72.Background = new SolidColorBrush(Colors.Green);
            pushpin72.Location = new GeoCoordinate(23.8268027, 90.4432458);    // Gulshan Children's Clinic
            pushpin72.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin72_MouseLeftButtonUp);

            Pushpin pushpin73 = new Pushpin();
            pushpin73.Content = "Islamia Arogya Sadan";
            pushpin73.Background = new SolidColorBrush(Colors.Blue);
            pushpin73.Location = new GeoCoordinate(23.7436115, 90.38348010000004);    // Islamia Arogya Sadan
            pushpin73.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin73_MouseLeftButtonUp);

            Pushpin pushpin74 = new Pushpin();
            pushpin74.Content = "Jebel-E-Nur General Hospital Ltd.";
            pushpin74.Background = new SolidColorBrush(Colors.Brown);
            pushpin74.Location = new GeoCoordinate(23.7369033, 90.38531540000008);    // Jebel-E-Nur General Hospital Ltd.
            pushpin74.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin74_MouseLeftButtonUp);

            Pushpin pushpin75 = new Pushpin();
            pushpin75.Content = "Life line Medical Services";
            pushpin75.Background = new SolidColorBrush(Colors.Blue);
            pushpin75.Location = new GeoCoordinate(23.7776282, 90.40544980000004);    // Life line Medical Services
            pushpin75.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin75_MouseLeftButtonUp);

            Pushpin pushpin76 = new Pushpin();
            pushpin76.Content = "Laila Shakoor Memorial Hospital";
            pushpin76.Background = new SolidColorBrush(Colors.DarkGray);
            pushpin76.Location = new GeoCoordinate(23.7517816, 90.3856882);    // Laila Shakoor Memorial Hospital
            pushpin76.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin76_MouseLeftButtonUp);

            Pushpin pushpin77 = new Pushpin();
            pushpin77.Content = "Marie Stopes Clinic";
            pushpin77.Background = new SolidColorBrush(Colors.Gray);
            pushpin77.Location = new GeoCoordinate(23.7389159, 90.39044439999998);    // Marie Stopes Clinic
            pushpin77.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin77_MouseLeftButtonUp);

            Pushpin pushpin78 = new Pushpin();
            pushpin78.Content = "Medi Aid Clinic";
            pushpin78.Background = new SolidColorBrush(Colors.Green);
            pushpin78.Location = new GeoCoordinate(23.751615, 90.38016919999995);    // Medi Aid Clinic
            pushpin78.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin78_MouseLeftButtonUp);

            Pushpin pushpin79 = new Pushpin();
            pushpin79.Content = "Memory Medical Centre";
            pushpin79.Background = new SolidColorBrush(Colors.Orange);
            pushpin79.Location = new GeoCoordinate(23.7468288, 90.39785729999994);    // Memory Medical Centre
            pushpin79.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin79_MouseLeftButtonUp);

            Pushpin pushpin80 = new Pushpin();
            pushpin80.Content = "Metropolitan Asthma Chest & Allergy Centre";
            pushpin80.Background = new SolidColorBrush(Colors.Gray);
            pushpin80.Location = new GeoCoordinate(23.7191944, 90.40946099999996);    // Metropolitan Asthma Chest & Allergy Centre
            pushpin80.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin80_MouseLeftButtonUp);

            Pushpin pushpin81 = new Pushpin();
            pushpin81.Content = "Metropolitan Medical Centre";
            pushpin81.Background = new SolidColorBrush(Colors.Blue);
            pushpin81.Location = new GeoCoordinate(23.7735097, 90.39866260000008);       // Metropolitan Medical Centre
            pushpin81.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin81_MouseLeftButtonUp);

            Pushpin pushpin82 = new Pushpin();
            pushpin82.Content = "Millennium Heart & General Hospital Ltd.";
            pushpin82.Background = new SolidColorBrush(Colors.Brown);
            pushpin82.Location = new GeoCoordinate(23.7551788, 90.36729590000004);       // Millennium Heart & General Hospital Ltd.
            pushpin82.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin82_MouseLeftButtonUp);

            Pushpin pushpin83 = new Pushpin();
            pushpin83.Content = "Mirpur General Hospital";
            pushpin83.Background = new SolidColorBrush(Colors.Blue);
            pushpin83.Location = new GeoCoordinate(23.7961437, 90.35004170000002);       // Mirpur General Hospital
            pushpin83.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin83_MouseLeftButtonUp);

            Pushpin pushpin84 = new Pushpin();
            pushpin84.Content = "Modern Clinic";
            pushpin84.Background = new SolidColorBrush(Colors.DarkGray);
            pushpin84.Location = new GeoCoordinate(23.760639, 90.43633369999998);    // Modern Clinic
            pushpin84.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin84_MouseLeftButtonUp);

            Pushpin pushpin85 = new Pushpin();
            pushpin85.Content = "Module General Hospital";
            pushpin85.Background = new SolidColorBrush(Colors.Gray);
            pushpin85.Location = new GeoCoordinate(23.7416199, 90.39308040000003);    // Module General Hospital
            pushpin85.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin85_MouseLeftButtonUp);

            Pushpin pushpin86 = new Pushpin();
            pushpin86.Content = "Motijheel Nursing Home Pvt. Ltd.";
            pushpin86.Background = new SolidColorBrush(Colors.Red);
            pushpin86.Location = new GeoCoordinate(23.7373301, 90.40926139999999);    // Motijheel Nursing Home Pvt. Ltd.
            pushpin86.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin86_MouseLeftButtonUp);

            Pushpin pushpin87 = new Pushpin();
            pushpin87.Content = "Mukti Mental Hospital, Drug & Alcohol Treatment Center";
            pushpin87.Background = new SolidColorBrush(Colors.LightGray);
            pushpin87.Location = new GeoCoordinate(23.7930652, 90.41074460000004);    // Mukti Mental Hospital, Drug & Alcohol Treatment Center
            pushpin87.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin87_MouseLeftButtonUp);

            Pushpin pushpin88 = new Pushpin();
            pushpin88.Content = "Naz-E-Noor Hospital Pvt. Ltd.";
            pushpin88.Background = new SolidColorBrush(Colors.Red);
            pushpin88.Location = new GeoCoordinate(23.7390022, 90.38506080000002);    // Naz-E-Noor Hospital Pvt. Ltd.
            pushpin88.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin88_MouseLeftButtonUp);

            Pushpin pushpin89 = new Pushpin();
            pushpin89.Content = "Neurology Foundation Hospital";
            pushpin89.Background = new SolidColorBrush(Colors.Orange);
            pushpin89.Location = new GeoCoordinate(23.751615, 90.38016919999995);    // Neurology Foundation Hospital
            pushpin89.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin89_MouseLeftButtonUp);

            Pushpin pushpin90 = new Pushpin();
            pushpin90.Content = " New Dhaka Clinic";
            pushpin90.Background = new SolidColorBrush(Colors.Purple);
            pushpin90.Location = new GeoCoordinate(23.725012, 90.405067);    //  New Dhaka Clinic
            pushpin90.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin90_MouseLeftButtonUp);

            Pushpin pushpin91 = new Pushpin();
            pushpin91.Content = "New Mukti Clinic";
            pushpin91.Background = new SolidColorBrush(Colors.Purple);
            pushpin91.Location = new GeoCoordinate(23.7389159, 90.39044439999998);    // New Mukti Clinic (1)
            pushpin91.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin91_MouseLeftButtonUp);

            Pushpin pushpin92 = new Pushpin();
            pushpin92.Content = "New Mukti Clinic";
            pushpin92.Background = new SolidColorBrush(Colors.LightGray);
            pushpin92.Location = new GeoCoordinate(23.7775426, 90.36189919999993);    // New Mukti Clinic (2)
            pushpin92.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin92_MouseLeftButtonUp);

            Pushpin pushpin93 = new Pushpin();
            pushpin93.Content = "Nirupam Hospital Pvt. Ltd.";
            pushpin93.Background = new SolidColorBrush(Colors.Green);
            pushpin93.Location = new GeoCoordinate(23.753944, 90.369553);    // Nirupam Hospital Pvt. Ltd.
            pushpin93.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin93_MouseLeftButtonUp);

            Pushpin pushpin94 = new Pushpin();
            pushpin94.Content = "Padma General Hospital Ltd.";
            pushpin94.Background = new SolidColorBrush(Colors.Blue);
            pushpin94.Location = new GeoCoordinate(23.747502, 90.392406);    // Padma General Hospital Ltd.
            pushpin94.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin94_MouseLeftButtonUp);

            Pushpin pushpin95 = new Pushpin();
            pushpin95.Content = "Paedihope Hospital For Sick Children";
            pushpin95.Background = new SolidColorBrush(Colors.Brown);
            pushpin95.Location = new GeoCoordinate(23.744026, 90.380119);    // Paedihope Hospital For Sick Children
            pushpin95.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin95_MouseLeftButtonUp);

            Pushpin pushpin96 = new Pushpin();
            pushpin96.Content = "Pan Pacific Hospital";
            pushpin96.Background = new SolidColorBrush(Colors.Blue);
            pushpin96.Location = new GeoCoordinate(23.738801, 90.422409);    // Pan Pacific Hospital
            pushpin96.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin96_MouseLeftButtonUp);

            Pushpin pushpin97 = new Pushpin();
            pushpin97.Content = "Pioneer General & Dental Hospital";
            pushpin97.Background = new SolidColorBrush(Colors.DarkGray);
            pushpin97.Location = new GeoCoordinate(23.748298, 90.412168);    // Pioneer General & Dental Hospital
            pushpin97.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin97_MouseLeftButtonUp);

            Pushpin pushpin98 = new Pushpin();
            pushpin98.Content = "Plastic & Cosmetic Surgery Clinic";
            pushpin98.Background = new SolidColorBrush(Colors.Gray);
            pushpin98.Location = new GeoCoordinate(23.81714, 90.4278392);    // Plastic & Cosmetic Surgery Clinic
            pushpin98.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin98_MouseLeftButtonUp);

            Pushpin pushpin99 = new Pushpin();
            pushpin99.Content = "Prime General Hospital";
            pushpin99.Background = new SolidColorBrush(Colors.Green);
            pushpin99.Location = new GeoCoordinate(23.9331484, 90.7094065);    // Prime General Hospital
            pushpin99.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin99_MouseLeftButtonUp);

            Pushpin pushpin100 = new Pushpin();
            pushpin100.Content = "Proshanti Hospital Ltd.";
            pushpin100.Background = new SolidColorBrush(Colors.Orange);
            pushpin100.Location = new GeoCoordinate(23.753296, 90.41468);    // Proshanti Hospital Ltd.
            pushpin100.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin100_MouseLeftButtonUp);

            Pushpin pushpin101 = new Pushpin();
            pushpin101.Content = "Renaissance Hospital & Research Institute Ltd.";
            pushpin101.Background = new SolidColorBrush(Colors.Gray);
            pushpin101.Location = new GeoCoordinate(23.7608974, 90.35870339999997);    // Renaissance Hospital & Research Institute Ltd.
            pushpin101.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin101_MouseLeftButtonUp);

            Pushpin pushpin102 = new Pushpin();
            pushpin102.Content = "Rushmono General Hospital";
            pushpin102.Background = new SolidColorBrush(Colors.Blue);
            pushpin102.Location = new GeoCoordinate(23.749039, 90.407818);       // Rushmono General Hospital
            pushpin102.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin102_MouseLeftButtonUp);

            Pushpin pushpin103 = new Pushpin();
            pushpin103.Content = "Sakalpa Dental Clinic (Pvt) Ltd.";
            pushpin103.Background = new SolidColorBrush(Colors.Brown);
            pushpin103.Location = new GeoCoordinate(23.7474828, 90.39979930000004);       // Sakalpa Dental Clinic (Pvt) Ltd.
            pushpin103.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin103_MouseLeftButtonUp);

            Pushpin pushpin104 = new Pushpin();
            pushpin104.Content = "Salauddin Ash-Shifa General Hospital Ltd.";
            pushpin104.Background = new SolidColorBrush(Colors.Blue);
            pushpin104.Location = new GeoCoordinate(23.725012, 90.420517);       //Salauddin Ash-Shifa General Hospital Ltd.
            pushpin104.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin104_MouseLeftButtonUp);

            Pushpin pushpin105 = new Pushpin();
            pushpin105.Content = "Salvation Specialised Hospital & Research Ltd.";
            pushpin105.Background = new SolidColorBrush(Colors.DarkGray);
            pushpin105.Location = new GeoCoordinate(23.7392923, 90.39050010000005);    // Salvation Specialised Hospital & Research Ltd.
            pushpin105.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin105_MouseLeftButtonUp);

            Pushpin pushpin106 = new Pushpin();
            pushpin106.Content = "Sheba Drug Abuse Mental Treatment & Rehabilitation Centre";
            pushpin106.Background = new SolidColorBrush(Colors.Gray);
            pushpin106.Location = new GeoCoordinate(23.739165, 90.390453);    // Sheba Drug Abuse Mental Treatment & Rehabilitation Centre
            pushpin106.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin106_MouseLeftButtonUp);

            Pushpin pushpin107 = new Pushpin();
            pushpin107.Content = "Shefa Nursing Home";
            pushpin107.Background = new SolidColorBrush(Colors.Red);
            pushpin107.Location = new GeoCoordinate(23.7715177, 90.36567100000002);    // Shefa Nursing Home
            pushpin107.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin107_MouseLeftButtonUp);

            Pushpin pushpin108 = new Pushpin();
            pushpin108.Content = "South Asian Hospital Ltd.";
            pushpin108.Background = new SolidColorBrush(Colors.LightGray);
            pushpin108.Location = new GeoCoordinate(23.7517816, 90.3856882);    // South Asian Hospital Ltd.
            pushpin108.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin108_MouseLeftButtonUp);

            Pushpin pushpin109 = new Pushpin();
            pushpin109.Content = "South View Hospital";
            pushpin109.Background = new SolidColorBrush(Colors.Red);
            pushpin109.Location = new GeoCoordinate(23.8176124, 90.36269360000006);    // South View Hospital
            pushpin109.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin109_MouseLeftButtonUp);

            Pushpin pushpin110 = new Pushpin();
            pushpin110.Content = "SPRC & General Hospital";
            pushpin110.Background = new SolidColorBrush(Colors.Orange);
            pushpin110.Location = new GeoCoordinate(23.747473, 90.40071);    // SPRC & General Hospital
            pushpin110.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin110_MouseLeftButtonUp);

            Pushpin pushpin111 = new Pushpin();
            pushpin111.Content = "Square Diagnostic & Hospital Services Ltd";
            pushpin111.Background = new SolidColorBrush(Colors.Gray);
            pushpin111.Location = new GeoCoordinate(23.7457863, 90.38517589999992);    // Square Diagnostic & Hospital Services Ltd
            pushpin111.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin111_MouseLeftButtonUp);

            Pushpin pushpin112 = new Pushpin();
            pushpin112.Content = "Stone Crush Hospital";
            pushpin112.Background = new SolidColorBrush(Colors.Blue);
            pushpin112.Location = new GeoCoordinate(23.7390022, 90.38506080000002);       // Stone Crush Hospital
            pushpin112.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin112_MouseLeftButtonUp);

            Pushpin pushpin113 = new Pushpin();
            pushpin113.Content = "Sumona Clinic General Hospital";
            pushpin113.Background = new SolidColorBrush(Colors.Brown);
            pushpin113.Location = new GeoCoordinate(23.708097, 90.410097);       // Sumona Clinic General Hospital
            pushpin113.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin113_MouseLeftButtonUp);

            Pushpin pushpin114 = new Pushpin();
            pushpin114.Content = "The Barakah General Hospital";
            pushpin114.Background = new SolidColorBrush(Colors.Blue);
            pushpin114.Location = new GeoCoordinate(23.742612, 90.41884);       // The Barakah General Hospital
            pushpin114.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin114_MouseLeftButtonUp);

            Pushpin pushpin115 = new Pushpin();
            pushpin115.Content = "The Health Hospital & Diagnostic Centre";
            pushpin115.Background = new SolidColorBrush(Colors.DarkGray);
            pushpin115.Location = new GeoCoordinate(23.717983, 90.42403850000005);    // The Health Hospital & Diagnostic Centre
            pushpin115.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin115_MouseLeftButtonUp);

            Pushpin pushpin116 = new Pushpin();
            pushpin116.Content = "Udayon Poly Clinic";
            pushpin116.Background = new SolidColorBrush(Colors.Gray);
            pushpin116.Location = new GeoCoordinate(23.7490924, 90.40063910000003);    // Udayon Poly Clinic
            pushpin116.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin116_MouseLeftButtonUp);

            Pushpin pushpin117 = new Pushpin();
            pushpin117.Content = "Vision Eye Hospital";
            pushpin117.Background = new SolidColorBrush(Colors.Red);
            pushpin117.Location = new GeoCoordinate(23.7551788, 90.36729590000004);    // Vision Eye Hospital
            pushpin117.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin117_MouseLeftButtonUp);

            Pushpin pushpin118 = new Pushpin();
            pushpin118.Content = "Women & Children's Hospital";
            pushpin118.Background = new SolidColorBrush(Colors.LightGray);
            pushpin118.Location = new GeoCoordinate(23.7456235, 90.37212650000004);    // Women & Children's Hospital
            pushpin118.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin118_MouseLeftButtonUp);

            Pushpin pushpin119 = new Pushpin();
            pushpin119.Content = "Aichi Hospital";
            pushpin119.Background = new SolidColorBrush(Colors.Red);
            pushpin119.Location = new GeoCoordinate(23.8758547, 90.37954379999996);    // Aichi Hospital
            pushpin119.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin119_MouseLeftButtonUp);

            Pushpin pushpin120 = new Pushpin();
            pushpin120.Content = "Anwer Khan Modern Hospital";
            pushpin120.Background = new SolidColorBrush(Colors.Orange);
            pushpin120.Location = new GeoCoordinate(23.745194, 90.38214970000001);    // Anwer Khan Modern Hospital
            pushpin120.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin120_MouseLeftButtonUp);

            Pushpin pushpin121 = new Pushpin();
            pushpin121.Content = "Aysha Memorial Specialised Hospital ";
            pushpin121.Background = new SolidColorBrush(Colors.Red);
            pushpin121.Location = new GeoCoordinate(23.7776282, 90.40544980000004);    // Aysha Memorial Specialised Hospital
            pushpin121.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin121_MouseLeftButtonUp);


            m_PushpinLayer.AddChild(pushpin1, pushpin1.Location);
            m_PushpinLayer.AddChild(pushpin2, pushpin2.Location);
            m_PushpinLayer.AddChild(pushpin3, pushpin3.Location);
            m_PushpinLayer.AddChild(pushpin4, pushpin4.Location);
            m_PushpinLayer.AddChild(pushpin5, pushpin5.Location);
            m_PushpinLayer.AddChild(pushpin6, pushpin6.Location);
            m_PushpinLayer.AddChild(pushpin7, pushpin7.Location);
            m_PushpinLayer.AddChild(pushpin8, pushpin8.Location);
            m_PushpinLayer.AddChild(pushpin9, pushpin9.Location);
            m_PushpinLayer.AddChild(pushpin10, pushpin10.Location);
            m_PushpinLayer.AddChild(pushpin11, pushpin11.Location);
            //m_PushpinLayer.AddChild(pushpin12, pushpin12.Location);
            m_PushpinLayer.AddChild(pushpin13, pushpin13.Location);
            m_PushpinLayer.AddChild(pushpin14, pushpin14.Location);
            m_PushpinLayer.AddChild(pushpin15, pushpin15.Location);
            m_PushpinLayer.AddChild(pushpin16, pushpin16.Location);
            //m_PushpinLayer.AddChild(pushpin17, pushpin17.Location);
            m_PushpinLayer.AddChild(pushpin18, pushpin18.Location);
            m_PushpinLayer.AddChild(pushpin19, pushpin19.Location);
            m_PushpinLayer.AddChild(pushpin20, pushpin20.Location);
            m_PushpinLayer.AddChild(pushpin21, pushpin21.Location);
            m_PushpinLayer.AddChild(pushpin22, pushpin22.Location);
            m_PushpinLayer.AddChild(pushpin23, pushpin23.Location);
            m_PushpinLayer.AddChild(pushpin24, pushpin24.Location);
            m_PushpinLayer.AddChild(pushpin25, pushpin25.Location);
            m_PushpinLayer.AddChild(pushpin26, pushpin26.Location);
            m_PushpinLayer.AddChild(pushpin27, pushpin27.Location);
            m_PushpinLayer.AddChild(pushpin28, pushpin28.Location);
            m_PushpinLayer.AddChild(pushpin29, pushpin29.Location);
            m_PushpinLayer.AddChild(pushpin30, pushpin30.Location);
            m_PushpinLayer.AddChild(pushpin31, pushpin31.Location);
            m_PushpinLayer.AddChild(pushpin32, pushpin32.Location);
            m_PushpinLayer.AddChild(pushpin33, pushpin33.Location);
            m_PushpinLayer.AddChild(pushpin34, pushpin34.Location);
            m_PushpinLayer.AddChild(pushpin35, pushpin35.Location);
            m_PushpinLayer.AddChild(pushpin36, pushpin36.Location);
            m_PushpinLayer.AddChild(pushpin37, pushpin37.Location);
            m_PushpinLayer.AddChild(pushpin38, pushpin38.Location);
            m_PushpinLayer.AddChild(pushpin39, pushpin39.Location);
            m_PushpinLayer.AddChild(pushpin40, pushpin40.Location);
            m_PushpinLayer.AddChild(pushpin41, pushpin41.Location);
            m_PushpinLayer.AddChild(pushpin42, pushpin42.Location);
            m_PushpinLayer.AddChild(pushpin43, pushpin43.Location);
            m_PushpinLayer.AddChild(pushpin44, pushpin44.Location);
            m_PushpinLayer.AddChild(pushpin45, pushpin45.Location);
            m_PushpinLayer.AddChild(pushpin46, pushpin46.Location);
            m_PushpinLayer.AddChild(pushpin47, pushpin47.Location);
            m_PushpinLayer.AddChild(pushpin48, pushpin48.Location);
            m_PushpinLayer.AddChild(pushpin49, pushpin49.Location);
            m_PushpinLayer.AddChild(pushpin50, pushpin50.Location);
            m_PushpinLayer.AddChild(pushpin51, pushpin51.Location);
            m_PushpinLayer.AddChild(pushpin52, pushpin52.Location);
            m_PushpinLayer.AddChild(pushpin53, pushpin53.Location);
            m_PushpinLayer.AddChild(pushpin54, pushpin54.Location);
            m_PushpinLayer.AddChild(pushpin55, pushpin55.Location);
            m_PushpinLayer.AddChild(pushpin56, pushpin56.Location);
            m_PushpinLayer.AddChild(pushpin57, pushpin57.Location);
            m_PushpinLayer.AddChild(pushpin58, pushpin58.Location);
            m_PushpinLayer.AddChild(pushpin59, pushpin59.Location);
            m_PushpinLayer.AddChild(pushpin60, pushpin60.Location);
            m_PushpinLayer.AddChild(pushpin61, pushpin61.Location);
            m_PushpinLayer.AddChild(pushpin62, pushpin62.Location);
            m_PushpinLayer.AddChild(pushpin63, pushpin63.Location);
            m_PushpinLayer.AddChild(pushpin64, pushpin64.Location);
            m_PushpinLayer.AddChild(pushpin65, pushpin65.Location);
            m_PushpinLayer.AddChild(pushpin66, pushpin66.Location);
            m_PushpinLayer.AddChild(pushpin67, pushpin67.Location);
            m_PushpinLayer.AddChild(pushpin68, pushpin68.Location);
            m_PushpinLayer.AddChild(pushpin69, pushpin69.Location);
            m_PushpinLayer.AddChild(pushpin70, pushpin70.Location);
            m_PushpinLayer.AddChild(pushpin71, pushpin71.Location);
            m_PushpinLayer.AddChild(pushpin72, pushpin72.Location);
            m_PushpinLayer.AddChild(pushpin73, pushpin73.Location);
            m_PushpinLayer.AddChild(pushpin74, pushpin74.Location);
            m_PushpinLayer.AddChild(pushpin75, pushpin75.Location);
            m_PushpinLayer.AddChild(pushpin76, pushpin76.Location);
            m_PushpinLayer.AddChild(pushpin77, pushpin77.Location);
            m_PushpinLayer.AddChild(pushpin78, pushpin78.Location);
            m_PushpinLayer.AddChild(pushpin79, pushpin79.Location);
            m_PushpinLayer.AddChild(pushpin80, pushpin80.Location);
            m_PushpinLayer.AddChild(pushpin81, pushpin81.Location);
            m_PushpinLayer.AddChild(pushpin82, pushpin82.Location);
            m_PushpinLayer.AddChild(pushpin83, pushpin83.Location);
            m_PushpinLayer.AddChild(pushpin84, pushpin84.Location);
            m_PushpinLayer.AddChild(pushpin85, pushpin85.Location);
            m_PushpinLayer.AddChild(pushpin86, pushpin86.Location);
            m_PushpinLayer.AddChild(pushpin87, pushpin87.Location);
            m_PushpinLayer.AddChild(pushpin88, pushpin88.Location);
            m_PushpinLayer.AddChild(pushpin89, pushpin89.Location);
            m_PushpinLayer.AddChild(pushpin90, pushpin90.Location);
            m_PushpinLayer.AddChild(pushpin91, pushpin91.Location);
            m_PushpinLayer.AddChild(pushpin92, pushpin92.Location);
            m_PushpinLayer.AddChild(pushpin93, pushpin93.Location);
            m_PushpinLayer.AddChild(pushpin94, pushpin94.Location);
            m_PushpinLayer.AddChild(pushpin95, pushpin95.Location);
            m_PushpinLayer.AddChild(pushpin96, pushpin96.Location);
            m_PushpinLayer.AddChild(pushpin97, pushpin97.Location);
            m_PushpinLayer.AddChild(pushpin98, pushpin98.Location);
            m_PushpinLayer.AddChild(pushpin99, pushpin99.Location);
            m_PushpinLayer.AddChild(pushpin100, pushpin100.Location);
            m_PushpinLayer.AddChild(pushpin101, pushpin101.Location);
            m_PushpinLayer.AddChild(pushpin102, pushpin102.Location);
            m_PushpinLayer.AddChild(pushpin103, pushpin103.Location);
            m_PushpinLayer.AddChild(pushpin104, pushpin104.Location);
            m_PushpinLayer.AddChild(pushpin105, pushpin105.Location);
            m_PushpinLayer.AddChild(pushpin106, pushpin106.Location);
            m_PushpinLayer.AddChild(pushpin107, pushpin107.Location);
            m_PushpinLayer.AddChild(pushpin108, pushpin108.Location);
            m_PushpinLayer.AddChild(pushpin109, pushpin109.Location);
            m_PushpinLayer.AddChild(pushpin110, pushpin110.Location);
            m_PushpinLayer.AddChild(pushpin111, pushpin111.Location);
            m_PushpinLayer.AddChild(pushpin112, pushpin112.Location);
            m_PushpinLayer.AddChild(pushpin113, pushpin113.Location);
            m_PushpinLayer.AddChild(pushpin114, pushpin114.Location);
            m_PushpinLayer.AddChild(pushpin115, pushpin115.Location);
            m_PushpinLayer.AddChild(pushpin116, pushpin116.Location);
            m_PushpinLayer.AddChild(pushpin117, pushpin117.Location);
            m_PushpinLayer.AddChild(pushpin118, pushpin118.Location);
            m_PushpinLayer.AddChild(pushpin119, pushpin119.Location);
            m_PushpinLayer.AddChild(pushpin120, pushpin120.Location);
            m_PushpinLayer.AddChild(pushpin121, pushpin121.Location);
        }

        void pushpin0_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Pushpin localPushPin = new Pushpin();
            localPushPin = (Pushpin)sender;
            MessageBox.Show("You are Here : " +
            localPushPin.Location.Latitude + "," +
            localPushPin.Location.Longitude);
        }

        void pushpin1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("PG Hospital (BSMMU)\n" +
                            "Shahabagh, Dhaka.\n" + 
                            "Tel: 8612550, 8614545-9, 9661900-59");
        }

        void pushpin2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Dhaka Medical College Hospital\n" + 
                            "Tel : 8626812-26, 8626818-25");
        }

        void pushpin3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Sir Salimullah Medical College Hospital\n" + 
                            "Midford, Dhaka - 1100.\n" +
                            "Tel: 7310061-4, 7319002-6");
        }

        void pushpin4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Holy Family Hospital\n" + 
                            "Eskaton Garden,\n" + 
                            "Tel: 8311721-25, 8321721-24");
        }

        void pushpin5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Islamic Eye Hospital\n" + 
                            "Tel: 9119315, 8112856, ICDDR-B, Tel: 8811751-60");
        }

        void pushpin6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Shishu Hashpatal\n" + 
                            "(Child Hospital) Shre-E-Bangla Nagar, Agargaon, Dhaka.\n" + 
                            "Tel: 9119119, 8116061-2");
        }

        void pushpin7_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("BIRDEM (Diabetic Hospital)\n" + 
                            "Shahabagh.\n" +
                            "Tel: 9661551-60,8616641-50");
        }

        void pushpin8_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Lion eye Hospital\n" + 
                            "Tel: 9112927");
        }

        void pushpin9_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Orthopadic Hospital\n" + 
                            "Tel: 91 14075, 91 12150");
        }

        void pushpin10_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Shahid Suhrawardy Hospital\n" + 
                            "Tel : 9122560-78, 9130800");
        }

        void pushpin11_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Bangabandhu Sheikh Mujib Medical University\n" + 
                            "Shahabagh Rd, Dhaka\n" +
                            "Phone: 02-8618652");
        }

        //void pushpin12_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    MessageBox.Show("Tahirunnesa Memorial Medical Center" + 
        //                    "Tel : 9801874,9803302");
        //}

        void pushpin13_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("National Heart Foundation Hospital\n" + 
                            "Mirpur, Dhaka.\n" + 
                            "Tel : 8014914");
        }

        void pushpin14_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Ahsania Mission Cancer & General Hospital\n" + 
                            "Plot M-1/C, Mirpur 14, Dhaka.\n" +
                            "Tel: (880-2) 9008919,8051618, Fax: 8113010,8118522");
        }

        void pushpin15_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Appolo Hospitals Network\n" + 
                            "Plot: 81,Block E, Bashundhara, Dhaka 1229.\n" +
                            "Tel (880-2)9891661-5 Fax: 9125655");
        }

        void pushpin16_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Central Hospital\n" + 
                            "House: 10/A (New-2), Road-5, Dhanmondi, Dhaka.\n" +
                            "Tel : (880-2) 9660015-19, Fax : 9667812");
        }

        //void pushpin17_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    MessageBox.Show("Gulshan Mother & Child Clinic\n" + 
        //                    "House : 11A, Road. 48, Kemal Ataturk Avenue, Banani,Dhaka.\n" +
        //                    "Tel: (880-2) 8822738, 8812992");
        //}

        void pushpin18_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Labaid Cardiac Hospital\n" + 
                            "House: 1, Road: 4, Dhanmondi, Dhaka.\n" +
                            "Tel: (880-2) 8610793-8, Fax: 8617372");
        }

        void pushpin19_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Medinova Medical Services Ltd.\n" + 
                            "House 71 /A Road 5/A, Dhanmondi, Dhakia.\n" +
                            "Tel: (880-2) 8620353");
        }

        void pushpin20_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Samorita Hospital Ltd.\n" + 
                            "89/1 Panthapath, Dhaka.\n" +
                            "Tel. (880-2)7319002-6");
        }

        void pushpin21_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("United Hospital Limited\n" + 
                            "Plot # 15, Road # 71, Gulshan-2, Dhaka\n" +
                            "Ambulance Service: 01914001326\n" +
                            "Emergency: +880 2 8836000-10(8066), 8836434-44(8066), 01914001234\n" +
                            "Information Desk: +880 2 8836000-10(8068), +880 2 8836434-44(8068)");
        }

        void pushpin22_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Square Hospital Ltd.\n" + 
                            "18/F West Panthapath, Dhaka - 1205, Bangladesh.\n" +
                            "8159457 (10 Numbers), 8142431 (10 Numbers), 8141522(10 Numbers),\n" +
                            "8144400(10 Numbers), 8142333(10Numbers), Operator Help -9,\n" +
                            "ER T&T : 8144466, 8144477, 8144488, ER Mobile : 01713377773-5,\n" +
                            "PABX Mobile: 01713141447, FAX : 9118921(General), 9114342(Commercial)\n" +
                            "www.squarehospital.com");
        }

        void pushpin23_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Harun Eye Foundation & Green Hospital\n" + 
                            "H-31, Rd-6, DRA,\n" +
                            "Phone: 8612412, 8619068, 9663183");
        }

        void pushpin24_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Ibn Sina Hospital\n" + 
                            "H-68, Rd-15/A, DRA,\n" +
                            "Phone: 8113709, 8119513-5");
        }

        void pushpin25_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Ahsania Mission Cancer Hospital" + 
                            "Plot No: M-1/C, Mirpur-14, Dhaka\n" +
                            "Phone: 9008919");
        }

        void pushpin26_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Al-Rajhi Hospital Pvt. Ltd.\n" + 
                            "12, Farmgate, Dhaka\n" +
                            "Phone: 8119229, 8121172, 9140749, 9133563-4");
        }

        void pushpin27_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("New Al-Rajhi Hospital\n" +
                            "32, Green Road\n" +
                            "Phone: 8628820-1, 8611213");
        }

        void pushpin28_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Crescent Gastroliver & General Hospital Ltd.\n" +
                            "25/I, Green Road, Dhanmondi\n" +
                            "Phone: 8621612, 8611936");
        }

        void pushpin29_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Farida Clinic\n" +
                            "23, North Shajahanpur, Dhaka\n" +
                            "Phone: 8315991");
        }

        void pushpin30_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Islami Bank Central Hospital\n" + 
                            "30 V.I.P Road, Kakrail, Dhaka\n" +
                            "Phone: 9338810, 8316166, 9355801-2. 9360331-2 ");
        }

        void pushpin31_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Islami Bank Hospital\n" +
                            "24/B Outer Circular Road, Shajahanpur\n" +
                            "Phone: 8317090, 8318715, 9336421-3");
        }

        void pushpin32_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Japan Bangladesh Friendship Hospital\n" +
                            "H-27, Rd-114, Gulshan-2\n" +
                            "Phone: 8817575, 8828855, 8827575, 8828855");
        }

        void pushpin33_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Monowara Hospital (Pvt.) Ltd.\n" +
                            "54, Siddeswari\n" +
                            "Phone: 8318135, 8318529, 8319802, 8318135");
        }

        void pushpin34_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Ahmad Medical Centre\n" +
                            "H-71/1, Rd, 15/A, DRA\n" +
                            "Phone: 8113628, 9119738");
        }

        void pushpin35_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Dhaka Hospital\n" +
                            "17, D.C Roy Road, Mitford\n" +
                            "Phone: 7320709, 7310750, 7320212, 7316643");
        }

        void pushpin36_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Ad-din Hospital\n" +
                            "2 Baro Moghbazar, Dhaka\n" +
                            "Phone: 9353391-3");
        }

        void pushpin37_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Al-Baraka Hospital Complex (Pvt) Ltd.\n" +
                            "82/8, North Jatrabari, Dhaka\n" +
                            "Phone: 7515855, 7515877");
        }

        void pushpin38_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Al-Barakah Kidney Hospital\n" +
                            "129, New Eskaton Road, Dhaka\n" +
                            "Phone: 9350884. 9351164, 9337521");
        }

        void pushpin39_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Al-Fateh Medical Services Pvt. Ltd.\n" +
                            "11 Farmgate, Dhaka\n" +
                            "Phone: 8150959");
        }

        void pushpin40_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Al-Manar Hospital Ltd.\n" +
                            "H-5/4, Block-F, Lalmatia, Dhaka\n" +
                            "Phone: 9121387, 9121588");
        }

        void pushpin41_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Al-Markajul Islami Hospital\n" +
                            "H-29, Rd-3, Shayamoli\n" +
                            "Phone: 9129426, 9129217");
        }

        void pushpin42_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Al-Noor Eye Hospital\n" +
                            "1/9/E, Lalmatia, Dhaka\n" +
                            "Phone: 9135451-2, 8114142");
        }

        void pushpin43_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Al-Sami Hospital Pvt. Ltd.\n" +
                            "Sha-23/Ka, Adarshanagar Middle Badda, Dhaka\n" +
                            "8827239 (C), 01711-076896 (M)");
        }

        void pushpin44_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Al-Sheefa Diagnostic & Medical Center\n" +
                            "351, East Rampura, DIT Road\n" +
                            "Phone: 8319882");
        }

        void pushpin45_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Arafat Medical (Pvt.) Ltd\n" +
                            "39, Mitford Road, Dhaka\n" +
                            "Phone: 7315993, 7319505, 7319461");
        }

        void pushpin46_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Badda General Hospital Pvt. Ltd\n" +
                            "Cha-107/2, North Badda, Progati Sharoni, Dhaka\n" +
                            "Phone: 8857828-30");
        }

        void pushpin47_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Bangladesh Eye Hospital\n" +
                            "H-19/1, Rd-6, Dhanmondi, Dhaka\n" +
                            "Phone: 8651950-3");
        }

        void pushpin48_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Bangladesh Heart & Chest Hospital Ltd.\n" +
                            "H-47, Rd-27, Dhanmondi\n" +
                            "Phone: 9114166, 8123977, 0188740647, 0171868817 (M)");
        }

        void pushpin49_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Bhuya Poly Clinic\n" +
                            "100, DIT Road, Malibagh, Dhaka\n" +
                            "Phone: 8356235");
        }

        void pushpin50_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Brain & Mind Hospital (Pvt) Ltd.\n" +
                            "149/A, Airport Road, Farmgate, Dhaka\n" +
                            "Phone: 8120710");
        }

        void pushpin51_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Brighton Hospital & Diagnostic Center\n" +
                            "163-164, Sonargaon Road, Hatirpool\n" +
                            "Phone: 8626901-2, 8651128-35, 9677792-5");
        }

        void pushpin52_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("City General Hospital\n" +
                            "H-120, Rd-9/A, Dhanmondi, Dhaka\n" +
                            "Phone: 9120862, 8130778");
        }

        void pushpin53_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Comfort Nursing Home\n" +
                            "167/B, Green Road, DRA, Dhaka\n" +
                            "Phone: 8124990, 8124380, 8127394");
        }

        void pushpin54_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Conscious Health Services Ltd.\n" +
                            "H-25/A, Rd-6, DRA\n" +
                            "Phone: 9665544, 9667604");
        }

        void pushpin55_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Crescent Hospital\n" +
                            "22/2, Mohammadpur, Dhaka\n" +
                            "Phone: 9117524");
        }

        void pushpin56_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Delta Hospital Ltd.\n" +
                            "26/2, Darussalam Road, Mirpur-1, Dhaka\n" +
                            "Phone: 8017151-52, 8031378-79");
        }

        void pushpin57_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Dhaka Community Hospital\n" +
                            "190/1, Bara Moghbazar, Wireless Railgate\n" +
                            "Phone: 9351190-1, 8314887");
        }

        void pushpin58_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Dhaka Health Care Ltd.\n" +
                            "2/2, Mirpur Road, Black-A, Lalmatia, Dhaka\n" +
                            "Phone: 8117571, 9137214");
        }

        void pushpin59_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Dhaka Monorog Clinic\n" +
                            "11/A-1/13, Mirpur, Rd-11, Dhaka\n" +
                            "Phone: 9005050");
        }

        void pushpin60_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Dhaka Renal Centre & Poly Clinic\n" +
                            "5, Green Corner, Green. H-27, Rd- 7, Road\n" +
                            "Phone: 8610928, 8621841");
        }

        void pushpin61_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Dhanmondi Clinic Pvt. Ltd\n" +
                            "H-2, Rd-8, DRA, Dhaka\n" +
                            "Phone: 8616014, 8617502");
        }

        void pushpin62_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Dhanmondi Hospital Pvt. Ltd.\n" +
                            "H-19/E, Green Road, Dhaka\n" +
                            "Phone: 8628849");
        }

        void pushpin63_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Dhanmondi South East Hospital\n" +
                            "H-25, Rd-5, Dhanmondi R/A, Dhaka\n" +
                            "Phone: 9669904, 9671739");
        }

        void pushpin64_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Dr. Salahuddin Hospital\n" +
                            "H-37, Rd-9/A, DRA, Dhaka\n" +
                            "Phone: 9122264, 9122264, 9121779");
        }

        void pushpin65_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Farabi General Hospital\n" +
                            "H-8/3, Rd-14, DRA, Dhaka\n" +
                            "Phone: 8122471, 9140442");
        }

        void pushpin66_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Fuad Al-Khatib Hospital\n" +
                            "Almas Tower, 282/1 Majar Road, Mirpur, Dhaka\n" +
                            "Phone: 9007188, 8013638, 9004317");
        }

        void pushpin67_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Gastro-liver Clinic\n" +
                            "69/D, Green Road, Dhaka\n" +
                            "Phone: 9667339, 8620960, 8625393");
        }

        void pushpin68_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("General Medical Hospital\n" +
                            "103, Elephant Road\n" +
                            "Phone: 8611932, 8614298");
        }

        void pushpin69_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Gonoshasthya Nagor Hospital\n" +
                            "14/E, Rd-6, Dhanmondi\n" +
                            "Phone: 8613567");
        }

        void pushpin70_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Green Eye & General Hospital\n" +
                            "H-31, Rd-6, Dhanmondi\n" +
                            "Phone: 8612412, 8619068");
        }

        void pushpin71_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Greenland Hospital\n" +
                            "H-4, Rd-4, Sector-7, Azampur, Uttara Model Town\n" +
                            "Phone: 8912663, 8915189, 8915688");
        }

        void pushpin72_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Gulshan Children's Clinic\n" +
                            "H-11, Rd-2/A, Block-J, Baridara New Bazar\n" +
                            "Phone: 8822738, 8812992");
        }

        void pushpin73_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Islamia Arogya Sadan\n" +
                            "H-35, Rd-1, DRA\n" +
                            "Phone: 8612798, 8629493, 9671612");
        }

        void pushpin74_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Jebel-E-Nur General Hospital Ltd.\n" +
                            "H-21, Rd-9/A, DRA, Dhaka\n" +
                            "Phone: 8117031, 9131300");
        }

        void pushpin75_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Life line Medical Services\n" +
                            "75/K, Rahman Mansion, Mohakhali\n" +
                            "Phone: 8826677");
        }

        void pushpin76_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Laila Shakoor Memorial Hospital\n" +
                            "69/B, Panthapath, Green Road, Dhaka\n" +
                            "Phone: 9667762");
        }

        void pushpin77_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Marie Stopes Clinic\n" +
                            "360, Elephant Road, Dhaka\n" +
                            "Phone: 8631500");
        }

        void pushpin78_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Medi Aid Clinic\n" +
                            "70/C, Lake Circus, Kalabagan\n" +
                            "Phone: 9112076, 8117043, 8118456");
        }

        void pushpin79_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Memory Medical Centre\n" +
                            "22, New Eskaton\n" +
                            "Phone: 8314317, 8319323");
        }

        void pushpin80_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Metropolitan Asthma Chest & Allergy Centre\n" +
                            "215, North Shahjahanpur, Dhaka\n" +
                            "Phone: 9330016");
        }

        void pushpin81_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Metropolitan Medical Centre\n" +
                            "Mohakhali (Opposite to Bus Terminal)\n" +
                            "Phone: 9899209, 8824155");
        }

        void pushpin82_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Millennium Heart & General Hospital Ltd.\n" +
                            "4/9, Block-F, Lalmatia, Dhaka\n" +
                            "Phone: 9124046, 9122115, 8120097");
        }

        void pushpin83_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Mirpur General Hospital\n" +
                            "H-35, Rd-1, Section-10, Mirpur\n" +
                            "Phone: 8015444, 9007873");
        }

        void pushpin84_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Modern Clinic\n" +
                            "H-5, Rd-11, Gulshan-1, Dhaka\n" +
                            "Phone: 9898685, 9894985");
        }

        void pushpin85_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Module General Hospital\n" +
                            "1/G/3, Paribag, Hatirpul, Dhaka\n" +
                            "Phone: 8610512, 8616083");
        }

        void pushpin86_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Motijheel Nursing Home Pvt. Ltd.\n" +
                            "30/A, Purana Paltan Line, VIP Road, Dhaka\n" +
                            "Phone: 9337685, 8352578");
        }

        void pushpin87_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Mukti Mental Hospital, Drug & Alcohol Treatment Center\n" +
                            "H-2, Rd-49, Gulshan-2, Dhaka\n" +
                            "Phone: 9889044, 9883991, 8814562");
        }

        void pushpin88_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Naz-E-Noor Hospital Pvt. Ltd.\n" +
                            "H-51, Rd-10/A, DRA, Dhaka\n" +
                            "8118226, 9130152");
        }

        void pushpin89_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Neurology Foundation Hospital\n" +
                            "3/1, Lake Circus, Kalabagan, Dhaka\n" +
                            "Phone: 8114846");
        }

        void pushpin90_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("New Dhaka Clinic\n" +
                            "4, D.C. Roy Road, Mitford, Dhaka\n" +
                            "Phone: 7317496, 7313557");
        }

        void pushpin91_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("New Mukti Clinic\n" +
                            "301, Elephant Road, Dhaka\n" +
                            "Phone: 8611360, 8621889");
        }

        void pushpin92_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("New Mukti Clinic\n" +
                            "12 South Kallanpur, mirpur Road\n" +
                            "Phone: 0152383639, 0171200720");
        }

        void pushpin93_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Nirupam Hospital Pvt. Ltd.\n" +
                            "H-1/5, Block-D, Lalmatia\n" +
                            "Phone: 8114429, 9131037");
        }

        void pushpin94_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Padma General Hospital Ltd\n" +
                            "290, Sonargaon Road, Dhaka\n" +
                            "Phone: 8620889-19, 9661528, 9662502");
        }

        void pushpin95_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Paedihope Hospital For Sick Children\n" +
                            "H-32/A, Rd-6, Dhanmondi, Dhaka\n" +
                            "Phone: 9671345, 9631375");
        }

        void pushpin96_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Pan Pacific Hospital\n" +
                            "24, Outer Circular Road, Shajahanpur, Dhaka\n" +
                            "Phone: 8359731, 9349794, 9351777, 9351476");
        }

        void pushpin97_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Pioneer General & Dental Hospital\n" +
                            "111, Malibagh DIT Road, Dhaka\n" +
                            "Phone: 8321904");
        }

        void pushpin98_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Plastic & Cosmetic Surgery Clinic\n" +
                            "H-78/E, Rd-12, Banani, Dhaka\n" +
                            "PHone: 8829222");
        }

        void pushpin99_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Prime General Hospital\n" +
                            "Gojaria Tower, 89/12, R.K Mission Road, Gopibagh,\n" +
                            "Phone: 7512425, 7514763");
        }

        void pushpin100_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Proshanti Hospital Ltd.\n" +
                            "3 Outer Circular Road, (Malibag-more), Dhaka\n" +
                            "8318699, 9348728");
        }

        void pushpin101_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Renaissance Hospital & Research Institute Ltd.\n" +
                            "H-60/A, Rd-4/A, Dhanmondi\n" +
                            "9664930, 8615792, 8611455");
        }

        void pushpin102_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Rushmono General Hospital\n" +
                            "208-9, Outer Circular Road, Moghbazar\n" +
                            "8317606, 8317819, 9357354, 9332358");
        }

        void pushpin103_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Sakalpa Dental Clinic (Pvt) Ltd.\n" +
                            "3, New Eskaton, Mona Tower, Dhaka\n" +
                            "9336301, 8352012");
        }

        void pushpin104_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Salauddin Ash-Shifa General Hospital Ltd.\n" +
                            "Salauddin Bhaban, 44/A, Hatkhola Road, Sutrapur, Dhaka\n" +
                            "Phone: 7168411, 7168422, 7168433, 7114582, 7167974");
        }

        void pushpin105_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Salvation Specialised Hospital & Research Ltd.\n" +
                            "House-36, Road-3, DRA\n" +
                            "Phone: 9674114");
        }

        void pushpin106_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Sheba Drug Abuse Mental Treatment & Rehabilitation Centre\n" +
                            "364, Elephant Road, Dhanmondi, Dhaka\n" +
                            "Phone: 8610257");
        }

        void pushpin107_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Shefa Nursing Home\n" +
                            "H-14/1, Block-B, Babar Road, Mohammadpur\n" +
                            "Phone: 9111758");
        }

        void pushpin108_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("South Asian Hospital Ltd.\n" +
                            "69/E, Green Road, Panthapath, Dhaka\n" +
                            "Phone: 8616565, 8618282, 9665852");
        }

        void pushpin109_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("South View Hospital\n" +
                            "H-1, Rd-11/1, Section-10, Mirpur, Dhaka\n" +
                            "Phone: 8022038, 8018065");
        }

        void pushpin110_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("SPRC & General Hospital\n" +
                            "135, New Eskaton Road\n" +
                            "Phone: 9339089, 9342744, 8313185");
        }

        void pushpin111_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Square Diagnostic & Hospital Services Ltd\n" +
                            "A, K, Complex, Green Road, Dhaka\n" +
                            "Phone: 8616389, 9336367");
        }

        void pushpin112_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Stone Crush Hospital\n" +
                            "H-48, Rd-4/A, DRA\n" +
                            "Phone: 8618388, 503948");
        }

        void pushpin113_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Sumona Clinic General Hospital\n" +
                            "3-4 Patuatuly, Dhaka\n" +
                            "Phone: 7112583, 7115531, 9561786");
        }

        void pushpin114_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("The Barakah General Hospital\n" +
                            "937, Outer Circular Road, Rajarbag, Dhaka\n" +
                            "Phone: 8317765, 9337534, 9346265");
        }

        void pushpin115_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("The Health Hospital & Diagnostic Centre\n" +
                            "103/1, R.M Das Road, Sutrapur, Dhaka");
        }

        void pushpin116_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Udayon Poly Clinic\n" +
                            "16-17/1, New Eskaton, Dhaka\n" +
                            "Phone: 8016300-1, 9351100-1, 9357095");
        }

        void pushpin117_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Vision Eye Hospital\n" +
                            "4/2 Lalmatia, Block-D, Dhaka\n" +
                            "Phone: 8113302, 9119109");
        }

        void pushpin118_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Women & Children's Hospital\n" +
                            "H-48/6, Rd- 9/A, DRA, Dhaka\n" +
                            "Phone: 9115458, 9121077");
        }

        void pushpin119_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Aichi Hospital\n" +
                            "House #13, Isa Kha Avenue, Sector#6, Uttara, Dhaka\n" +
                            "Phone : 02 8916290, 02 8920156");
        }

        void pushpin120_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Anwer Khan Modern Hospital \n" +
                            "House # 17 Road 8, Dhaka 1205\n" +
                            "Phone: 8613883, 8624600, 9661213");
        }

        void pushpin121_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Women & Children's Hospital\n" +
                            "Address: 74G/75, Pea-cock Square, Dhaka 1215\n" +
                            "Phone:02-9122690");
        }

        private void zoomIn_click(object sender, EventArgs e)
        {
            map1.ZoomLevel++;
        }

        private void zoomOut_click(object sender, EventArgs e)
        {
            map1.ZoomLevel--;
        }

        private void Aerial_click(object sender, EventArgs e)
        {
            map1.Mode = new AerialMode(true);
        }

        private void Road_click(object sender, EventArgs e)
        {
            map1.Mode = new RoadMode();
        }

        private void addPin_click(object sender, EventArgs e)
        {
            var task = new Microsoft.Phone.Tasks.BingMapsDirectionsTask();
            task.Start = new Microsoft.Phone.Tasks.LabeledMapLocation("Start", new GeoCoordinate(23.753203, 90.411708));
            task.End = new Microsoft.Phone.Tasks.LabeledMapLocation("End", new GeoCoordinate(23.749039, 90.407818));
            task.Show();
        } 
    }
}