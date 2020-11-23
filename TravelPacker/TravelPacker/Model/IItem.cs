using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model {
	public abstract class IItem {
		[JsonProperty("id")]
		public int Id { get; set; }
		[Required]
		[JsonProperty("title")]
		public string Title { get; }

		[Required]
		[JsonProperty("done")]
		private bool _done;
		public bool Done { get { return _done; } }

		public IItem(string title) {
			Title = title;
		}

		[JsonConstructor]
		public IItem() {
			// Deserializeren
		}

		public void SetDone() {
			_done = true;
		}
	}
}