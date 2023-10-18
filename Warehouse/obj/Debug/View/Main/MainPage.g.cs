﻿#pragma checksum "..\..\..\..\View\Main\MainPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "04EEDF92EFE83CB8F008B8BFC1528B9EFA7E35F7908697F59FB67A0483971041"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Warehouse.View.Main;


namespace Warehouse.View.Main {
    
    
    /// <summary>
    /// MainPage
    /// </summary>
    public partial class MainPage : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 62 "..\..\..\..\View\Main\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ClientGrid;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\View\Main\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem EditClient;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\View\Main\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem DeleteClient;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\View\Main\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridMenu;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\View\Main\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonCloseMenu;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\..\View\Main\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonOpenMenu;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\..\View\Main\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListViewItem AdminRegistration;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Warehouse;component/view/main/mainpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Main\MainPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ClientGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.EditClient = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 3:
            this.DeleteClient = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 4:
            this.GridMenu = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.ButtonCloseMenu = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\..\..\View\Main\MainPage.xaml"
            this.ButtonCloseMenu.Click += new System.Windows.RoutedEventHandler(this.ButtonCloseMenu_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ButtonOpenMenu = ((System.Windows.Controls.Button)(target));
            
            #line 99 "..\..\..\..\View\Main\MainPage.xaml"
            this.ButtonOpenMenu.Click += new System.Windows.RoutedEventHandler(this.ButtonOpenMenu_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.AdminRegistration = ((System.Windows.Controls.ListViewItem)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

