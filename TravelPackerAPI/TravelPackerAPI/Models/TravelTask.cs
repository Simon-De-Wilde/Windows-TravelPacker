using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models {
	public class TravelTask : IItem {

		public TimeSpan Duration { get; set; }

		public TravelTask(string title, TimeSpan duration) : base(title) {
			Duration = duration;
		}

		protected TravelTask() {
			// EF
		}

		[JsonConstructor]
		protected TravelTask(int id, string title, bool done, TimeSpan duration) : base(id, title, done) {
			//deserializeren
			Duration = duration;
		}
	}
}
