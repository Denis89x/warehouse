// Updated by XamlIntelliSenseFileGenerator 22.10.2023 13:03:46
#pragma checksum "..\..\..\..\View\Main\MainPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0213F040D813E8B4B452BF019AF4617C2E18502B77E2413E24CE2B5375BE0183"
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


namespace Warehouse.View.Main
{


    /// <summary>
    /// MainPage
    /// </summary>
    public partial class MainPage : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


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


#line 89 "..\..\..\..\View\Main\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Add;

#line default
#line hidden


#line 96 "..\..\..\..\View\Main\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridMenu;

#line default
#line hidden


#line 99 "..\..\..\..\View\Main\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonCloseMenu;

#line default
#line hidden


#line 102 "..\..\..\..\View\Main\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonOpenMenu;

#line default
#line hidden


#line 149 "..\..\..\..\View\Main\MainPage.xaml"
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
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:

#line 11 "..\..\..\..\View\Main\MainPage.xaml"
                    ((Warehouse.View.Main.MainPage)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);

#line default
#line hidden
                    return;
                case 2:
                    this.ClientGrid = ((System.Windows.Controls.DataGrid)(target));
                    return;
                case 3:
                    this.EditClient = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 4:
                    this.DeleteClient = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 5:
                    this.Add = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 6:
                    this.GridMenu = ((System.Windows.Controls.Grid)(target));
                    return;
                case 7:
                    this.ButtonCloseMenu = ((System.Windows.Controls.Button)(target));

#line 99 "..\..\..\..\View\Main\MainPage.xaml"
                    this.ButtonCloseMenu.Click += new System.Windows.RoutedEventHandler(this.ButtonCloseMenu_Click);

#line default
#line hidden
                    return;
                case 8:
                    this.ButtonOpenMenu = ((System.Windows.Controls.Button)(target));

#line 102 "..\..\..\..\View\Main\MainPage.xaml"
                    this.ButtonOpenMenu.Click += new System.Windows.RoutedEventHandler(this.ButtonOpenMenu_Click);

#line default
#line hidden
                    return;
                case 9:

#line 143 "..\..\..\..\View\Main\MainPage.xaml"
                    ((System.Windows.Controls.ListViewItem)(target)).PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ListViewItem_PreviewMouseLeftButtonDown);

#line default
#line hidden
                    return;
                case 10:
                    this.AdminRegistration = ((System.Windows.Controls.ListViewItem)(target));

#line 149 "..\..\..\..\View\Main\MainPage.xaml"
                    this.AdminRegistration.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.AdminRegistration_MouseLeftButtonDown);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.DataGrid ProductTypeGrid;
        internal System.Windows.Controls.MenuItem EditProductType;
        internal System.Windows.Controls.MenuItem DeleteProductType;
        internal System.Windows.Controls.MenuItem AddProductType;
    }
}

