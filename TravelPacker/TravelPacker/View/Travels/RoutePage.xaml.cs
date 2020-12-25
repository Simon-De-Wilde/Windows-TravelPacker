using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using TravelPacker.Model;
using TravelPacker.ViewModel;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
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
	public sealed partial class RoutePage : Page {
		public RoutePageViewModel ViewModel;
		public new bool Loading { get; set; }

		public RoutePage() {
			InitializeComponent();
			ViewModel = new RoutePageViewModel();
			DataContext = ViewModel;
			Loading = true;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);

			try {
				Travel travel = (Travel)e.Parameter;

				if (travel == null) {
					throw new Exception();
				}

				ViewModel.Travel = travel;
				ShowRouteOnMap();
			}
			catch {
				MessageDialog md = new MessageDialog("Something went wrong. Try again later");
				md.ShowAsync();

				Frame.Navigate(typeof(TravelsPage));
			}
		}

		private async void ShowRouteOnMap() {
			double latitude = 0;
			double longitude = 0;
			try {
				if (Loading) {
					sp.Visibility = Visibility.Collapsed;
				}
				var geoLocator = new Geolocator();
				geoLocator.DesiredAccuracy = PositionAccuracy.High;
				var pos = await geoLocator.GetGeopositionAsync();
				latitude = pos.Coordinate.Point.Position.Latitude;
				longitude = pos.Coordinate.Point.Position.Longitude;
				// The address or business to geocode.
				string addressToGeocode = ViewModel.Travel.Location;

				// The nearby location to use as a query hint.
				BasicGeoposition queryHint = new BasicGeoposition();
				queryHint.Latitude = 50.8505;
				queryHint.Longitude = 4.3488;
				Geopoint hintPoint = new Geopoint(queryHint);

				BasicGeoposition startLocation = new BasicGeoposition() { Latitude = latitude, Longitude = longitude };
				BasicGeoposition endLocation;

				// Reverse geocode the specified geographic location.
				MapLocationFinderResult userLocation =
					  await MapLocationFinder.FindLocationsAtAsync(new Geopoint(startLocation));

				// If the query returns results, display the name of the town
				// contained in the address of the first result.
				if (userLocation.Status == MapLocationFinderStatus.Success) {
					locationBlock.Text = userLocation.Locations[0].Address.Town;
				}

				// Geocode the specified address, using the specified reference point
				// as a query hint. Return no more than 3 results.
				MapLocationFinderResult result =
					  await MapLocationFinder.FindLocationsAsync(
										addressToGeocode,
										hintPoint,
										3);

				if (result.Status == MapLocationFinderStatus.Success) {
					endLocation = new BasicGeoposition() { Latitude = result.Locations[0].Point.Position.Latitude, Longitude = result.Locations[0].Point.Position.Longitude };
				}
				else {
					endLocation = new BasicGeoposition() { Latitude = 47.604, Longitude = -122.329 };
				}
				MapRouteFinderResult routeResult =
					  await MapRouteFinder.GetDrivingRouteAsync(
					  new Geopoint(startLocation),
					  new Geopoint(endLocation),
					  MapRouteOptimization.Time,
					  MapRouteRestrictions.None);

				if (routeResult.Status == MapRouteFinderStatus.Success) {
					// Use the route to initialize a MapRouteView.
					MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
					viewOfRoute.RouteColor = Colors.Yellow;
					viewOfRoute.OutlineColor = Colors.Black;

					// Add the new MapRouteView to the Routes collection
					// of the MapControl.
					MapWithRoute.Routes.Add(viewOfRoute);

					// Fit the MapControl to the route.
					await MapWithRoute.TrySetViewBoundsAsync(
						  routeResult.Route.BoundingBox,
						  null,
						  Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
				}
			}
			catch {
				ContentDialog contentDialog = new ContentDialog() {
					Title = "Make sure the location services for the app are enabled and a valid location is given.",
					CloseButtonText = "Close"
				};
				contentDialog.ShowAsync();
				contentDialog.CloseButtonClick += OnCloseButtonClicked;
			}
			finally {
				this.Loading = false;
				sp.Visibility = Visibility.Visible;
				pBar.Visibility = Visibility.Collapsed;
			}
		}

		public void OnCloseButtonClicked(object sender, ContentDialogButtonClickEventArgs e) {
			Frame.GoBack();
		}
	}
}
