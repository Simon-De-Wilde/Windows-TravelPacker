using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TravelPacker.Model {
	public class Category {
		public int Id { get; set; }

		[Required]
		public string Name { get; }

		[Required]
		public IList<Item> Items { get; set; }

		[Required]
		private IList<TravelTask> _tasks;
		public IList<TravelTask> Tasks { get; set; }

		public double Progress {
			get {
				double itemProgress = Convert.ToDouble(Items.Count(i => i.Done)) / Items.Count * 100;
				double taskProgress = Convert.ToDouble(Tasks.Count(t => t.Done)) / Tasks.Count * 100;

				double calculated = (itemProgress + taskProgress) / 2;
				return calculated;
			}
			set { }
		}

		public Category(string name) {
			Name = name;
			Items = new List<Item>();
			Tasks = new List<TravelTask>();
		}

		protected Category() {
			// Deserializeren
		}

	}
}
