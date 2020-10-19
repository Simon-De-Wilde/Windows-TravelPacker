using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models {
	public class Task : IItem {

		public TimeSpan Duration { get; set; }

		public Task(string title, TimeSpan duration) : base(title) {
			Duration = duration;
		}

		protected Task() {
			// EF
		}
	}
}
