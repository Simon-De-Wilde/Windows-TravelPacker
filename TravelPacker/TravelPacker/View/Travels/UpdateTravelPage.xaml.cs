﻿using System;
using System.Linq;
using TravelPacker.Model;
using TravelPacker.ViewModel;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelPacker.View.Travels {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class UpdateTravelPage : Page {

		public UpdateTravelPageViewModel ViewModel { get; set; }


		public UpdateTravelPage() {
			this.InitializeComponent();

			ViewModel = new UpdateTravelPageViewModel();

		}

		private async void Button_Click(object sender, RoutedEventArgs e) {

			errorbox.Children.Clear();

			CheckForm();

			if (!errorbox.Children.Any()) { // no errors
				bool success = await ViewModel.UpdateTravel(txt_title.Text, txt_location.Text, txt_image.Text);

				if (success) {
					Frame.GoBack();
				}
				else {
					MessageDialog md = new MessageDialog("Something went wrong, travel was not updated. Try again later");
					await md.ShowAsync();
				}
			}
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);

			Travel travel = (Travel)e.Parameter;

			txt_title.Text = travel.Name;
			txt_location.Text = travel.Location;
			txt_image.Text = travel.ImageUrl;

			ViewModel.Travel = travel;

		}

		private void CheckForm() {
			// Check title
			if (string.IsNullOrEmpty(txt_title.Text) || string.IsNullOrWhiteSpace(txt_title.Text)) {
				TextBlock titleError = new TextBlock() {
					Text = "Title may not be empty",
					Foreground = new SolidColorBrush(Colors.DarkRed)
				};
				errorbox.Children.Add(titleError);
			}
			// Check location
			if (string.IsNullOrEmpty(txt_location.Text) || string.IsNullOrWhiteSpace(txt_location.Text)) {
				TextBlock locationError = new TextBlock() {
					Text = "Location may not be empty",
					Foreground = new SolidColorBrush(Colors.DarkRed)
				};
				errorbox.Children.Add(locationError);
			}

		}
	}
}
