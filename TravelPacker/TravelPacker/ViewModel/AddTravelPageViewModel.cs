using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TravelPacker.Model;
using TravelPacker.Util;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;

namespace TravelPacker.View {
	public class AddTravelPageViewModel {


		public async Task<bool> AddTravel(string title, string location, string imageUrl) {
			try {
				Travel newTravel = new Travel(title, location, imageUrl.Length == 0 ? null : imageUrl);
				var newTravelJSON = JsonConvert.SerializeObject(newTravel);

				HttpClient client = new HttpClient();

				var result = await client.PostAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Travels"),
					new HttpStringContent(newTravelJSON, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode) {
					return true;
				}
				else {
					throw new Exception();
				}
			}
			catch (Exception e) {
				return false;
			}
		}
	}
}