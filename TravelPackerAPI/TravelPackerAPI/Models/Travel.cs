using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

		public ObservableCollection<Category> Categories { get; set; }
		public ObservableCollection<TravelTask> Tasks { get; set; }
		public ObservableCollection<ItineraryItem> Itineraries { get; set; }

		public Travel(string name, string location, string imageUrl) {
			Name = name;
			Location = location;
			ImageUrl = imageUrl;

			Categories = new ObservableCollection<Category>();
			Tasks = new ObservableCollection<TravelTask>();
			Itineraries = new ObservableCollection<ItineraryItem>();
		}

		protected Travel() {
			// EF
		}

		[JsonConstructor]
		protected Travel(int id, string name, string location, string imageUrl, ObservableCollection<Category> categories, ObservableCollection<TravelTask> tasks, ObservableCollection<ItineraryItem> itineraries) {
			// Deserializeren
			Id = id;
			Name = name;
			Location = location;
			ImageUrl = imageUrl;
			Categories = categories;
			Tasks = tasks;
			Itineraries = itineraries;
		}
	}
}