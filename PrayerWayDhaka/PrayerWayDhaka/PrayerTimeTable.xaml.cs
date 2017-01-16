using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Globalization;
using Microsoft.Phone.Scheduler;
using Coding4Fun.Toolkit.Controls;
using System.Windows.Media;

namespace PrayerWayDhaka
{
    public partial class PrayerTimeTable : PhoneApplicationPage
    {
        public PrayerTimeTable()
        {
            InitializeComponent();

            DateTime now = DateTime.Now;
            dateTextBlock.Text = now.ToString("dddd, MMMM d, yyyy h:mm tt");

            DateTime utc = DateTime.UtcNow;
            Calendar cal = new HijriCalendar();
            int yy = cal.GetYear(utc);
            int mm = cal.GetMonth(utc);
            int dd = cal.GetDayOfMonth(utc);
            string M = "";
            string[] month = { "Muharram", "Safar", "Rabi Al-Awal", "Rabi Al-Akhar", "Jumada Al-Awwal", "Jumada Al-Akhirah", "Rajab", "Shabarn", "Ramadan", "Shawwal", "Dhul-Qadah", "Dhul-Hijjah" };

            for (int i = 0; i < month.Length; i++)
            {
                if (mm == i)
                {
                    M = month[mm - 1].ToString();
                }
            }

            hijriTextBlock.Text = DateTime.Today.DayOfWeek + ", " + dd + " " + M + " " + yy + " Hijri";

            PrayTime p = new PrayTime();
            double lo = 23.7231;
            double la = 90.4086;
            int y = 0, m = 0, d = 0, tz = 0;

            DateTime cc = DateTime.Now;
            y = cc.Year;
            m = cc.Month;
            d = cc.Day;
            tz = TimeZoneInfo.Local.GetUtcOffset(new DateTime(y, m, d)).Hours;
            String[] s;

            p.setCalcMethod(1);
            p.setAsrMethod(0);

            s = p.getDatePrayerTimes(y, m, d, lo, la, tz);
            fajrTextBlock.Text = "\tFajr       " + "          \t\t\t          " + s[0];
            sunriseTextBlock.Text = "\tSunrise    " + "          \t\t          " + s[1];
            dhuhrTextBlock.Text = "\tDhuhr      " + "          \t\t          " + s[2];
            asrTextBlock.Text = "\tAsr        " + "          \t\t\t          " + s[3];
            sunsetTextBlock.Text = "\tSunset     " + "          \t\t          " + s[4];
            maghribTextBlock.Text = "\tMaghrib    " + "          \t\t          " + s[5];
            ishaTextBlock.Text = "\tIsha       " + "          \t\t\t          " + s[6];
        }
        
        private void remindMe_click(object sender, EventArgs e)
        {
            PrayTime p = new PrayTime();
            double lo = 23.7;
            double la = 90.4;
            int y = 0, m = 0, d = 0, tz = 0;

            DateTime cc = DateTime.Now;
            y = cc.Year;
            m = cc.Month;
            d = cc.Day;
            tz = TimeZoneInfo.Local.GetUtcOffset(new DateTime(y, m, d)).Hours;
            String[] s;

            p.setCalcMethod(1);
            p.setAsrMethod(0);

            s = p.getDatePrayerTimes(y, m, d, lo, la, tz);

            int resultFajr = DateTime.Compare(DateTime.Now, DateTime.Parse(s[0]));
            int resultDhuhr = DateTime.Compare(DateTime.Now, DateTime.Parse(s[2]));
            int resultAsr = DateTime.Compare(DateTime.Now, DateTime.Parse(s[3]));
            int resultMaghrib = DateTime.Compare(DateTime.Now, DateTime.Parse(s[5]));
            int resultIsha = DateTime.Compare(DateTime.Now, DateTime.Parse(s[6]));

            string message = "";

            if (resultFajr < 0)
            {
                message = "Reminder set to Fajr.";
            }
            else if (resultDhuhr < 0)
            {
                message = "Reminder set to Dhuhr.";
            }
            else if (resultAsr < 0)
            {
                message = "Reminder set to Asr.";
            }
            else if (resultMaghrib < 0)
            {
                message = "Reminder set to Maghrib.";
            }
            else if (resultIsha < 0)
            {
                message = "Reminder set to Isha.";
            }
            else
            {
                message = "Reminder set to Fajr.";
            }

            var MymsgPrompt = new MessagePrompt
            {
                Title = "Confirmation!",
                Message = message,

                IsAppBarVisible = true,
                IsCancelVisible = true
            };
            MymsgPrompt.Completed += messagePrompt_Completed;
            MymsgPrompt.Background = new SolidColorBrush(Color.FromArgb(240, 70, 130, 180));
            MymsgPrompt.Show();
        }

        private void messagePrompt_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            switch (e.PopUpResult)
            {
                case PopUpResult.Cancelled:
                    break;
                case PopUpResult.NoResponse:
                    break;
                case PopUpResult.Ok:
                    PrayTime p = new PrayTime();
                    double lo = 23.7;
                    double la = 90.4;
                    int y = 0, m = 0, d = 0, tz = 0;

                    DateTime cc = DateTime.Now;
                    y = cc.Year;
                    m = cc.Month;
                    d = cc.Day;
                    tz = TimeZoneInfo.Local.GetUtcOffset(new DateTime(y, m, d)).Hours;
                    String[] s;

                    p.setCalcMethod(1);
                    p.setAsrMethod(0);

                    s = p.getDatePrayerTimes(y, m, d, lo, la, tz);

                    int resultFajr = DateTime.Compare(DateTime.Now, DateTime.Parse(s[0]));
                    int resultDhuhr = DateTime.Compare(DateTime.Now, DateTime.Parse(s[2]));
                    int resultAsr = DateTime.Compare(DateTime.Now, DateTime.Parse(s[3]));
                    int resultMaghrib = DateTime.Compare(DateTime.Now, DateTime.Parse(s[5]));
                    int resultIsha = DateTime.Compare(DateTime.Now, DateTime.Parse(s[6]));

                    try
                    {
                        //if (ScheduledActionService.Find("TimeToPray") != null)
                        //    ScheduledActionService.Remove("TimeToPray");

                        Reminder r = new Reminder("TimeToPray");
                        r.Content = "Stop what you are doing and start praying!";

                        if (ScheduledActionService.Find("TimeToPray") == null)
                        {
                            if (resultFajr < 0)
                            {
                                r.Title = "Time To Pray Fajr";
                                r.BeginTime = DateTime.Parse(s[0]);
                            }
                            else if (resultDhuhr < 0)
                            {
                                r.Title = "Time To Pray Dhuhr";
                                r.BeginTime = DateTime.Parse(s[2]);
                            }
                            else if (resultAsr < 0)
                            {
                                r.Title = "Time To Pray Asr";
                                r.BeginTime = DateTime.Parse(s[3]);
                            }
                            else if (resultMaghrib < 0)
                            {
                                r.Title = "Time To Pray Maghrib";
                                r.BeginTime = DateTime.Parse(s[5]);
                            }
                            else if (resultIsha < 0)
                            {
                                r.Title = "Time To Pray Isha";
                                r.BeginTime = DateTime.Parse(s[6]);
                            }
                            else
                            {
                                r.Title = "Time To Pray Fajr";
                                DateTime tomorrow = DateTime.Today.AddDays(1);
                                DateTime beginTime = tomorrow + DateTime.Parse(s[0]).TimeOfDay;
                                r.BeginTime = beginTime;
                            }
                            r.ExpirationTime = DateTime.Now.AddDays(1);
                            r.NavigationUri = NavigationService.CurrentSource;
                            ScheduledActionService.Add(r);

                        }
                        else
                        {
                            var MymsgPrompt = new MessagePrompt
                            {
                                Title = "Error!",
                                Message = "Reminder has already set."
                            };
                            MymsgPrompt.Background = new SolidColorBrush(Color.FromArgb(240, 70, 130, 180));
                            MymsgPrompt.Show();
                        }
                    }
                    catch
                    {
                        var MymsgPrompt = new MessagePrompt
                        {
                            Title = "Error!",
                            Message = "Try some moment later."
                        };
                        MymsgPrompt.Background = new SolidColorBrush(Color.FromArgb(240, 70, 130, 180));
                        MymsgPrompt.Show();
                    }
                    break;
                case PopUpResult.UserDismissed:
                    break;
                default:
                    break;
            }
        }

        private void remindOff_click(object sender, EventArgs e)
        {
            PrayTime p = new PrayTime();
            double lo = 23.7;
            double la = 90.4;
            int y = 0, m = 0, d = 0, tz = 0;

            DateTime cc = DateTime.Now;
            y = cc.Year;
            m = cc.Month;
            d = cc.Day;
            tz = TimeZoneInfo.Local.GetUtcOffset(new DateTime(y, m, d)).Hours;
            String[] s;

            p.setCalcMethod(1);
            p.setAsrMethod(0);

            s = p.getDatePrayerTimes(y, m, d, lo, la, tz);

            int resultFajr = DateTime.Compare(DateTime.Now, DateTime.Parse(s[0]));
            int resultDhuhr = DateTime.Compare(DateTime.Now, DateTime.Parse(s[2]));
            int resultAsr = DateTime.Compare(DateTime.Now, DateTime.Parse(s[3]));
            int resultMaghrib = DateTime.Compare(DateTime.Now, DateTime.Parse(s[5]));
            int resultIsha = DateTime.Compare(DateTime.Now, DateTime.Parse(s[6]));

            string message = "";

            if (resultFajr < 0)
            {
                message = "Fajr";
            }
            else if (resultDhuhr < 0)
            {
                message = "Dhuhr";
            }
            else if (resultAsr < 0)
            {
                message = "Asr";
            }
            else if (resultMaghrib < 0)
            {
                message = "Maghrib";
            }
            else if (resultIsha < 0)
            {
                message = "Isha";
            }
            else
            {
                message = "Fajr";
            }

            var MymsgPrompt = new MessagePrompt
            {
                Title = "Confirmation!",
                Message = "Do you want to remove " + message + " reminder?",

                IsAppBarVisible = true,
                IsCancelVisible = true
                
            };
            MymsgPrompt.Completed += RemoveMessagePrompt_Completed;
            MymsgPrompt.Background = new SolidColorBrush(Color.FromArgb(240, 70, 130, 180));
            MymsgPrompt.Show();
        }

        private void RemoveMessagePrompt_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            switch (e.PopUpResult)
            {
                case PopUpResult.Cancelled:
                    break;
                case PopUpResult.NoResponse:
                    break;
                case PopUpResult.Ok:
                    if (ScheduledActionService.Find("TimeToPray") != null)
                    {
                        ScheduledActionService.Remove("TimeToPray");
                    }
                    else
                    {
                        var MymsgPrompt = new MessagePrompt
                        {
                            Title = "Error!",
                            Message = "No reminder found."
                        };
                        MymsgPrompt.Background = new SolidColorBrush(Color.FromArgb(240, 70, 130, 180));
                        MymsgPrompt.Show();
                    }
                    break;
                case PopUpResult.UserDismissed:
                    break;
                default:
                    break;
            }
        }
    }
}