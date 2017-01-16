using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Windows.Shapes;

namespace DMPNewsPortalTest
{
    public partial class NewsDetails : PhoneApplicationPage
    {
        private string url = "", title = "", newUrl = "", a="",b="";

        public NewsDetails()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            url = NavigationContext.QueryString["url"];
            title = NavigationContext.QueryString["title"];

            HtmlWeb web = new HtmlWeb();
            web.LoadCompleted += web_LoadCompleted;

            newUrl = "http://www.prothom-alo.com/" + url;
            web.LoadAsync(newUrl);

            //this.launchListGropus.ItemsSource = this.GetLaunchGroups();
            
        }

        public void web_LoadCompleted(object sender, HtmlDocumentLoadCompleted e)
        {
            try
            {
                HtmlNode parent = e.Document.DocumentNode.SelectSingleNode("//div[@itemprop='articleBody']");
                b = e.Document.ToString();
                MessageBox.Show(b);
                foreach (HtmlNode comment in e.Document.DocumentNode.SelectNodes("//comment()"))
                {
                    comment.ParentNode.RemoveChild(comment);
                }

                string s = parent.InnerText;
                string resultString = Regex.Replace(s, @"( |\r?\n)\1+", "$1");
                string pattern = "&nbsp;";
                string replacement = "";

                Regex rgx = new Regex(pattern);
                string result = rgx.Replace(resultString, replacement);

                pattern = "&rsquo;";
                rgx = new Regex(pattern);
                result = rgx.Replace(result, replacement);

                pattern = "&lsquo;";
                rgx = new Regex(pattern);
                result = rgx.Replace(result, replacement);

                pattern = "&ldquo;";
                rgx = new Regex(pattern);
                result = rgx.Replace(result, replacement);

                pattern = "&rdquo;";
                rgx = new Regex(pattern);
                result = rgx.Replace(result, replacement);

                pattern = "&mdash;";
                rgx = new Regex(pattern);
                result = rgx.Replace(result, replacement);

                TextBlock TitleTextBlock = new TextBlock();
                TitleTextBlock.FontWeight = FontWeights.SemiBold;
                TitleTextBlock.VerticalAlignment = VerticalAlignment.Top;
                TitleTextBlock.FontSize = 20;
                TitleTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                TitleTextBlock.Style = (Style)Application.Current.Resources["PhoneTextTitle3Style"];
                TitleTextBlock.TextAlignment = TextAlignment.Left;
                TitleTextBlock.Margin = new Thickness(0, 0, 0, 0);
                TitleTextBlock.TextWrapping = TextWrapping.Wrap;
                TitleTextBlock.Text = title;

                var panel = new StackPanel();

                //panel.Children.Add(TitleTextBlock);

                Line l = new Line();
                l.Stroke = new SolidColorBrush(Colors.Black);
                l.StrokeThickness = 1;

                l.X1 = 0;
                l.X2 = 456;

                l.Y1 = 5;
                l.Y2 = 5;

                //panel.Children.Add(l);

                System.Windows.Shapes.Rectangle r = new System.Windows.Shapes.Rectangle();
                r.Height = 10.0;
                panel.Children.Add(r);

                HtmlDocument document = new HtmlDocument();
                string htmlString = parent.InnerHtml;
                document.LoadHtml(htmlString);

                try
                {
                    HtmlNode node = document.DocumentNode.SelectSingleNode("//img");
                    string photoUrl = node.Attributes["src"].Value;
                    MessageBox.Show(photoUrl);
                    BitmapImage bitmapImage = new BitmapImage(new Uri(photoUrl, UriKind.RelativeOrAbsolute));

                    Image img = new Image();
                    img.Source = bitmapImage;
                    img.MaxHeight = 300;
                    img.MaxWidth = 460;
                    img.Stretch = Stretch.UniformToFill;
                    img.VerticalAlignment = VerticalAlignment.Top;
                    img.HorizontalAlignment = HorizontalAlignment.Left;

                    panel.Children.Add(img);

                    System.Windows.Shapes.Rectangle r1 = new System.Windows.Shapes.Rectangle();
                    r1.Height = 20.0;
                    panel.Children.Add(r1);
                }
                catch { }

                if (result.Trim().Equals(""))
                    result = "No text available!";

                result = result.Trim();


                string[] stringSeparators = new string[] { "\n" };
                string[] splittedString = result.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in splittedString)
                {
                    if (string.IsNullOrEmpty(line.Trim()))
                    {
                        System.Windows.Shapes.Rectangle r2 = new System.Windows.Shapes.Rectangle();
                        r2.Height = 20.0;
                        panel.Children.Add(r2);
                    }
                    else
                    {
                        var richTextBlock = new RichTextBox();

                        richTextBlock.TextWrapping = TextWrapping.Wrap;
                        richTextBlock.TextAlignment = TextAlignment.Left;
                        richTextBlock.Foreground = new SolidColorBrush(Colors.Black); ;
                        richTextBlock.FontSize = 20;

                        Run myRun = new Run();
                        myRun.Text = line;

                        Paragraph myParagraph = new Paragraph();
                        // Add the Run and image to it.
                        myParagraph.Inlines.Add(myRun);
                        richTextBlock.Blocks.Add(myParagraph);
                        panel.Children.Add(richTextBlock);
                    }
                }
                ScrollViewer.Content = panel;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry! Can not fetch data from website!\n" + ex.Message);
            }


            //List<Details> movies = new List<Details>();

            //Details newMovie = new Details();
            ////newMovie.Cover = e.Document.DocumentNode.SelectSingleNode(".//img").Attributes["src"].Value;

            //newMovie.Title = title;
            //newMovie.Summary = e.Document.DocumentNode.SelectSingleNode(".//div[@itemprop='articleBody']").InnerText.Trim();
            //a = e.Document.DocumentNode.SelectSingleNode(".//div[@itemprop='articleBody']").InnerText.Trim();
            
            //foreach (HtmlNode comment in e.Document.DocumentNode.SelectNodes("//comment()"))
            //{
            //    comment.ParentNode.RemoveChild(comment);
            //}
            //MessageBox.Show(this.a);
            //movies.Add(newMovie);
            //lstMovies.ItemsSource = movies;
            //ScrollViewer.Content = movies;
        }




        IEnumerable<Launch> GetLaunchList()
        {
            List<Launch> launchtList = new List<Launch>();
            
            launchtList.Add(new Launch() { Name = title, Route = a});

            return launchtList;
        }

        List<Group<Launch>> GetLaunchGroups()
        {
            IEnumerable<Launch> launchList = GetLaunchList();
            return GetItemGroups(launchList, c => c.Name);
        }

        List<Group<T>> GetItemGroups<T>(IEnumerable<T> itemList, Func<T, string> getKeyFunc)
        {
            IEnumerable<Group<T>> groupList = from item in itemList
                                              group item by getKeyFunc(item) into g
                                              select new Group<T>(g.Key, g);

            return groupList.ToList();
        }

        public class Group<T> : List<T>
        {
            public Group(string route, IEnumerable<T> items)
                : base(items)
            {
                this.Title = route;
            }

            public string Title { get; set; }
        }
    }
}