﻿#pragma checksum "..\..\..\..\AppUserControl\ImportIngredientTab\ImportIngredientTab.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "08F2C45E41BC313AE2FC22546F4CD115"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using QuanLyBanHangClient.AppUserControl.ImportIngredientTab;
using QuanLyBanHangClient.AppUserControl.ImportIngredientTab.ImportDetailTab;
using QuanLyBanHangClient.AppUserControl.ImportIngredientTab.ImportHistoryTab;
using QuanLyBanHangClient.AppUserControl.ImportIngredientTab.ImportTab;
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


namespace QuanLyBanHangClient.AppUserControl.ImportIngredientTab {
    
    
    /// <summary>
    /// ImportIngredientTab
    /// </summary>
    public partial class ImportIngredientTab : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\..\AppUserControl\ImportIngredientTab\ImportIngredientTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl TabControlMain;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\AppUserControl\ImportIngredientTab\ImportIngredientTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem TabImportIngredient;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\AppUserControl\ImportIngredientTab\ImportIngredientTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem TabHistory;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\AppUserControl\ImportIngredientTab\ImportIngredientTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem TabDetail;
        
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
            System.Uri resourceLocater = new System.Uri("/QuanLyBanHangClient;component/appusercontrol/importingredienttab/importingredien" +
                    "ttab.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\AppUserControl\ImportIngredientTab\ImportIngredientTab.xaml"
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
            this.TabControlMain = ((System.Windows.Controls.TabControl)(target));
            
            #line 25 "..\..\..\..\AppUserControl\ImportIngredientTab\ImportIngredientTab.xaml"
            this.TabControlMain.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TabControlMain_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TabImportIngredient = ((System.Windows.Controls.TabItem)(target));
            return;
            case 3:
            this.TabHistory = ((System.Windows.Controls.TabItem)(target));
            return;
            case 4:
            this.TabDetail = ((System.Windows.Controls.TabItem)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

