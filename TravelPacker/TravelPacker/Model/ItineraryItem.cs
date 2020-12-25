
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
		public DateTime End { get => Start.AddSeconds(Duration.TotalSeconds); }

		[Required]
		[JsonProperty("doing")]
		private bool _doing;
		public bool Doing { get { return _doing; } }

		public ItineraryItem(string title, DateTime start, TimeSpan duration) : base(title) {
			Start = StartTimeValidation(start);
			Duration = duration;
		}

		[JsonConstructor]
		protected ItineraryItem(int id, string title, bool done, DateTime start, TimeSpan duration, bool doing) : base(id, title, done) {
			// Deserializeren
			Start = start;
			Duration = duration;
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

		public new bool Done { get => End < DateTime.Now; }

	}
}
