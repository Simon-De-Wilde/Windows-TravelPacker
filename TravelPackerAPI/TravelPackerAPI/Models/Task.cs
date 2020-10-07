using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models {
	public class Task : IItem {
		public string Title { get; set; }
		public bool Done { get; set; }

		public TimeSpan Duration { get; set; }
	}
}
