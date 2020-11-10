using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelPacker.View {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AddTravelPage : Page {

		public AddTravelPageViewModel ViewModel { get; private set; }

		public AddTravelPage() {
			this.InitializeComponent();

			// TODO make image random --> set imagesource
			Random rd = new Random();
			int rdnumber = rd.Next(1, 7);

			//bgImage.ImageSource

			//this.Background = new ImageBrush() { ImageSource = new Uri("../Assets/BackgroundImages/travelBG1.jpg", UriKind.Relative) };

			ViewModel = new AddTravelPageViewModel();



		}

		private void Button_Click(object sender, RoutedEventArgs e) {

		}
	}
}
