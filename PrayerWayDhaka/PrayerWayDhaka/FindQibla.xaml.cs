using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;

namespace PrayerWayDhaka
{
    public partial class FindQibla : PhoneApplicationPage
    {
        Compass compass;
        // Constructor
        public FindQibla()
        {
            InitializeComponent();

            if (Compass.IsSupported)
            {
                compass = new Compass();
                compass.TimeBetweenUpdates = TimeSpan.FromSeconds(1);
                compass.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<CompassReading>>(compass_CurrentValueChanged);
                compass.Start();
            }
            else
            {
                MessageBox.Show("Device doesn't support compass");
            }
        }

        void compass_CurrentValueChanged(object sender, SensorReadingEventArgs<CompassReading> e)
        {
            Dispatcher.BeginInvoke(() => UpdateLine(e.SensorReading));
        }

        private void UpdateLine(CompassReading e)
        {
            degreeText.Text = (e.MagneticHeading).ToString("0");

            line.X2 = line.X1 - (200 * Math.Sin(MathHelper.ToRadians((float)(e.MagneticHeading+90))));
            line.Y2 = line.Y1 - (200 * Math.Cos(MathHelper.ToRadians((float)(e.MagneticHeading+90))));
        }
    }
}