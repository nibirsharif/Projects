﻿#pragma checksum "C:\Users\Nibir\Documents\Visual Studio 2012\Projects\PrayerWayDhaka\PrayerWayDhaka\NearestMosque.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "40AA51A8EE1FE19B6961A8508AD0FF63"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace PrayerWayDhaka {
    
    
    public partial class NearestMosque : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.RowDefinition DirectionsTitleRowDefinition;
        
        internal System.Windows.Controls.RowDefinition DirectionsRowDefinition;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Maps.Controls.Map MyMap;
        
        internal System.Windows.Controls.Button ButtonShowRouteOff;
        
        internal System.Windows.Controls.TextBox SearchTextBox;
        
        internal System.Windows.Controls.Grid MapInformation;
        
        internal System.Windows.Controls.Primitives.Popup popupForMarker;
        
        internal System.Windows.Controls.TextBlock popupTextBlock;
        
        internal System.Windows.Controls.Button ButtonRoute;
        
        internal System.Windows.Controls.Button ButtonCancel;
        
        internal System.Windows.Controls.StackPanel RouteDirections;
        
        internal System.Windows.Controls.TextBlock DestinationText;
        
        internal System.Windows.Controls.TextBlock DestinationDetailsText;
        
        internal System.Windows.Controls.Button DriveButton;
        
        internal System.Windows.Controls.Button WalkButton;
        
        internal Microsoft.Phone.Controls.LongListSelector RouteLLS;
        
        internal System.Windows.Controls.Slider PitchSlider;
        
        internal System.Windows.Controls.Slider HeadingSlider;
        
        internal System.Windows.Controls.Grid LocationPanel;
        
        internal System.Windows.Controls.Button AllowButton;
        
        internal System.Windows.Controls.Button CancelButton;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/PrayerWayDhaka;component/NearestMosque.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.DirectionsTitleRowDefinition = ((System.Windows.Controls.RowDefinition)(this.FindName("DirectionsTitleRowDefinition")));
            this.DirectionsRowDefinition = ((System.Windows.Controls.RowDefinition)(this.FindName("DirectionsRowDefinition")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.MyMap = ((Microsoft.Phone.Maps.Controls.Map)(this.FindName("MyMap")));
            this.ButtonShowRouteOff = ((System.Windows.Controls.Button)(this.FindName("ButtonShowRouteOff")));
            this.SearchTextBox = ((System.Windows.Controls.TextBox)(this.FindName("SearchTextBox")));
            this.MapInformation = ((System.Windows.Controls.Grid)(this.FindName("MapInformation")));
            this.popupForMarker = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupForMarker")));
            this.popupTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("popupTextBlock")));
            this.ButtonRoute = ((System.Windows.Controls.Button)(this.FindName("ButtonRoute")));
            this.ButtonCancel = ((System.Windows.Controls.Button)(this.FindName("ButtonCancel")));
            this.RouteDirections = ((System.Windows.Controls.StackPanel)(this.FindName("RouteDirections")));
            this.DestinationText = ((System.Windows.Controls.TextBlock)(this.FindName("DestinationText")));
            this.DestinationDetailsText = ((System.Windows.Controls.TextBlock)(this.FindName("DestinationDetailsText")));
            this.DriveButton = ((System.Windows.Controls.Button)(this.FindName("DriveButton")));
            this.WalkButton = ((System.Windows.Controls.Button)(this.FindName("WalkButton")));
            this.RouteLLS = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("RouteLLS")));
            this.PitchSlider = ((System.Windows.Controls.Slider)(this.FindName("PitchSlider")));
            this.HeadingSlider = ((System.Windows.Controls.Slider)(this.FindName("HeadingSlider")));
            this.LocationPanel = ((System.Windows.Controls.Grid)(this.FindName("LocationPanel")));
            this.AllowButton = ((System.Windows.Controls.Button)(this.FindName("AllowButton")));
            this.CancelButton = ((System.Windows.Controls.Button)(this.FindName("CancelButton")));
        }
    }
}
