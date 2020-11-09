using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TravelPacker.Model;
using TravelPacker.ViewModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
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
	public sealed partial class TravelsPage : Page {

		public TravelsPageViewModel ViewModel { get; set; }

		public TravelsPage() {
			this.InitializeComponent();


			ViewModel = new TravelsPageViewModel();

			this.DataContext = ViewModel;
		}

		private void Add_Travel_Btn(object sender, RoutedEventArgs e) {
			// TODO viewmodel aanspreken om een travel toe te voegen
			// TODO deze frame wordt niet op de stack van de navigationview geplaatst
			Frame.Navigate(typeof(AddTravelPage));
		}

		private void TravelsGV_RightTapped(object sender, RightTappedRoutedEventArgs e) {
			var selectedTravel = TravelsGV.SelectedItem as Travel;

			// TODO open update screen met optie om te deleten


		}

		private void TravelsGV_Tapped(object sender, TappedRoutedEventArgs e) {
			var selectedTravel = TravelsGV.SelectedItem as Travel;

			MessageDialog md = new MessageDialog(selectedTravel.Name);
			md.ShowAsync();

			// TODO routen naar detailscherm
		}

	}
}
