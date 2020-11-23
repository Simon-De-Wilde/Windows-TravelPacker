using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model {
	public class Item : IItem {
		[Required]
		[JsonProperty("amount")]
		public int Amount { get; }

		public Item(string title, int amount = 1) : base(title) {
			Amount = amount;
		}

		[JsonConstructor]
		protected Item(int id, string title, bool done, int amount) : base(id, title, done) {
			// Deserializeren
			Amount = amount;
		}
	}
}