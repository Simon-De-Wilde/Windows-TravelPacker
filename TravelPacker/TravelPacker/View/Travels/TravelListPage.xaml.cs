using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelPacker.Model;
using TravelPacker.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        public TravelListPage()
        {
            this.InitializeComponent();
            this.viewModel = new TravelsDetailPageViewModel();
            this.DataContext = this.viewModel;
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
            (sender as CheckBox).Foreground.Opacity = 1;
            // TravelTask selectedTask = viewModel.Travel.Tasks.First(elem => elem.Title.Equals(selectedItem));
        }

        private async void RemoveCategory_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedCategory = (sender as FontIcon).DataContext as Category;

            if (selectedCategory != null)
            {
                //MessageDialog md = new MessageDialog(selectedCategory.Name);
                //md.ShowAsync();
                
                await viewModel.DeleteCategory(selectedCategory);
            }

        }

        private async void RemoveItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedItem = (sender as FontIcon).DataContext as Item;

            if (selectedItem != null)
            {
                //MessageDialog md = new MessageDialog(selectedItem.Title);
                //md.ShowAsync();

                await viewModel.DeleteItem(selectedItem);
            }
        }

        private async void AddCategory_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await viewModel.addCategory(NewCategoryTitle.Text);
            NewCategoryTitle.Text = "";
        }

        private async void AddItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var categoryID = ((sender as FontIcon).DataContext as Category).Id;
            var newItemTitle = (((sender as FontIcon).Parent as Grid).Children.Where(c => c.GetType().Name == "TextBox").ToList()[0] as TextBox).Text;

            await viewModel.addItem(newItemTitle, categoryID);
        }
    }
}
