using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using TravelPacker.Model;
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
	public sealed partial class RegistrationPage : Page {

		public RegistrationPageViewModel RegistrationPageViewModel { get; set; }

		public RegistrationPage() {
			this.InitializeComponent();

			RegistrationPageViewModel = new RegistrationPageViewModel();

		}

		private async void RegisterClicked(object sender, RoutedEventArgs e) {
			errorbox.Children.Clear();

			CheckForm();

			if (!errorbox.Children.Any()) { // no errors
				bool success = await RegistrationPageViewModel.RegisterUser(txt_firstname.Text, txt_lastname.Text, txt_email.Text, txt_password.Password);

				if (success) {

					Frame.Navigate(typeof(MainPage));
				}
				else {
					TextBlock loginError = new TextBlock() {
						Text = "Unable to register, try again later",
						Foreground = new SolidColorBrush(Colors.DarkRed)
					};

					errorbox.Children.Add(loginError);
				}
			}

		}
		private void LoginClicked(object sender, RoutedEventArgs e) {
			errorbox.Children.Clear();
			Frame.Navigate(typeof(LoginPage));
		}

		private void CheckForm() {
			// Check firstname
			if (string.IsNullOrEmpty(txt_firstname.Text) || string.IsNullOrWhiteSpace(txt_firstname.Text)) {
				TextBlock firstnameError = new TextBlock() {
					Text = "Firstname may not be empty",
					Foreground = new SolidColorBrush(Colors.DarkRed)
				};
				errorbox.Children.Add(firstnameError);
			}
			// Check Lastname
			if (string.IsNullOrEmpty(txt_lastname.Text) || string.IsNullOrWhiteSpace(txt_lastname.Text)) {
				TextBlock lastnameError = new TextBlock() {
					Text = "Lastname may not be empty",
					Foreground = new SolidColorBrush(Colors.DarkRed)
				};
				errorbox.Children.Add(lastnameError);
			}
			// Check email
			if (string.IsNullOrEmpty(txt_email.Text) ||
				string.IsNullOrWhiteSpace(txt_email.Text) ||
				!Regex.IsMatch(txt_email.Text, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")) {

				TextBlock emailError = new TextBlock() {
					Text = "Invalid email",
					Foreground = new SolidColorBrush(Colors.DarkRed)
				};
				errorbox.Children.Add(emailError);
			}
			// Check password
			if (string.IsNullOrEmpty(txt_password.Password) ||
				string.IsNullOrWhiteSpace(txt_password.Password) ||
				!Regex.IsMatch(txt_password.Password, "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")) {

				TextBlock passwordError = new TextBlock() {
					Text = "Invalid password",
					Foreground = new SolidColorBrush(Colors.DarkRed)
				};
				errorbox.Children.Add(passwordError);
			}
			// Check confirm password
			if (string.IsNullOrEmpty(txt_passwordConfirm.Password) ||
				string.IsNullOrWhiteSpace(txt_passwordConfirm.Password) ||
				!txt_passwordConfirm.Password.Equals(txt_password.Password)) {

				TextBlock confirmPasswordError = new TextBlock() {
					Text = "Passwords don't match",
					Foreground = new SolidColorBrush(Colors.DarkRed)
				};
				errorbox.Children.Add(confirmPasswordError);
			}

		}

	}
}
