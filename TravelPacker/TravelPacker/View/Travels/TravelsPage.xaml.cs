﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TravelPacker.Model;
using TravelPacker.ViewModel;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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

			DataContext = ViewModel;
		}

		private void Add_Travel_Btn(object sender, RoutedEventArgs e) {
			Frame.Navigate(typeof(AddTravelPage));
		}

		private async void TravelsGV_RightTapped(object sender, RightTappedRoutedEventArgs e) {
			Travel selectedTravel = (sender as StackPanel).DataContext as Travel;

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
			Travel selectedTravel = TravelsGV.SelectedItem as Travel;

			if (selectedTravel != null) {
				Frame.Navigate(typeof(TravelListPage), selectedTravel);
			}
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);

			bool result = await ViewModel.GetTravels();

			if (result) {
				KeyValuePair<Travel, ItineraryItem> itineraryItem = GetEarliestItineraryItem();
				if (itineraryItem.Value != null) {
					if (itineraryItem.Value.Done == false) {
						showToast(itineraryItem);
					}
				}
			}

		}

		private void showToast(KeyValuePair<Travel, ItineraryItem> itineraryItem) {
			string toastTitle = $"[{itineraryItem.Key.Name}]\nUpcoming itinerary: {itineraryItem.Value.Title}";

			TimeSpan timeUntillItinerary = itineraryItem.Value.Start - DateTime.Now;
			string time = $"Starts in {timeUntillItinerary.Days} day(s) {timeUntillItinerary.Hours} hour(s) {timeUntillItinerary.Minutes} minute(s)";

			Windows.Data.Xml.Dom.XmlDocument xml = CreateToast(toastTitle, time);
			ToastNotification toast = new ToastNotification(xml);

			try {
				ToastNotifier notifi = ToastNotificationManager.CreateToastNotifier();
				notifi.Show(toast);
			}
			catch { }


		}

		private static Windows.Data.Xml.Dom.XmlDocument CreateToast(string ToastTitle, string Time) {
			XDocument toastXml = new XDocument(
			   new XElement("toast",
			   new XElement("visual",
			   new XElement("binding", new XAttribute("template", "ToastGeneric"),
			   new XElement("text", ToastTitle),
			   new XElement("text", Time)
			))));

			Windows.Data.Xml.Dom.XmlDocument xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
			xmlDoc.LoadXml(toastXml.ToString());
			return xmlDoc;
		}

		private KeyValuePair<Travel, ItineraryItem> GetEarliestItineraryItem() {
			Travel travel = ViewModel.Travels.FirstOrDefault();

			if (travel == null) {
				return new KeyValuePair<Travel, ItineraryItem>(null, null);
			}

			ItineraryItem itineraryItem = ViewModel.Travels.FirstOrDefault().Itineraries.FirstOrDefault();

			if (itineraryItem == null) {
				return new KeyValuePair<Travel, ItineraryItem>(null, null);
			}

			double timeBetween = (itineraryItem.Start - DateTime.Now).TotalMinutes;

			foreach (Travel t in ViewModel.Travels) {
				foreach (ItineraryItem i in t.Itineraries) {
					double minutesUntillItiniraryStart = (i.Start - DateTime.Now).TotalMinutes;

					// Itinerary in the future
					if (minutesUntillItiniraryStart > 0) {

						// Itinerary in the future as new default
						if (timeBetween < 0) {
							itineraryItem = i;
							travel = t;
							timeBetween = minutesUntillItiniraryStart;
						}

						// Compare
						if (minutesUntillItiniraryStart < timeBetween) {
							itineraryItem = i;
							travel = t;
							timeBetween = minutesUntillItiniraryStart;
						}

					}

				}
			}

			return new KeyValuePair<Travel, ItineraryItem>(travel, itineraryItem);
		}
	}
}
