using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DMPNewsPortalTest.Resources;
using System.Collections.ObjectModel;
using System.Net.Http;
using HtmlAgilityPack;
using System.Text;

namespace DMPNewsPortalTest
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string latestNewsUrl = "http://www.prothom-alo.com/";
        private ObservableCollection<NewsHeaderData> latestList = new ObservableCollection<NewsHeaderData>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            HtmlWeb web = new HtmlWeb();
            web.LoadCompleted += web_LoadCompleted;
            web.LoadAsync(latestNewsUrl);

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        async void web_LoadCompleted(object sender, HtmlDocumentLoadCompleted e)
        {
            try
            {
                HtmlNodeCollection parent = e.Document.DocumentNode.SelectNodes("//div[@class='p']//h2");

                HtmlDocument document;

                foreach (HtmlNode m in parent)
                {
                    if (m == null)
                        return;

                    document = new HtmlDocument();
                    string htmlString = m.InnerHtml;
                    document.LoadHtml(htmlString);


                    HtmlNodeCollection n = document.DocumentNode.SelectNodes("//a");
                    
                    foreach (HtmlNode node in n)
                    {
                        string url = node.Attributes["href"].Value;           
                        string title = node.InnerText.Trim();

                        NewsHeaderData data = new NewsHeaderData();
                        data.Title = title;
                        data.Url = url;

                        latestList.Add(data);
                    }
                }
                LatestLongListSelector.DataContext = latestList;
            }
            catch (Exception)
            {
                MessageBox.Show("Sorry.\nWebsite is not responding.", "DMP News Portal", MessageBoxButton.OK);
            }
            /*string htmlPage = "";
            using (var client = new HttpClient())
            {
                htmlPage = await client.GetStringAsync("http://www.prothom-alo.com/");
            }

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlPage);

            List<NewsHeaderData> latestList = new List<NewsHeaderData>();
            foreach (var div in htmlDocument.DocumentNode.SelectNodes("//div[starts-with(@class, 'p_d')]"))
            {
                NewsHeaderData newsHeaderData = new NewsHeaderData();
                newsHeaderData.Title = div.SelectSingleNode(".//h2[@class='title']").InnerText.Trim();
                newsHeaderData.Url = div.SelectSingleNode(".//a[@class='more_link']").Attributes["href"].Value; ;
                latestList.Add(newsHeaderData);
            }
            LatestLongListSelector.DataContext = latestList;*/
        }

        private void LongListSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector selector = sender as LongListSelector;

            // verifying our sender is actually a LongListSelector
            if (sender == null)
                return;

            NewsHeaderData newsHeaderData = selector.SelectedItem as NewsHeaderData;
            

            // veryfying our sender is actually SoundData
            if (newsHeaderData == null)
                return;

            selector.SelectedItem = null;

            NavigationService.Navigate(new Uri("/NewsDetails.xaml?title=" + newsHeaderData.Title + "&url=" + newsHeaderData.Url, UriKind.Relative));
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //string htmlPage = "";
            //using (var client = new HttpClient())
            //{
            //    htmlPage = await client.GetStringAsync("http://www.prothom-alo.com/");
            //}

            //HtmlDocument htmlDocument = new HtmlDocument();
            //htmlDocument.LoadHtml(htmlPage);

            //List<NewsHeaderData> latestList = new List<NewsHeaderData>();
            //foreach (var div in htmlDocument.DocumentNode.SelectNodes("//div[starts-with(@class, 'p_d')]"))
            //{
            //    NewsHeaderData newsHeaderData = new NewsHeaderData();
            //    newsHeaderData.Title = div.SelectSingleNode("//h2[@class='title']").InnerText.Trim();
            //    newsHeaderData.Url = div.SelectSingleNode("//a[@class='more_link']").Attributes["href"].Value.ToString();
            //    latestList.Add(newsHeaderData);
            //}
            //LatestLongListSelector.DataContext = latestList;
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