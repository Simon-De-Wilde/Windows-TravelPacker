using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelPacker.Util;
using TravelPacker.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelPacker.View.Users {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class LoginPage : Page {

		public LoginPageViewModel LoginPageViewModel { get; set; }

		public LoginPage() {
			LoginPageViewModel = new LoginPageViewModel();
			this.InitializeComponent();

			// TODO REMOVE
			txt_email.Text = "student@hogent.be";
			txt_password.Password = "Root1234";
		}

		private async void LoginClicked(object sender, RoutedEventArgs e) {

			errorbox.Children.Clear();

			bool success = await LoginPageViewModel.LoginUser(txt_email.Text, txt_password.Password);

			if (success) {

				Frame.Navigate(typeof(MainPage));
			}
			else {
				TextBlock loginError = new TextBlock() {
					Text = "Unable to login with these credentials",
					Foreground = new SolidColorBrush(Colors.DarkRed)
				};

				errorbox.Children.Add(loginError);
			}

		}

		private void RegisterClicked(object sender, RoutedEventArgs e) {
			errorbox.Children.Clear();
			Frame.Navigate(typeof(RegistrationPage));
		}
	}
}
