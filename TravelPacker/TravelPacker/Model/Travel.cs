
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TravelPacker.Model {
	public class Travel {
		[JsonProperty("id")]
		public int Id { get; set; }
		[Required]
		[JsonProperty("name")]
		public string Name { get; set; }

		[Required]
		[JsonProperty("location")]
		public string Location { get; set; }

		[JsonProperty("imageUrl")]
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

		public double Progress {
			get {
				double progressSum = Categories.Sum(c => c.Progress);
				return Categories.Count == 0 ? 0 : progressSum / Categories.Count;
			}
			set { }
		}


		[Required]
		[JsonProperty("categories")]
		public IList<Category> Categories { get; set; }

		[Required]
		[JsonProperty("itineraries")]
		public IList<ItineraryItem> Itineraries { get; set; }

		public Travel(string name, string location, string imageUrl) {
			Name = name;
			Location = location;
			ImageUrl = imageUrl;
			Categories = new List<Category>();
			Itineraries = new List<ItineraryItem>();
		}

		[JsonConstructor]
		protected Travel(int id, string name, string location, string imageUrl, IList<Category> categories, IList<ItineraryItem> itineraries) {
			// Deserializeren
			Id = id;
			Name = name;
			Location = location;
			ImageUrl = imageUrl;
			Categories = categories;
			Itineraries = itineraries;
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
