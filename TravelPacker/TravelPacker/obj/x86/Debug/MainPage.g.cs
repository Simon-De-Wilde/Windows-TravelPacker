﻿#pragma checksum "C:\Users\Simon\Desktop\NativeApps2-Windows\Windows-TravelPacker\TravelPacker\TravelPacker\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6FF3705E50A4C686484697A29BDA2B70"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TravelPacker
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainPage.xaml line 11
                {
                    this.navigation = (global::Windows.UI.Xaml.Controls.NavigationView)(target);
                }
                break;
            case 3: // MainPage.xaml line 15
                {
                    global::Windows.UI.Xaml.Controls.NavigationViewItem element3 = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationViewItem)element3).Tapped += this.Home_Tapped;
                }
                break;
            case 4: // MainPage.xaml line 22
                {
                    global::Windows.UI.Xaml.Controls.NavigationViewItem element4 = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationViewItem)element4).Tapped += this.Logout_Tapped;
                }
                break;
            case 5: // MainPage.xaml line 28
                {
                    this.mainframe = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

