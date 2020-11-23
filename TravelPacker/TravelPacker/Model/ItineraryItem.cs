
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model {
	public class ItineraryItem : IItem {
		[Required]
		[JsonProperty("start")]
		public DateTime Start { get; }

		[Required]
		[JsonProperty("duration")]
		public TimeSpan Duration { get; }

		[Required]
		[JsonProperty("end")]
		public DateTime End { get; }

		[Required]
		[JsonProperty("doing")]
		private bool _doing;
		public bool Doing { get { return _doing; } }

		public ItineraryItem(string title, DateTime start, TimeSpan duration) : base(title) {
			Start = StartTimeValidation(start);
			Duration = duration;
			End = Start.AddSeconds(Duration.Seconds);
		}

		[JsonConstructor]
		protected ItineraryItem(int id, string title, bool done, DateTime start, TimeSpan duration, DateTime end, bool doing) : base(id, title, done) {
			// Deserializeren
			Start = start;
			Duration = duration;
			End = end;
			_doing = doing;
		}

		private DateTime StartTimeValidation(DateTime startTime) {
			if (startTime > DateTime.Now) { return startTime; }
			else { throw new ArgumentException("De start van een itinerary item moet in de toekomst liggen."); }
		}

		public bool IsDoing() {
			_doing = End < DateTime.Now;
			return Doing;
		}
	}
}
