using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace TravelPackerAPI.Models {
	public class Category {
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public ObservableCollection<Item> Items { get; set; }


		public Category(string name) {
			Name = name;
			Items = new ObservableCollection<Item>();
		}

		protected Category() {
			// EF
		}


		[JsonConstructor]
		protected Category(int id, string name, ObservableCollection<Item> items) {
			// Deserializeren
			Id = id;
			Name = name;
			Items = items;
		}
	}
}