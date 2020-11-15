using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model {
	public class Category {
		public int Id { get; set; }

		[Required]
		public string Name { get; }

		[Required]
		private IList<Item> _items;
		public IList<Item> Items { get { return _items; } }

		[Required]
		private IList<TravelTask> _tasks;
		public IList<TravelTask> Tasks { get { return _tasks; } }

		public Category(string name) {
			Name = name;
			_items = new List<Item>();
			_tasks = new List<TravelTask>();
		}

		public void AddItemToList(Item newItem) {
			_items.Add(newItem);
		}

		public void RemoveItemFromList(Item item) {
			Items.Remove(item);
		}

		public void AddTaskToList(TravelTask newTask) {
			_tasks.Add(newTask);
		}

		public void RemoveTaskFromList(TravelTask task) {
			_tasks.Remove(task);
		}
	}
}
