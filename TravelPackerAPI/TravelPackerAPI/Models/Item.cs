using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models {
	public class Item : IItem {
		public string Title { get; set; }
		public bool Done { get; set; }

		// own properties
		public int Amount { get; set; }

	}
}
