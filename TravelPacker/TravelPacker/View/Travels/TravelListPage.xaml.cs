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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelPacker.View.Travels
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TravelListPage : Page
    {
        public TravelsDetailPageViewModel viewModel;
        private CancellationTokenSource _cts;

        public TravelListPage()
        {
            this.InitializeComponent();
            this.viewModel = new TravelsDetailPageViewModel();
            this.DataContext = this.viewModel;
            ShowRouteOnMap();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Travel travel = (Travel)e.Parameter;

            /*txt_title.Text = travel.Name;
            txt_location.Text = travel.Location;
            txt_image.Text = travel.ImageUrl;*/

            viewModel.Travel = travel;
        }

        private void onItemChecked(object sender, RoutedEventArgs e)
        {
            var selectedItem = (sender as CheckBox).Content;
            (sender as CheckBox).Foreground.Opacity = 100;
           // TravelTask selectedTask = viewModel.Travel.Tasks.First(elem => elem.Title.Equals(selectedItem));
        }

        private async void ShowRouteOnMap()
        {
            // The address or business to geocode.
            string addressToGeocode = "Paris";

            // The nearby location to use as a query hint.
            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = 50.8505;
            queryHint.Longitude = 4.3488;
            Geopoint hintPoint = new Geopoint(queryHint);

            BasicGeoposition startLocation = new BasicGeoposition() { Latitude = 50.8505, Longitude = 4.3488 };
            BasicGeoposition endLocation;

            // Geocode the specified address, using the specified reference point
            // as a query hint. Return no more than 3 results.
            MapLocationFinderResult result =
                  await MapLocationFinder.FindLocationsAsync(
                                    addressToGeocode,
                                    hintPoint,
                                    3);

            // If the query returns results, display the coordinates
            // of the first result.
            if (result.Status == MapLocationFinderStatus.Success)
            {
                // End at the city of Seattle, Washington.
                endLocation = new BasicGeoposition() { Latitude = result.Locations[0].Point.Position.Latitude, Longitude = result.Locations[0].Point.Position.Longitude };
            }
            else
            {
                endLocation = new BasicGeoposition() { Latitude = 47.604, Longitude = -122.329 };
            }


            // Get the route between the points.
            MapRouteFinderResult routeResult =
                  await MapRouteFinder.GetDrivingRouteAsync(
                  new Geopoint(startLocation),
                  new Geopoint(endLocation),
                  MapRouteOptimization.Time,
                  MapRouteRestrictions.None);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
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
    }
}
