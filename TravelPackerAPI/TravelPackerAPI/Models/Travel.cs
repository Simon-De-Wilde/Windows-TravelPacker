using System.Collections.Generic;

namespace TravelPackerAPI.Models {
	public class Travel {
		public string Name { get; set; }
		public string Location { get; set; }
		public List<Category> Categories { get; set; }

		public Itinerary Itinerary { get; set; }
	}
}