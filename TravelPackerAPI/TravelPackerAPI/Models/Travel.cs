using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelPackerAPI.Models {
	public class Travel {
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Location { get; set; }

		public IList<Category> Categories { get; set; }

		public IList<ItineraryItem> Itineraries { get; set; }

		public Travel(string name, string location) {
			Name = name;
			Location = location;

			Categories = new List<Category>();

			Itineraries = new List<ItineraryItem>();
		}

		protected Travel() {
			// EF
		}
	}
}