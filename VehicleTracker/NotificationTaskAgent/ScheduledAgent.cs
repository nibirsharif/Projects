#define DEBUG_AGENT

using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;

using System.IO;
using System.IO.IsolatedStorage;
using System;
using System.Device.Location;
using Microsoft.Phone.Shell;
using System.Threading;
using Microsoft.WindowsAzure.MobileServices;

using System.Linq;
using Windows.Devices.Geolocation;

namespace NotificationTaskAgent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <summary>
        /// Azure Mobile Service
        /// </summary>
        public static MobileServiceClient MobileService = new MobileServiceClient(
            "https://vehicletracker.azure-mobile.net/",
            "qdJtftyUlIJPiZvYGeOjwhSonFxZCB26"
        );

        /// <summary>
        /// Azure Data Table
        /// </summary>
        private MobileServiceCollection<Item, Item> items;
        private IMobileServiceTable<Item> itemTable = MobileService.GetTable<Item>();

        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected async override void OnInvoke(ScheduledTask task)
        {
            Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
            try
            {
                items = await itemTable
                    .Where(item => (item.Lat <= (currentPosition.Coordinate.Point.Position.Latitude + 0.054) && item.Lat >= (currentPosition.Coordinate.Point.Position.Latitude - 0.054)
                                    && item.Long <= (currentPosition.Coordinate.Point.Position.Longitude + 0.054) && item.Long >= (currentPosition.Coordinate.Point.Position.Longitude - 0.054)))
                    .ToCollectionAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading items", MessageBoxButton.OK);
            }

            // Update the Iconic Tile
            ShellTile.ActiveTiles.First().Update(new IconicTileData()
            {
                Title = "Vehicle Tracker",
                WideContent1 = items.Last().Message,
                WideContent2 = items.Last().Location,
                WideContent3 = items.Last().Density + " on " + items.Last().Date,
                Count = items.Count()
            });

            // Send a Toast Notification
            ShellToast toast = new ShellToast();
            if (items.Last().Message == "Traffic Jam")
                toast.NavigationUri = new Uri("/Page3.xaml", UriKind.Relative);
            else if (items.Last().Message == "Emergency")
                toast.NavigationUri = new Uri("/Page4.xaml", UriKind.Relative);
            else
                toast.NavigationUri = new Uri("/Page5.xaml", UriKind.Relative);
            toast.Title = "Vehicle Tracker";
            toast.Content = items.Last().Message;
            toast.Show();

#if DEBUG_AGENT
            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(60));
#endif
            NotifyComplete();
        }

        Geolocator geolocator = new Geolocator();
    }
}