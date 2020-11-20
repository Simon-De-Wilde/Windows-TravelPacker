using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model {
	public abstract class IItem {
		public int Id { get; set; }
		[Required]
		public string Title { get; }

		[Required]
		private bool _done;
		public bool Done { get { return _done; } }

		public IItem(string title) {
			Title = title;
		}

		protected IItem() {
			// Deserializeren
		}

		public void SetDone() {
			_done = true;
		}
	}
}