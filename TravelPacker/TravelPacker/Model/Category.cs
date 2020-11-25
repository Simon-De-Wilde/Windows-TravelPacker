using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
		public IList<Item> Items { get; set; }

		public double Progress {
			get {
				return Items.Count == 0 ? 0 : Convert.ToDouble(Items.Count(i => i.Done)) / Items.Count * 100;
			}
			set { }
		}

		public Category(string name) {
			Name = name;
			Items = new List<Item>();
		}

		[JsonConstructor]
		protected Category(int id, string name, IList<Item> items) {
			// Deserializeren
			Id = id;
			Name = name;
			Items = items;
		}

	}
}
