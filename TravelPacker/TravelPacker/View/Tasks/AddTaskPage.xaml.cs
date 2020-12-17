using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelPacker.Model;
using TravelPacker.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class AddTaskPage : Page
    {
        public AddTaskPageViewModel ViewModel { get; set; }

        public AddTaskPage()
        {
            this.InitializeComponent();
            ViewModel = new AddTaskPageViewModel();
        }

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			Travel travel = (Travel)e.Parameter;

			ViewModel.Travel = travel;
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
        {
			errorbox.Children.Clear();
			CheckForm();

			if (!errorbox.Children.Any())
			{ 
				var success = await ViewModel.AddTaskToTravel(txt_title.Text, TimeSpan.FromMinutes(Convert.ToInt32(tp_duration.Text)));

				if (success != null)
				{
					ViewModel.Travel.Tasks.Add(success);
					Frame.GoBack();
				}
				else
				{
					MessageDialog md = new MessageDialog("Something went wrong, task was not created. Try again later");
					await md.ShowAsync();
				}
			}
		}



		private void CheckForm()
		{
			if (string.IsNullOrEmpty(txt_title.Text) || string.IsNullOrWhiteSpace(txt_title.Text))
			{
				TextBlock titleError = new TextBlock()
				{
					Text = "Title is required",
					Foreground = new SolidColorBrush(Colors.DarkRed)
				};
				errorbox.Children.Add(titleError);
			}
			if (tp_duration.Text == null)
			{
				TextBlock locationError = new TextBlock()
				{
					Text = "Duration is required",
					Foreground = new SolidColorBrush(Colors.DarkRed)
				};
				errorbox.Children.Add(locationError);
			}

		}
	}
}
