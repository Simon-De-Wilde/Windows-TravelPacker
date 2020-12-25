using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TravelPacker.Model;
using TravelPacker.Util;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace TravelPacker.ViewModel {
	public class UpdateTravelPageViewModel {

		public Travel Travel { get; set; }

		public async Task<bool> UpdateTravel(string title, string location, string imageUrl) {
			try {
				Travel.Name = title;
				Travel.Location = location;
				Travel.ImageUrl = imageUrl;


				string updatedTravelJSON = JsonConvert.SerializeObject(Travel);

				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				HttpResponseMessage result = await client.PutAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Travels/{Travel.Id}"),
					new HttpStringContent(updatedTravelJSON, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

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
