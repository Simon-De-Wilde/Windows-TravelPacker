using System;
using TravelPacker.Model;
using TravelPacker.ViewModel;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelPacker.View.Travels {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class RoutePage : Page {
		public RoutePageViewModel viewModel;
		public new bool Loading { get; set; }

		public RoutePage() {
			this.InitializeComponent();
			viewModel = new RoutePageViewModel();
			DataContext = viewModel;
			Loading = true;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);

			Travel travel = (Travel)e.Parameter;

			/*txt_title.Text = travel.Name;
            txt_location.Text = travel.Location;
            txt_image.Text = travel.ImageUrl;*/

			viewModel.Travel = travel;
			ShowRouteOnMap();
		}

		private async void ShowRouteOnMap() {
			double latitude = 0;
			double longitude = 0;
			try {
				if (Loading) {
					sp.Visibility = Visibility.Collapsed;
				}
				Geolocator geoLocator = new Geolocator {
					DesiredAccuracy = PositionAccuracy.High
				};
				Geoposition pos = await geoLocator.GetGeopositionAsync();
				latitude = pos.Coordinate.Point.Position.Latitude;
				longitude = pos.Coordinate.Point.Position.Longitude;
				// The address or business to geocode.
				string addressToGeocode = viewModel.Travel.Location;

				// The nearby location to use as a query hint.
				BasicGeoposition queryHint = new BasicGeoposition {
					Latitude = 50.8505,
					Longitude = 4.3488
				};
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
					MapRouteView viewOfRoute = new MapRouteView(routeResult.Route) {
						RouteColor = Colors.Yellow,
						OutlineColor = Colors.Black
					};

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
				Loading = false;
				sp.Visibility = Visibility.Visible;
				pBar.Visibility = Visibility.Collapsed;
			}
		}

		public void OnCloseButtonClicked(object sender, ContentDialogButtonClickEventArgs e) {
			Frame.GoBack();
		}
	}
}
