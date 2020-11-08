
using System;
using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model {
	public class ItineraryItem : IItem {
		[Required]
		public DateTime Start { get; }

		[Required]
		public TimeSpan Duration { get; }

		[Required]
		public DateTime End { get; }

		[Required]
		private bool _doing;
		public bool Doing { get { return _doing; } }

		public ItineraryItem(string title, DateTime start, TimeSpan duration) : base(title) {
			Start = StartTimeValidation(start);
			Duration = duration;
			End = Start.AddSeconds(Duration.Seconds);
		}

		private DateTime StartTimeValidation(DateTime startTime) {
			if (startTime > DateTime.Now) { return startTime; }
			else { throw new ArgumentException("De start van een itinerary item moet in de toekomst liggen."); }
		}

		public bool isDoing() {
			_doing = End < DateTime.Now;
			return Doing;
		}
	}
}
