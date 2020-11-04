using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model
{
    internal class Category
    {
        [Required]
        public string Name { get; }

        [Required]
        private IList<Item> _items;
        public IList<Item> Items { get { return _items;  } }

        [Required]
        private IList<Task> _tasks;
        public IList<Task> Tasks { get { return _tasks; } }

        public Category(string name) {
            Name = name;
            _items = new List<Item>();
            _tasks = new List<Task>();
        }

        public void addItemToList(Item newItem) {
            _items.Add(newItem);
        }

        public void removeItemFromList(Item item) {
            Items.Remove(item);
        }

        public void addTaskToList(Task newTask) {
            _tasks.Add(newTask);
        }

        public void removeTaskFromList(Task task) {
            _tasks.Remove(task);
        }
    }
}
