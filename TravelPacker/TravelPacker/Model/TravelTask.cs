

using System;
using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model {
	public class TravelTask : IItem {
		[Required]
		public TimeSpan Duration { get; }

		public TravelTask(string title, TimeSpan duration) : base(title) {
			Duration = duration;
		}
	}
}
