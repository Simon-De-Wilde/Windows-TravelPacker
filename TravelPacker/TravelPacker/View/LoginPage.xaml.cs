using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelPacker.Util;
using TravelPacker.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelPacker.View {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class LoginPage : Page {

		public LoginPageViewModel LoginPageViewModel { get; set; }

		public LoginPage() {
			LoginPageViewModel = new LoginPageViewModel();
			this.InitializeComponent();
		}

		private async void LoginClicked(object sender, RoutedEventArgs e) {

			bool success = await LoginPageViewModel.LoginUser(txt_email.Text, txt_password.Password);

			if (success) {
				// TODO remove
				MessageDialog md = new MessageDialog(Globals.LoggedInUserName + " is logged in");
				md.ShowAsync();
				////////


				Frame.Navigate(typeof(MainPage));
			}
			else {
				MessageDialog md = new MessageDialog("Inloggen mislukt");
				md.ShowAsync();
			}

		}

		private void RegisterClicked(object sender, RoutedEventArgs e) {
			// TODO navigeren naar registerpage
		}
	}
}
