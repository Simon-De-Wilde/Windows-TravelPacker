using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models {
	public class Item : IItem {

		// own properties
		[Required]
		public int Amount { get; set; }

		public Item(string title, int amount = 1) : base(title) {
			Amount = amount;
			Done = false;
		}

		protected Item() {
			// EF
		}

		[JsonConstructor]
		protected Item(int id, string title, bool done, int amount) : base(id, title, done) {
			// Deserializeren
			Amount = amount;
		}
	}
}
