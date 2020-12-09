using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace TravelPackerAPI.Models {
	public class Category {
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public IList<Item> Items { get; set; }
		public string OverviewName => Name + "	" + Items.Where(i => i.Done).ToList().Count  + "/" + Items.Count;


		public Category(string name) {
			Name = name;
			Items = new List<Item>();
		}

		protected Category() {
			// EF
		}
	}
}