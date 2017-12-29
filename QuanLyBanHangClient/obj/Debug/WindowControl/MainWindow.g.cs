﻿#pragma checksum "..\..\..\WindowControl\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F829F9A5E81BF1B7C3606AF9C8932F70"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LoadingPanelSample.Controls;
using QuanLyBanHangClient;
using QuanLyBanHangClient.AppUserControl.FoodTab;
using QuanLyBanHangClient.AppUserControl.ImportIngredientTab;
using QuanLyBanHangClient.AppUserControl.IngredientTab;
using QuanLyBanHangClient.AppUserControl.OrderTab;
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


namespace QuanLyBanHangClient {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\WindowControl\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl TabControlMain;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\WindowControl\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem TabItemOrder;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\WindowControl\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem TabItemFood;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\WindowControl\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem TabItemIngredient;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\WindowControl\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem TabItemRespository;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\WindowControl\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid loadingAnim;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\WindowControl\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LoadingPanelSample.Controls.CircularProgressBar progressBar;
        
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
            System.Uri resourceLocater = new System.Uri("/QuanLyBanHangClient;component/windowcontrol/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WindowControl\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.TabControlMain = ((System.Windows.Controls.TabControl)(target));
            
            #line 27 "..\..\..\WindowControl\MainWindow.xaml"
            this.TabControlMain.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TabControlMain_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TabItemOrder = ((System.Windows.Controls.TabItem)(target));
            return;
            case 3:
            this.TabItemFood = ((System.Windows.Controls.TabItem)(target));
            return;
            case 4:
            this.TabItemIngredient = ((System.Windows.Controls.TabItem)(target));
            return;
            case 5:
            this.TabItemRespository = ((System.Windows.Controls.TabItem)(target));
            return;
            case 6:
            this.loadingAnim = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.progressBar = ((LoadingPanelSample.Controls.CircularProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

