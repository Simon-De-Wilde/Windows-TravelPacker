using System;

namespace TravelPackerAPI.Models {
	public class Itinerary : IItem {
		public DateTime Start { get; set; }
		public TimeSpan Duration { get; set; }

		public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public bool Done {
			get {
				return Done;
			}
			set {
				_ = Start.AddSeconds(Duration.TotalSeconds) < DateTime.Now;
			}
		}
		public bool Doing {
			get {
				return Doing;
			}
			set {
				_ = Start < DateTime.Now;
			}
		}

	}
}