using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelPacker.Model;
using TravelPacker.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelPacker.View.Travels {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class TravelListPage : Page {
		public TravelsDetailPageViewModel viewModel;

		public TravelListPage() {
			this.InitializeComponent();
			this.viewModel = new TravelsDetailPageViewModel();
			this.DataContext = this.viewModel;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);

			Travel travel = (Travel)e.Parameter;

			/*txt_title.Text = travel.Name;
            txt_location.Text = travel.Location;
            txt_image.Text = travel.ImageUrl;*/

			viewModel.Travel = travel;
		}

		private void onItemChecked(object sender, RoutedEventArgs e) {
			var selectedItem = (sender as CheckBox).Content;
			(sender as CheckBox).Foreground.Opacity = 100;
			//TravelTask selectedTask = viewModel.Travel.Tasks.First(elem => elem.Title.Equals(selectedItem));
		}

		private void btn_updateTravel_Click(object sender, RoutedEventArgs e) {
			Frame.Navigate(typeof(UpdateTravelPage), viewModel.Travel);
		}
	}
}
