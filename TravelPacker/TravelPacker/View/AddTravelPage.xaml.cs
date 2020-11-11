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

namespace TravelPacker.View {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AddTravelPage : Page {
		public AddTravelPage() {
			this.InitializeComponent();

			// TODO make image random --> set imagesource
			Random rd = new Random();
			int rdnumber = rd.Next(1, 7);

			//bgImage.ImageSource

			//this.Background = new ImageBrush() { ImageSource = new Uri("../Assets/BackgroundImages/travelBG1.jpg", UriKind.Relative) };

		}

		private async void Button_Click(object sender, RoutedEventArgs e) {
			try {
				Travel newTravel = new Travel(txt_title.Text, txt_location.Text, txt_image.Text.Length == 0 ? null : txt_image.Text);
				var newTravelJSON = JsonConvert.SerializeObject(newTravel);

				HttpClient client = new HttpClient();

				var result = await client.PostAsync(new Uri("https://localhost:44354/api/Travels"),
					new HttpStringContent(newTravelJSON, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode) {
					Frame.GoBack();
				}

			}
			catch (Exception ex) {
				// TODO Errormessage in form
				Console.WriteLine(ex.Message);
			}
		}
	}
}
