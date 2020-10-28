using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models {
	public class Category {
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public IList<Item> Items { get; set; }
		public IList<Task> Tasks { get; set; }

		public Category(string name) {
			Name = name;
			Items = new List<Item>();
			Tasks = new List<Task>();
		}

		protected Category() {
			// EF
		}
	}
}