﻿#pragma checksum "C:\Users\Simon\Desktop\NativeApps2-Windows\Windows-TravelPacker\TravelPacker\TravelPacker\View\AddTravelPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "221EE9EDAE488710597EF0C1B305F4A5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TravelPacker.View
{
    partial class AddTravelPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // View\AddTravelPage.xaml line 11
                {
                    this.bgImage = (global::Windows.UI.Xaml.Media.ImageBrush)(target);
                }
                break;
            case 3: // View\AddTravelPage.xaml line 52
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Click += this.Button_Click;
                }
                break;
            case 4: // View\AddTravelPage.xaml line 49
                {
                    this.txt_image = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // View\AddTravelPage.xaml line 42
                {
                    this.txt_location = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6: // View\AddTravelPage.xaml line 36
                {
                    this.txt_title = (global::Windows.UI.Xaml.Controls.TextBox)(target);
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

