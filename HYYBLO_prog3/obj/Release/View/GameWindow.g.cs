﻿#pragma checksum "..\..\..\View\GameWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "782ED0C54CEB34F2E7B4983E132C356E6E2F0E36"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Hyyblo_View {
    
    
    /// <summary>
    /// GameWindow
    /// </summary>
    public partial class GameWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\View\GameWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.UserControl PlayArea;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\View\GameWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu TopBar;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\View\GameWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu BottomBar;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\View\GameWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu LeftBar;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\View\GameWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu RightBar;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\View\GameWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton btnRoad;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\View\GameWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton btnWarehouse;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\View\GameWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton btnDelete;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\View\GameWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblMoney;
        
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
            System.Uri resourceLocater = new System.Uri("/Hyyblo_Prog3;component/view/gamewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\GameWindow.xaml"
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
            
            #line 9 "..\..\..\View\GameWindow.xaml"
            ((Hyyblo_View.GameWindow)(target)).MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.Window_MouseWheel);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PlayArea = ((System.Windows.Controls.UserControl)(target));
            return;
            case 3:
            this.TopBar = ((System.Windows.Controls.Menu)(target));
            
            #line 28 "..\..\..\View\GameWindow.xaml"
            this.TopBar.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ViewMouseEnter);
            
            #line default
            #line hidden
            
            #line 28 "..\..\..\View\GameWindow.xaml"
            this.TopBar.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ViewMouseLeave);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BottomBar = ((System.Windows.Controls.Menu)(target));
            
            #line 29 "..\..\..\View\GameWindow.xaml"
            this.BottomBar.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ViewMouseEnter);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\View\GameWindow.xaml"
            this.BottomBar.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ViewMouseLeave);
            
            #line default
            #line hidden
            return;
            case 5:
            this.LeftBar = ((System.Windows.Controls.Menu)(target));
            
            #line 30 "..\..\..\View\GameWindow.xaml"
            this.LeftBar.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ViewMouseEnter);
            
            #line default
            #line hidden
            
            #line 30 "..\..\..\View\GameWindow.xaml"
            this.LeftBar.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ViewMouseLeave);
            
            #line default
            #line hidden
            return;
            case 6:
            this.RightBar = ((System.Windows.Controls.Menu)(target));
            
            #line 31 "..\..\..\View\GameWindow.xaml"
            this.RightBar.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ViewMouseEnter);
            
            #line default
            #line hidden
            
            #line 31 "..\..\..\View\GameWindow.xaml"
            this.RightBar.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ViewMouseLeave);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnRoad = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 34 "..\..\..\View\GameWindow.xaml"
            this.btnRoad.Click += new System.Windows.RoutedEventHandler(this.BtnRoad_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnWarehouse = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 37 "..\..\..\View\GameWindow.xaml"
            this.btnWarehouse.Click += new System.Windows.RoutedEventHandler(this.BtnWarehouse_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnDelete = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 40 "..\..\..\View\GameWindow.xaml"
            this.btnDelete.Click += new System.Windows.RoutedEventHandler(this.BtnDelete_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lblMoney = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
