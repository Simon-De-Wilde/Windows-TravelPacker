using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelPackerAPI.Models {
	public class Travel {
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Location { get; set; }

		private string _imageUrl;

		public string ImageUrl {
			get { return _imageUrl; }
			set {
				if (value == null) {
					_imageUrl = "https://randomwordgenerator.com/img/picture-generator/5ee6d6454d57b10ff3d8992cc12c30771037dbf852547848702a7ed4974c_640.jpg";
				}
				else {
					_imageUrl = value;
				}
			}
		}

		public IList<Category> Categories { get; set; }

		public IList<ItineraryItem> Itineraries { get; set; }

		public Travel(string name, string location, string imageUrl) {
			Name = name;
			Location = location;
			ImageUrl = imageUrl;

			Categories = new List<Category>();

			Itineraries = new List<ItineraryItem>();
		}

		protected Travel() {
			// EF
		}
	}
}