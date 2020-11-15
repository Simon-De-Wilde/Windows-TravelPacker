
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TravelPacker.Model {
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


		[Required]
		private IList<Category> _categories;
		public IList<Category> Categories { get { return _categories; } }

		[Required]
		private IList<ItineraryItem> _itineraries;
		public IList<ItineraryItem> Itineraries { get { return _itineraries; } }

		public Travel(string name, string location, string imageUrl) {
			Name = name;
			Location = location;
			ImageUrl = imageUrl;
			_categories = new List<Category>();
			_itineraries = new List<ItineraryItem>();
		}

		public IList<TravelTask> GetAllTasks() {
			var list = new List<TravelTask>();

			foreach (Category c in Categories) {
				foreach (TravelTask t in c.Tasks) {
					list.Add(t);
				}
			}

			return list;
		}
	}
}
