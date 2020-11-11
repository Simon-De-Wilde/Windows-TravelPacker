using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelPacker.View;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TravelPacker {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page {
		public MainPage() {
			this.InitializeComponent();

			mainframe.Navigate(typeof(TravelsPage));

		}

		private void Home_Tapped(object sender, TappedRoutedEventArgs e) {
			mainframe.Navigate(typeof(TravelsPage));
		}

		private void Logout_Tapped(object sender, TappedRoutedEventArgs e) {
			ContentDialog cd = new ContentDialog() { Title = "Logout tapped", CloseButtonText = "Close" };
			cd.ShowAsync();
		}

		private void navigation_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) {

			// check if travelsPage --> no return needed
			if (mainframe.CurrentSourcePageType == typeof(TravelsPage)) {
				mainframe.BackStack.Clear();
			}

			if (mainframe.CanGoBack) {
				mainframe.GoBack();

			}
		}
	}
}
