using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TravelPacker.Model {
	public class Category {
		[JsonProperty("id")]
		public int Id { get; set; }

		[Required]
		[JsonProperty("name")]
		public string Name { get; }

		[Required]
		[JsonProperty("items")]
		public ObservableCollection<Item> Items { get; set; }

		public int ItemsDone { get => Items.Where(i => i.Done == true).Count(); }

		public string OverviewName => Name + "	" + Items.Where(i => i.Done).ToList().Count + "/" + Items.Count;




		public Category(string name) {
			Name = name;
			Items = new ObservableCollection<Item>();
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
