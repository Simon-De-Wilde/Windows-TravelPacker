using System;
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

namespace TravelPacker.View.Itinerary {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AddItineraryPage : Page {

		public AddItineraryPageViewModel ViewModel { get; set; }

		public AddItineraryPage() {
			this.InitializeComponent();
			ViewModel = new AddItineraryPageViewModel();
		}

		private async void Button_Click(object sender, RoutedEventArgs e) {

			errorbox.Children.Clear();

			CheckForm();

			if (!errorbox.Children.Any()) { // no errors
				DateTime start = date.Date.Value.DateTime.Date;
				start = start.AddTicks(time_start.Time.Ticks);

				ItineraryItem success = await ViewModel.AddItineraryToTravel(txt_title.Text, start, Convert.ToInt32(duration.Text));

				if (success != null) {
					ViewModel.Travel.Itineraries.Add(success);
					Frame.GoBack();
				}
				else {
					MessageDialog md = new MessageDialog("Something went wrong, itinerary item was not created. Try again later");
					await md.ShowAsync();
				}
			}

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
			// Check date
			if (date.Date == null) {
				TextBlock dateError = new TextBlock() {
					Text = "Date may not be empty",
					Foreground = new SolidColorBrush(Colors.DarkRed)
				};
				errorbox.Children.Add(dateError);
			}
			//else if (date.Date.Value.Date < DateTime.Today) {
			//	TextBlock dateError = new TextBlock() {
			//		Text = "Date must be in the future",
			//		Foreground = new SolidColorBrush(Colors.DarkRed)
			//	};
			//	errorbox.Children.Add(dateError);
			//}
			// Check start time
			if (time_start.Time.Ticks == -1) {
				TextBlock startError = new TextBlock() {
					Text = "Start time may not be empty",
					Foreground = new SolidColorBrush(Colors.DarkRed)
				};
				errorbox.Children.Add(startError);
			}
			// check DateTime
			if (date.Date != null && time_start.Time.Ticks != -1) {
				DateTime dt = date.Date.Value.DateTime.Date;

				dt = dt.AddTicks(time_start.Time.Ticks);
				if (dt < DateTime.Now) {
					TextBlock dateError = new TextBlock() {
						Text = "Date must be in future",
						Foreground = new SolidColorBrush(Colors.DarkRed)
					};
					errorbox.Children.Add(dateError);
				}

			}
			// Check duration
			if (string.IsNullOrEmpty(duration.Text) || string.IsNullOrWhiteSpace(duration.Text)) {
				TextBlock durationError = new TextBlock() {
					Text = "Duration may not be empty",
					Foreground = new SolidColorBrush(Colors.DarkRed)
				};
				errorbox.Children.Add(durationError);
			}
			else {
				try {
					Convert.ToInt32(duration.Text);
				}
				catch {
					TextBlock durationError = new TextBlock() {
						Text = "Please fill in a number as duration",
						Foreground = new SolidColorBrush(Colors.DarkRed)
					};
					errorbox.Children.Add(durationError);

				}
			}



		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);

			Travel travel = (Travel)e.Parameter;

			ViewModel.Travel = travel;


		}
	}
}
