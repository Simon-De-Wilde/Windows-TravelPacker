using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Linq;
using TravelPacker.Model;
using TravelPacker.View.Itinerary;
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
	public sealed partial class TravelListPage : Page {
		public TravelsDetailPageViewModel ViewModel { get; set; }
		private int SumItems { get; set; }
		private int SumTasks { get; set; }

		public TravelListPage() {
			InitializeComponent();
			ViewModel = new TravelsDetailPageViewModel();
			DataContext = ViewModel;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);

			Travel travel = (Travel)e.Parameter;

			/*txt_title.Text = travel.Name;
            txt_location.Text = travel.Location;
            txt_image.Text = travel.ImageUrl;*/

			ViewModel.Travel = travel;

			CalculateSums(travel);
			BuildBadge(travel);
			BuildTile(travel);
		}

		private void CalculateSums(Travel travel) {
			SumItems = 0;
			SumTasks = 0;
			foreach (Category c in travel.Categories) {
				SumItems += c.Items.Where(i => !i.Done).Count();
			}
			SumTasks += travel.Tasks.Where(t => !t.Done).Count();
		}

		private void BuildBadge(Travel travel) {

			// build badge
			BadgeTemplateType type = BadgeTemplateType.BadgeNumber;
			Windows.Data.Xml.Dom.XmlDocument xml = BadgeUpdateManager.GetTemplateContent(type);

			// update element
			Windows.Data.Xml.Dom.XmlNodeList elements = xml.GetElementsByTagName("badge");
			Windows.Data.Xml.Dom.XmlElement element = elements[0] as Windows.Data.Xml.Dom.XmlElement;

			int sum = SumItems + SumTasks;

			element.SetAttribute("value", sum.ToString());

			// send to lock screen
			BadgeUpdater updator = BadgeUpdateManager.CreateBadgeUpdaterForApplication();
			BadgeNotification notification = new BadgeNotification(xml);
			updator.Update(notification);
		}

		private void BuildTile(Travel travel) {
			//Tile layout
			TileContent tileContent = new TileContent() {
				Visual = new TileVisual() {
					TileSmall = new TileBinding() {
						Content = new TileBindingContentAdaptive() {
							BackgroundImage = new TileBackgroundImage() {
								Source = travel.ImageUrl
							}
						}
					},
					TileMedium = new TileBinding() {
						Content = new TileBindingContentAdaptive() {
							Children =
							{
								new AdaptiveGroup()
								{
									Children =
									{
										new AdaptiveSubgroup()
										{
											HintWeight = 1,
											Children =
											{
												new AdaptiveText()
												{
													Text = travel.Name
												},
												new AdaptiveText()
												{
													Text = "Items: " + SumItems.ToString(),
													HintStyle = AdaptiveTextStyle.CaptionSubtle
												},
												new AdaptiveText()
												{
													Text = "Tasks: " + SumTasks.ToString(),
													HintStyle = AdaptiveTextStyle.CaptionSubtle
												}
											}
										}
									}
								}
							},
							BackgroundImage = new TileBackgroundImage() {
								Source = travel.ImageUrl
							}
						}
					},
					TileWide = new TileBinding() {
						Content = new TileBindingContentAdaptive() {
							Children =
				{
					new AdaptiveGroup()
					{
						Children =
						{
							new AdaptiveSubgroup()
							{
								HintWeight = 1,
								Children =
								{
									new AdaptiveText()
									{
										Text = travel.Name
									},
									new AdaptiveText()
									{
										Text = "Items: " + SumItems.ToString(),
										HintStyle = AdaptiveTextStyle.CaptionSubtle
									},
									new AdaptiveText()
									{
										Text = "Tasks: " + SumTasks.ToString(),
										HintStyle = AdaptiveTextStyle.CaptionSubtle
									}
								}
							}
						}
					}
				},
							BackgroundImage = new TileBackgroundImage() {
								Source = travel.ImageUrl
							}
						}
					},
					TileLarge = new TileBinding() {
						Content = new TileBindingContentAdaptive() {
							Children =
				{
					new AdaptiveGroup()
					{
						Children =
						{
							new AdaptiveSubgroup()
							{
								HintWeight = 1,
								Children =
								{
									new AdaptiveText()
									{
										Text = travel.Name
									},
									new AdaptiveText()
									{
										Text = "Items: " + SumItems.ToString(),
										HintStyle = AdaptiveTextStyle.CaptionSubtle
									},
									new AdaptiveText()
									{
										Text = "Tasks: " + SumTasks.ToString(),
										HintStyle = AdaptiveTextStyle.CaptionSubtle
									}
								}
							}
						}
					}
				},
							BackgroundImage = new TileBackgroundImage() {
								Source = travel.ImageUrl
							}
						}
					}
				}
			};
			string tileXml = tileContent.GetContent();

			Windows.Data.Xml.Dom.XmlDocument xmldocument = new Windows.Data.Xml.Dom.XmlDocument();
			xmldocument.LoadXml(tileXml);
			TileNotification tileNotification = new TileNotification(xmldocument);
			TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
		}


		private async void OnTaskChecked(object sender, RoutedEventArgs e) {
			TravelTask selectedTask = (sender as CheckBox).DataContext as TravelTask;
			bool done = (selectedTask.Done) ? false : true;

			if (selectedTask != null) {
				await ViewModel.UpdateTask(selectedTask, done);
			}

			CalculateSums(ViewModel.Travel);
			BuildBadge(ViewModel.Travel);
			BuildTile(ViewModel.Travel);
		}

		private void btn_updateTravel_Click(object sender, RoutedEventArgs e) {
			Frame.Navigate(typeof(UpdateTravelPage), ViewModel.Travel);
		}

		private void btn_route_Click(object sender, RoutedEventArgs e) {
			Frame.Navigate(typeof(RoutePage), ViewModel.Travel);
		}

		private void add_itineraryItem_Click(object sender, RoutedEventArgs e) {
			Frame.Navigate(typeof(AddItineraryPage), ViewModel.Travel);
		}

		private async void GV_itineraryCard_RightTapped(object sender, RightTappedRoutedEventArgs e) {
			ItineraryItem selectedItineraryItem = (sender as Grid).DataContext as ItineraryItem;

			if (selectedItineraryItem != null) {
				ContentDialog cd = new ContentDialog() {
					Title = "Delete itinerary item",
					Content = $"Do you wish to delete itinerary item '{selectedItineraryItem.Title}'? This action cannot be undone.",
					CloseButtonText = "Close",
					PrimaryButtonText = "Delete"
				};

				ContentDialogResult result = await cd.ShowAsync();
				if (result == ContentDialogResult.Primary) {

					bool success = await ViewModel.DeleteItineraryItem(selectedItineraryItem);

					if (success) {
						ViewModel.Travel.Itineraries.Remove(selectedItineraryItem);
					}
					else {

						ContentDialog diag = new ContentDialog() { Title = "Delete failed, try again later", CloseButtonText = "Close" };
						diag.ShowAsync();
					}
				}
			}
		}
		private void btn_AddTask_Click(object sender, RoutedEventArgs e) {
			Frame.Navigate(typeof(AddTaskPage), ViewModel.Travel);
		}

		private async void RemoveCategory_Tapped(object sender, RightTappedRoutedEventArgs e) {
			Category selectedCategory = (sender as Expander).DataContext as Category;

			if (selectedCategory != null) {

				ContentDialog cd = new ContentDialog() {
					Title = $"Delete category",
					Content = $"Do you wish to delete category '{selectedCategory.Name}'? All items under this category will be removed as well. This action cannot be undone.",
					CloseButtonText = "Close",
					PrimaryButtonText = "Delete"
				};

				ContentDialogResult result = await cd.ShowAsync();
				if (result == ContentDialogResult.Primary) {
					bool success = await ViewModel.DeleteCategory(selectedCategory);

					if (success) {
						ViewModel.Travel.Categories.Remove(selectedCategory);
					}
					else {
						ContentDialog diag = new ContentDialog() { Title = "Delete failed, try again later", CloseButtonText = "Close" };
						diag.ShowAsync();
					}
				}
			}
		}

		private async void RemoveItem_Tapped(object sender, RightTappedRoutedEventArgs e) {
			Item selectedItem = (sender as Grid).DataContext as Item;
			Category categoryOfItem = ViewModel.GetCategoryOfItem(selectedItem);

			e.Handled = true;

			if (selectedItem != null) {

				ContentDialog cd = new ContentDialog() {
					Title = $"Delete item from '{categoryOfItem.Name}'",
					Content = $"Do you wish to delete item '{selectedItem.Title}'? This action cannot be undone.",
					CloseButtonText = "Close",
					PrimaryButtonText = "Delete"
				};

				ContentDialogResult result = await cd.ShowAsync();
				if (result == ContentDialogResult.Primary) {
					bool success = await ViewModel.DeleteItem(selectedItem);

					if (success) {
						if (selectedItem.Done) { categoryOfItem.ItemsDone--; }
						categoryOfItem.Items.Remove(selectedItem);
					}
					else {
						ContentDialog diag = new ContentDialog() { Title = "Delete failed, try again later", CloseButtonText = "Close" };
						diag.ShowAsync();
					}
				}
			}
		}

		private async void AddCategory_Tapped(object sender, TappedRoutedEventArgs e) {
			string newCategoryTitle = txt_newCategory.Text;

			if (newCategoryTitle != null) {
				await ViewModel.addCategory(newCategoryTitle);
				txt_newCategory.Text = "";
			}
		}

		private async void AddItem_Tapped(object sender, TappedRoutedEventArgs e) {
			int categoryID = ((sender as FontIcon).DataContext as Category).Id;
			TextBox newItemTitle = (((sender as FontIcon).Parent as Grid).Children.Where(c => c.GetType().Name == "TextBox").ToList()[1] as TextBox);
			TextBox newItemAmount = (((sender as FontIcon).Parent as Grid).Children.Where(c => c.GetType().Name == "TextBox").ToList()[0] as TextBox);
			int amount = 1;

			try {
				amount = Int32.Parse(newItemAmount.Text);
			}
			catch (FormatException) {
				amount = 1;
			}

			if (newItemTitle != null && newItemAmount != null) {
				await ViewModel.addItem(newItemTitle.Text, amount, categoryID);
				newItemTitle.Text = "";
				newItemAmount.Text = "1";
			}
		}

		private async void CheckItem_Tapped(object sender, TappedRoutedEventArgs e) {
			Item selectedItem = (sender as CheckBox).DataContext as Item;
			bool done = (selectedItem.Done) ? false : true;

			if (selectedItem != null) {
				await ViewModel.updateItem(selectedItem, done);
			}

			CalculateSums(ViewModel.Travel);
			BuildBadge(ViewModel.Travel);
			BuildTile(ViewModel.Travel);
		}

		private async void btn_DeleteTask_Click(object sender, RightTappedRoutedEventArgs e) {
			TravelTask selectedTask = (sender as StackPanel).DataContext as TravelTask;

			if (selectedTask != null) {
				ContentDialog cd = new ContentDialog() {
					Title = "Delete task",
					Content = $"Do you wish to delete the task '{selectedTask.Title}'? This action cannot be undone.",
					CloseButtonText = "Close",
					PrimaryButtonText = "Delete"
				};

				ContentDialogResult result = await cd.ShowAsync();
				if (result == ContentDialogResult.Primary) {

					bool success = await ViewModel.DeleteTask(selectedTask);

					if (!success) {
						ContentDialog diag = new ContentDialog() { Title = "Delete failed, try again later", CloseButtonText = "Close" };
						diag.ShowAsync();
					}
				}
			}
		}
	}
}

