using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using TravelPacker.Model;
using TravelPacker.View.Itinerary;
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
namespace TravelPacker.View.Travels
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class TravelListPage : Page
	{
		public TravelsDetailPageViewModel ViewModel { get; set; }

		public TravelListPage()
		{
			this.InitializeComponent();
			this.ViewModel = new TravelsDetailPageViewModel();
			this.DataContext = this.ViewModel;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			Travel travel = (Travel)e.Parameter;

			/*txt_title.Text = travel.Name;
            txt_location.Text = travel.Location;
            txt_image.Text = travel.ImageUrl;*/

			ViewModel.Travel = travel;
		}

		private void onItemChecked(object sender, RoutedEventArgs e)
		{
			var selectedItem = (sender as CheckBox).Content;
			(sender as CheckBox).Foreground.Opacity = 100;
			//TravelTask selectedTask = viewModel.Travel.Tasks.First(elem => elem.Title.Equals(selectedItem));
		}

		private void btn_updateTravel_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(UpdateTravelPage), ViewModel.Travel);
		}

		private void btn_route_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(RoutePage), ViewModel.Travel);
		}

		private void add_itineraryItem_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(AddItineraryPage), ViewModel.Travel);
		}

		private async void GV_itineraryCard_RightTapped(object sender, RightTappedRoutedEventArgs e)
		{
			var selectedItineraryItem = (sender as Grid).DataContext as ItineraryItem;

			if (selectedItineraryItem != null)
			{
				ContentDialog cd = new ContentDialog()
				{
					Title = "Delete itinerary item",
					Content = $"Do you wish to delete itinerary item '{selectedItineraryItem.Title}'? This action cannot be undone.",
					CloseButtonText = "Close",
					PrimaryButtonText = "Delete"
				};

				ContentDialogResult result = await cd.ShowAsync();
				if (result == ContentDialogResult.Primary)
				{

					bool success = await ViewModel.DeleteItineraryItem(selectedItineraryItem);

					if (success)
					{
						// TODO De lijst wordt niet geupdate in de view
						ViewModel.Travel.Itineraries.Remove(selectedItineraryItem);
						//DataContext = ViewModel; WERKT OOK NIET...
					}
					else
					{

						ContentDialog diag = new ContentDialog() { Title = "Delete failed, try again later", CloseButtonText = "Close" };
						diag.ShowAsync();
					}
				}
			}
		}
		private void btn_AddTask_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(AddTaskPage), ViewModel.Travel);
		}

		private async void RemoveCategory_Tapped(object sender, TappedRoutedEventArgs e)
		{
			var selectedCategory = (sender as FontIcon).DataContext as Category;

			if (selectedCategory != null)
			{
				//MessageDialog md = new MessageDialog(selectedCategory.Name);
				//md.ShowAsync();

				await ViewModel.DeleteCategory(selectedCategory);
			}

		}

		private async void RemoveItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			var selectedItem = (sender as FontIcon).DataContext as Item;

			if (selectedItem != null)
			{
				//MessageDialog md = new MessageDialog(selectedItem.Title);
				//md.ShowAsync();

				await ViewModel.DeleteItem(selectedItem);
			}
		}

		private async void AddCategory_Tapped(object sender, TappedRoutedEventArgs e)
		{
			var newCategoryTitle = NewCategoryTitle.Text;

			if (newCategoryTitle != null)
			{
				await ViewModel.addCategory(newCategoryTitle);
				NewCategoryTitle.Text = "";
			}
		}

		private async void AddItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			var categoryID = ((sender as FontIcon).DataContext as Category).Id;
			var newItemTitle = (((sender as FontIcon).Parent as Grid).Children.Where(c => c.GetType().Name == "TextBox").ToList()[0] as TextBox).Text;

			if (newItemTitle != null)
			{
				await ViewModel.addItem(newItemTitle, categoryID);
			}
		}

		private async void CheckItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			var selectedItem = (sender as CheckBox).DataContext as Item;
			var done = (selectedItem.Done) ? false : true;

			if (selectedItem != null)
			{
				//MessageDialog md = new MessageDialog(selectedItem.Done.ToString());
				//md.ShowAsync();

				await ViewModel.updateItem(selectedItem, done);
			}
		}

		private async void btn_DeleteTask_Click(object sender, RoutedEventArgs e)
		{
			var selectedTask = (sender as Button).DataContext as TravelTask;

			if (selectedTask != null)
			{
				ContentDialog cd = new ContentDialog()
				{
					Title = "Delete task",
					Content = $"Do you wish to delete the task '{selectedTask.Title}'? This action cannot be undone.",
					CloseButtonText = "Close",
					PrimaryButtonText = "Delete"
				};

				ContentDialogResult result = await cd.ShowAsync();
				if (result == ContentDialogResult.Primary)
				{

					bool success = await ViewModel.DeleteTask(selectedTask);

					if (success)
					{
						ContentDialog diag = new ContentDialog() { Title = "Delete Successfull", CloseButtonText = "Close" };
						diag.ShowAsync();
						Frame.Navigate(Frame.Content.GetType(), ViewModel.Travel);
						Frame.GoBack();
					}
					else
					{

						ContentDialog diag = new ContentDialog() { Title = "Delete failed, try again later", CloseButtonText = "Close" };
						diag.ShowAsync();
					}
				}
			}
		}
	}
}

