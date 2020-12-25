using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TravelPacker.Model;
using TravelPacker.Util;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace TravelPacker.View {
	public class AddTravelPageViewModel {


		public async Task<bool> AddTravel(string title, string location, string imageUrl) {
			try {
				Travel newTravel = new Travel(title, location, imageUrl.Length == 0 ? null : imageUrl);
				string newTravelJSON = JsonConvert.SerializeObject(newTravel);

				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				HttpResponseMessage result = await client.PostAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Travels"),
					new HttpStringContent(newTravelJSON, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode) {
					return true;
				}
				else {
					throw new Exception();
				}
			}
			catch {
				return false;
			}
		}
	}
}