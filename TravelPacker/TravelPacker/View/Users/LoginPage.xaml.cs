using System;
using TravelPacker.ViewModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

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
		}

		private async void LoginClicked(object sender, RoutedEventArgs e) {

			errorbox.Children.Clear();

			bool success = await LoginPageViewModel.LoginUser(txt_email.Text, txt_password.Password);

			if (success) {

				Frame.Navigate(typeof(MainPage));
			}
			else {
				TextBlock loginError = new TextBlock() {
					Text = "Unable to log in with these credentials",
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
