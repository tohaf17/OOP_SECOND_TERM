﻿#pragma checksum "..\..\..\Fifth.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2336D1C10301ABA6EA329E00D6376119B1283954"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Laba1;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Laba1 {
    
    
    /// <summary>
    /// Fifth
    /// </summary>
    public partial class Fifth : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\Fifth.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button super;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Fifth.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox first;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Fifth.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox second;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Fifth.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox third;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Laba1;component/fifth.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Fifth.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\..\Fifth.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.trans_click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 12 "..\..\..\Fifth.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.back_click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 13 "..\..\..\Fifth.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.hello_click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.super = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\Fifth.xaml"
            this.super.Click += new System.Windows.RoutedEventHandler(this.super_click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.first = ((System.Windows.Controls.CheckBox)(target));
            
            #line 21 "..\..\..\Fifth.xaml"
            this.first.Checked += new System.Windows.RoutedEventHandler(this.checkbox_changed);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\Fifth.xaml"
            this.first.Unchecked += new System.Windows.RoutedEventHandler(this.checkbox_changed);
            
            #line default
            #line hidden
            return;
            case 6:
            this.second = ((System.Windows.Controls.CheckBox)(target));
            
            #line 24 "..\..\..\Fifth.xaml"
            this.second.Checked += new System.Windows.RoutedEventHandler(this.checkbox_changed);
            
            #line default
            #line hidden
            
            #line 24 "..\..\..\Fifth.xaml"
            this.second.Unchecked += new System.Windows.RoutedEventHandler(this.checkbox_changed);
            
            #line default
            #line hidden
            return;
            case 7:
            this.third = ((System.Windows.Controls.CheckBox)(target));
            
            #line 27 "..\..\..\Fifth.xaml"
            this.third.Checked += new System.Windows.RoutedEventHandler(this.checkbox_changed);
            
            #line default
            #line hidden
            
            #line 27 "..\..\..\Fifth.xaml"
            this.third.Unchecked += new System.Windows.RoutedEventHandler(this.checkbox_changed);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

