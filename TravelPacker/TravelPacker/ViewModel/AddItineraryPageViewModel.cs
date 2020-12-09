using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPacker.Model;
using TravelPacker.Util;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace TravelPacker.ViewModel {
	public class AddItineraryPageViewModel {
		public Travel Travel { get; internal set; }

		public async Task<ItineraryItem> AddItineraryToTravel(string titel, DateTime start, int durationInMinutes) {
			try {
				TimeSpan duration = new TimeSpan(0, durationInMinutes, 0);

				ItineraryItem newIT = new ItineraryItem(titel, start, duration);
				var newItineraryItemJSON = JsonConvert.SerializeObject(newIT);

				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var result = await client.PostAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/ItineraryItems/{Travel.Id}"),
					new HttpStringContent(newItineraryItemJSON, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode) {
					return JsonConvert.DeserializeObject<ItineraryItem>(result.Content.ToString());
				}
				else {
					throw new Exception();
				}
			}
			catch (Exception e) {
				return null;
			}
		}
	}
}
