

using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model {
	public class TravelTask : IItem {
		[JsonProperty("id")]
		public int Id { get; set; }
		[Required]
		[JsonProperty("duration")]
		public TimeSpan Duration { get; }

		public TravelTask(string title, TimeSpan duration) : base(title) {
			Duration = duration;
		}

		[JsonConstructor]
		public TravelTask() : base() {
			//deserializeren
		}
	}
}
