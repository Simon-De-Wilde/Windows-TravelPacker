using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPacker.Model;
using Windows.Web.Http;

namespace TravelPacker.ViewModel {
	public class TravelsPageViewModel {

		public ObservableCollection<Travel> Travels { get; set; }

		public TravelsPageViewModel() {
			Travels = new ObservableCollection<Travel>();

			GetTravels();
		}

		private async void GetTravels() {

			Travel travel = new Travel("Quartier Latin", "Paris");

			Category category = new Category("BathroomStuff");
			travel.Categories.Add(category);

			Item item = new Item("toothbrush");
			category.Items.Add(item);
			TravelTask task = new TravelTask("Refill shampoo", new TimeSpan(0, 20, 0));
			category.Tasks.Add(task);

			ItineraryItem ii = new ItineraryItem("Board", DateTime.Now.AddDays(1), new TimeSpan(0, 30, 0));
			travel.Itineraries.Add(ii);

			Travels.Add(travel);
			Travels.Add(travel);
			Travels.Add(travel);
			Travels.Add(travel);
			Travels.Add(travel);
			Travels.Add(travel);



			//TODO connectie met de api
			//try {
			//	HttpClient client = new HttpClient();

			//	var json = await client.GetStringAsync(new Uri("https://localhost:44354/api/Travels"));

			//	var list = JsonConvert.DeserializeObject<List<Travel>>(json);

			//	foreach (Travel t in list) {
			//		Travels.Add(t);
			//	}
			//}
			//catch (Exception e) {
			//	Console.WriteLine(e.Message);
			//}


		}
	}
}
