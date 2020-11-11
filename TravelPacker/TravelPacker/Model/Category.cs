﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model {
	public class Category {
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

		public void addItemToList(Item newItem) {
			_items.Add(newItem);
		}

		public void removeItemFromList(Item item) {
			Items.Remove(item);
		}

		public void addTaskToList(TravelTask newTask) {
			_tasks.Add(newTask);
		}

		public void removeTaskFromList(TravelTask task) {
			_tasks.Remove(task);
		}
	}
}