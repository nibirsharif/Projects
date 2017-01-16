using System;
using System.Diagnostics;
using System.Resources;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RastarObsta.Resources;
using System.Windows.Media;

namespace RastarObsta
{
    public partial class App : Application
    {
        public static PhoneApplicationFrame RootFrame { get; private set; }

        public App()
        {
            UnhandledException += Application_UnhandledException;

            InitializeComponent();

            InitializePhoneApplication();

            ThemeManager.SetAccentColor(Color.FromArgb(255, 70, 130, 180));

            InitializeLanguage();

            if (Debugger.IsAttached)
            {
                Application.Current.Host.Settings.EnableFrameRateCounter = true;
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            if ((Visibility) Resources["PhoneLightThemeVisibility"] == Visibility.Visible)
            {
                DarkTheme();
            }
        }

        private void DarkTheme()
        {
            ((SolidColorBrush)Resources["PhoneRadioCheckBoxCheckBrush"]).Color = ((SolidColorBrush)Resources["PhoneRadioCheckBoxBorderBrush"]).Color = ((SolidColorBrush)Resources["PhoneForegroundBrush"]).Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneBackgroundBrush"]).Color = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneContrastForegroundBrush"]).Color = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneContrastBackgroundBrush"]).Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneDisabledBrush"]).Color = Color.FromArgb(0x66, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneProgressBarBackgroundBrush"]).Color = Color.FromArgb(0x19, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneTextCaretBrush"]).Color = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneTextBoxBrush"]).Color = Color.FromArgb(0xBF, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneTextBoxForegroundBrush"]).Color = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneTextBoxEditBackgroundBrush"]).Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneTextBoxReadOnlyBrush"]).Color = Color.FromArgb(0x77, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneSubtleBrush"]).Color = Color.FromArgb(0x99, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneTextBoxSelectionForegroundBrush"]).Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneButtonBasePressedForegroundBrush"]).Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneTextHighContrastBrush"]).Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneTextMidContrastBrush"]).Color = Color.FromArgb(0x99, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneTextLowContrastBrush"]).Color = Color.FromArgb(0x73, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneSemitransparentBrush"]).Color = Color.FromArgb(0xAA, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneChromeBrush"]).Color = Color.FromArgb(0xFF, 0x1F, 0x1F, 0x1F);

            ((SolidColorBrush)Resources["PhoneInactiveBrush"]).Color = Color.FromArgb(0x33, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneInverseInactiveBrush"]).Color = Color.FromArgb(0xFF, 0xCC, 0xCC, 0xCC);
            ((SolidColorBrush)Resources["PhoneInverseBackgroundBrush"]).Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneBorderBrush"]).Color = Color.FromArgb(0xBF, 0xFF, 0xFF, 0xFF);
        }

        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        #region Phone application initialization

        private bool phoneApplicationInitialized = false;

        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            RootFrame = new TransitionFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;
            RootFrame.Navigated += CheckForResetNavigation;
            phoneApplicationInitialized = true;
        }

        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            RootFrame.Navigated -= ClearBackStackAfterReset;

            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            while (RootFrame.RemoveBackEntry() != null)
            {
                ;
            }
        }

        #endregion

        private void InitializeLanguage()
        {
            try
            {
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }
    }
}