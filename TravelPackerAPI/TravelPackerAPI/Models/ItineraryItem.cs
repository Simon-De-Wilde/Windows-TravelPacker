using System;
using System.ComponentModel.DataAnnotations;

namespace TravelPackerAPI.Models {
	public class ItineraryItem : IItem {

		private DateTime _start;

		[Required]
		public DateTime Start {
			get { return _start; }
			set {
				if (value < DateTime.Now)
					throw new ArgumentException("De start van een itinerary item moet in de toekomst liggen.");
				else
					_start = value;

			}

		}

		[Required]
		public TimeSpan Duration { get; set; }

		[Required]
		public override bool Done {
			get {
				return _done;
			}
			set {
				_done = Start.AddSeconds(Duration.TotalSeconds) < DateTime.Now;
			}
		}

		private bool _doing;
		[Required]
		public bool Doing {
			get {
				return _doing;
			}
			set {
				_doing = Start < DateTime.Now;
			}
		}

		public ItineraryItem(string title, DateTime start, TimeSpan duration) : base(title) {
			Start = start;
			Duration = duration;
		}

		protected ItineraryItem() {
			// EF
		}



	}
}