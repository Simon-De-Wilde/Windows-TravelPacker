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
	public class TravelsDetailPageViewModel {
		public Travel Travel { get; set; }

		public TravelsDetailPageViewModel() {
		}

		public async Task<bool> DeleteItineraryItem(ItineraryItem ii) {
			try {
				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var deleteResult = await client.DeleteAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/ItineraryItems/{ii.Id}"));

				if (deleteResult.IsSuccessStatusCode) {
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
