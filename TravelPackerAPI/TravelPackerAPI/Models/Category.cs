using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models {
	public class Category {
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public IList<IItem> Items { get; set; }

		public Category(string name) {
			Name = name;
			Items = new List<IItem>();
		}

		protected Category() {
			// EF
		}
	}
}