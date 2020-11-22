using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using TravelPacker.Model;
using TravelPacker.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelPacker.View.Travels {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AddTravelPage : Page {

		public AddTravelPageViewModel ViewModel { get; set; }

		public AddTravelPage() {
			this.InitializeComponent();

			ViewModel = new AddTravelPageViewModel();

			// TODO make image random --> werkt niet omdat de uri niet kan gecontroleerd worden of zoiets...
			//Random rd = new Random();
			//int rdnumber = rd.Next(1, 7);

			//String uriString = $"../Assets/BackgroundImages/travelBG{rdnumber}.jpg";

			//bgImage.ImageSource = new BitmapImage(new Uri(uriString, UriKind.Relative));

		}

		private async void Button_Click(object sender, RoutedEventArgs e) {

			bool success = await ViewModel.AddTravel(txt_title.Text, txt_location.Text, txt_image.Text);

			if (success) {
				Frame.GoBack();
			}
			else {
				MessageDialog md = new MessageDialog("There were mistakes in het form, please fill in all inputs");
				await md.ShowAsync();
			}

		}
	}
}
