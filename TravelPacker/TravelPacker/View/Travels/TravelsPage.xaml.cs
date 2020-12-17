using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TravelPacker.Model;
using TravelPacker.Util;
using TravelPacker.ViewModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Notifications;
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

namespace TravelPacker.View.Travels {
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
			Frame.Navigate(typeof(AddTravelPage));
		}

		private async void TravelsGV_RightTapped(object sender, RightTappedRoutedEventArgs e) {
			var selectedTravel = (sender as StackPanel).DataContext as Travel;

			if (selectedTravel != null) {
				ContentDialog cd = new ContentDialog() {
					Title = "Delete travel",
					Content = $"Do you wish to delete travel '{selectedTravel.Name}'? This action cannot be undone.",
					CloseButtonText = "Close",
					PrimaryButtonText = "Delete"
				};

				ContentDialogResult result = await cd.ShowAsync();

				if (result == ContentDialogResult.Primary) {

					bool success = await ViewModel.DeleteTravel(selectedTravel);

					if (success) {
						ContentDialog diag = new ContentDialog() { Title = "Delete Successfull", CloseButtonText = "Close" };
						diag.ShowAsync();
					}
					else {

						ContentDialog diag = new ContentDialog() { Title = "Delete failed, try again later", CloseButtonText = "Close" };
						diag.ShowAsync();
					}
				}
			}
		}

		private void TravelsGV_Tapped(object sender, TappedRoutedEventArgs e) {
			var selectedTravel = TravelsGV.SelectedItem as Travel;

			if (selectedTravel != null) {
				Frame.Navigate(typeof(TravelListPage), selectedTravel);
			}
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);

			var result = await ViewModel.GetTravels();

			if (result) { showToast(); }
						

			// TODO secondaryTile aanmaken met eerstvolgende reisroute
		}

		private void showToast() {
			var itineraryItem = GetEarliestItineraryItem();
			var toastTitle = $"[{itineraryItem.Key.Name}] Upcoming itinerary";
			var title = itineraryItem.Value.Title;
			var date = itineraryItem.Value.Start.ToString("dd/MM/yyyy HH:mm");

			var xmdock = CreateToast(toastTitle, title, date);
			var toast = new ToastNotification(xmdock);
			var notifi = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier();
			notifi.Show(toast);
		}

		private static Windows.Data.Xml.Dom.XmlDocument CreateToast(string ToastTitle, string Title, string Date)
		{
            var toastXml = new XDocument(
               new XElement("toast",
               new XElement("visual",
               new XElement("binding", new XAttribute("template", "ToastGeneric"),
               new XElement("text", ToastTitle),
               new XElement("text", Title),
			   new XElement("text", Date)
			))));

            var xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
			xmlDoc.LoadXml(toastXml.ToString());
			return xmlDoc;
		}

		private KeyValuePair<Travel,ItineraryItem> GetEarliestItineraryItem()
        {
			var travel = ViewModel.Travels.FirstOrDefault();
			var itineraryItem = ViewModel.Travels.FirstOrDefault().Itineraries.FirstOrDefault();

            foreach (Travel t in ViewModel.Travels)
            {
				foreach (ItineraryItem i in t.Itineraries)
                {
					if (i.Start < itineraryItem.Start) {
						itineraryItem = i;
						travel = t;
					}
                }
            }

			return new KeyValuePair<Travel, ItineraryItem>(travel, itineraryItem);
        }
	}
}
