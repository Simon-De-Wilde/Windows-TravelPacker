﻿#pragma checksum "C:\Users\Simon\Desktop\NativeApps2-Windows\Windows-TravelPacker\TravelPacker\TravelPacker\View\Itinerary\AddItineraryPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B0E51274BBDBEAA7B4C30FF86EE55E3C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TravelPacker.View.Itinerary
{
    partial class AddItineraryPage : 
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
            case 2: // View\Itinerary\AddItineraryPage.xaml line 12
                {
                    this.bgImage = (global::Windows.UI.Xaml.Media.ImageBrush)(target);
                }
                break;
            case 3: // View\Itinerary\AddItineraryPage.xaml line 63
                {
                    this.errorbox = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 4: // View\Itinerary\AddItineraryPage.xaml line 70
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.Button_Click;
                }
                break;
            case 5: // View\Itinerary\AddItineraryPage.xaml line 59
                {
                    this.duration = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6: // View\Itinerary\AddItineraryPage.xaml line 52
                {
                    this.time_start = (global::Windows.UI.Xaml.Controls.TimePicker)(target);
                }
                break;
            case 7: // View\Itinerary\AddItineraryPage.xaml line 45
                {
                    this.date = (global::Windows.UI.Xaml.Controls.CalendarDatePicker)(target);
                }
                break;
            case 8: // View\Itinerary\AddItineraryPage.xaml line 38
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

