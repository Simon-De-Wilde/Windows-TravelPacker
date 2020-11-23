using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model {
	public class Item : IItem {
		[JsonProperty("id")]
		public int Id { get; set; }
		[Required]
		[JsonProperty("amount")]
		public int Amount { get; }

		public Item(string title, int amount = 1) : base(title) {
			Amount = amount;
		}

		[JsonConstructor]
		public Item() : base() {
			// Deserializeren
		}
	}
}