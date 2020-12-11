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
		protected IItem(int id, string title, bool done) {
			// Deserializeren
			Id = id;
			Title = title;
			_done = done;
		}

		public void SetDone() {
			_done = true;
		}

		public void SetNotDone()
        {
			_done = false;
		}
	}
}