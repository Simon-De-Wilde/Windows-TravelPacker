using TravelPacker.Util;
using TravelPacker.View.Travels;
using TravelPacker.View.Users;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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
			Globals.BearerToken = null;

			ContentDialog cd = new ContentDialog() { Title = "Logged out successfully", CloseButtonText = "Close" };
			cd.ShowAsync();

			Frame.Navigate(typeof(LoginPage));
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
