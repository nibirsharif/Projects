﻿#pragma checksum "C:\Users\Nibir\Documents\Visual Studio 2013\Projects\LaunchServiceBD\LaunchServiceBD\Map35.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D9C8051BC1B0A79F76D83596FE67EBAE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using SidebarWP8;
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


namespace LaunchServiceBD {
    
    
    public partial class Map35 : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal SidebarWP8.SidebarControl sidebarControl;
        
        internal Microsoft.Phone.Controls.LongListSelector MenuListGropus;
        
        internal System.Windows.Controls.Grid MapView;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LaunchServiceBD;component/Map35.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.sidebarControl = ((SidebarWP8.SidebarControl)(this.FindName("sidebarControl")));
            this.MenuListGropus = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("MenuListGropus")));
            this.MapView = ((System.Windows.Controls.Grid)(this.FindName("MapView")));
        }
    }
}

