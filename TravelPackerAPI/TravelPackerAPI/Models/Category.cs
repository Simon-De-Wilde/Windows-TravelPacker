using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models {
	public class Category {
		public string Name { get; set; }
		public List<IItem> Items { get; set; }
	}
}