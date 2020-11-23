

using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model {
	public class TravelTask : IItem {
		[Required]
		[JsonProperty("duration")]
		public TimeSpan Duration { get; }

		public TravelTask(string title, TimeSpan duration) : base(title) {
			Duration = duration;
		}

		[JsonConstructor]
		protected TravelTask(int id, string title, bool done, TimeSpan duration) : base(id, title, done) {
			//deserializeren
			Duration = duration;
		}
	}
}
