
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
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
				int totalAmountOfItems = Categories.Sum(c => c.Items.Count);
				if (Tasks.Count == 0 && totalAmountOfItems == 0) {
					return 0;
				}
				double catProgress = Convert.ToDouble(Categories.Sum(c => c.ItemsDone)) / (totalAmountOfItems + Tasks.Count) * 100;
				double tasksProgress = Convert.ToDouble(Tasks.Count(i => i.Done)) / (totalAmountOfItems + Tasks.Count) * 100;
				return catProgress + tasksProgress;
			}
		}


		[Required]
		[JsonProperty("categories")]
		public ObservableCollection<Category> Categories { get; set; }

		[Required]
		[JsonProperty("tasks")]
		public ObservableCollection<TravelTask> Tasks { get; set; }

		[Required]
		[JsonProperty("itineraries")]
		public ObservableCollection<ItineraryItem> Itineraries { get; set; }

		public Travel(string name, string location, string imageUrl) {
			Name = name;
			Location = location;
			ImageUrl = imageUrl;
			Categories = new ObservableCollection<Category>();
			Tasks = new ObservableCollection<TravelTask>();
			Itineraries = new ObservableCollection<ItineraryItem>();
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
