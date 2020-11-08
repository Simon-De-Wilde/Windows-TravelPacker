using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravelPacker.Model;


namespace TravelPacker.ViewModel {
	public class TravelsPageViewModel {

		public ObservableCollection<Travel> Travels { get; set; }

		public TravelsPageViewModel() {
			Travels = new ObservableCollection<Travel>();

			GetTravels();
		}

		private async void GetTravels() {
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
	}
}
