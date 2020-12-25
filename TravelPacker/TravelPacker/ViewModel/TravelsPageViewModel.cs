using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPacker.Model;
using TravelPacker.Util;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace TravelPacker.ViewModel {
	public class TravelsPageViewModel {

		public ObservableCollection<Travel> Travels { get; set; }

		public TravelsPageViewModel() {
			Travels = new ObservableCollection<Travel>();
		}

		public async Task<bool> GetTravels() {

			Travels.Clear();

			try {
				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var json = await client.GetStringAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Travels"));

				var list = JsonConvert.DeserializeObject<List<Travel>>(json);

				foreach (Travel t in list) {
					Travels.Add(t);
				}

				return true;
			}
			catch (Exception e) {
				Console.WriteLine(e.Message);
				return false;
			}


		}

		public async Task<bool> DeleteTravel(Travel selectedTravel) {
			try {
				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var deleteResult = await client.DeleteAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Travels/{selectedTravel.Id}"));

				if (deleteResult.IsSuccessStatusCode) {
					Travels.Remove(selectedTravel);
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
