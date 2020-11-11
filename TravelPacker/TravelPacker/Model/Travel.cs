
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TravelPacker.Model {
	public class Travel {
		public int Id { get; set; }
		[Required]
		public string Name { get; }

		[Required]
		public string Location { get; }

		[Required]
		private IList<Category> _categories;
		public IList<Category> Categories { get { return _categories; } }

		[Required]
		private IList<ItineraryItem> _itineraries;
		public IList<ItineraryItem> Itineraries { get { return _itineraries; } }

		public Travel(string name, string location) {
			Name = name;
			Location = location;
			_categories = new List<Category>();
			_itineraries = new List<ItineraryItem>();
		}

		public IList<TravelTask> getAllTasks() {
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
