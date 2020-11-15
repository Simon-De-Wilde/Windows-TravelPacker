using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPacker.Model;
using System.Net.Http;

namespace TravelPacker.ViewModel {
	public class TravelsPageViewModel {

		public ObservableCollection<Travel> Travels { get; set; }

		public TravelsPageViewModel() {
			Travels = new ObservableCollection<Travel>();

			GetTravels();
		}

		public async void GetTravels() {

			Travels.Clear();

			try {
				HttpClient client = new HttpClient();

				var json = await client.GetStringAsync(new Uri("https://localhost:44354/api/Travels"));

				var list = JsonConvert.DeserializeObject<List<Travel>>(json);

				foreach (Travel t in list) {
					Travels.Add(t);
				}
			}
			catch (Exception e) {
				Console.WriteLine(e.Message);
			}


		}

		public async Task<bool> DeleteTravel(Travel selectedTravel) {
			try {
				HttpClient client = new HttpClient();

				var deleteResult = await client.DeleteAsync(new Uri($"https://localhost:44354/api/Travels/{selectedTravel.Id}"));

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
