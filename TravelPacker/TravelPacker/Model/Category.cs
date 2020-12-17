using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TravelPacker.Model {
	public class Category : INotifyPropertyChanged {
		[JsonProperty("id")]
		public int Id { get; set; }

		[Required]
		[JsonProperty("name")]
		public string Name { get; }

		[Required]
		[JsonProperty("items")]
		public ObservableCollection<Item> Items { get; set; }

		private int _itemsDone { get; set; }
		public int ItemsDone { get { return _itemsDone; } set { _itemsDone = value; OnPropertyChanged(nameof(ItemsDone)); } }

		public string OverviewName => Name + "	" + Items.Where(i => i.Done).ToList().Count + "/" + Items.Count;




		public Category(string name) {
			Name = name;
			Items = new ObservableCollection<Item>();

			_itemsDone = Items.Where(i => i.Done == true).Count();
		}

		[JsonConstructor]
		protected Category(int id, string name, ObservableCollection<Item> items) {
			// Deserializeren
			Id = id;
			Name = name;
			Items = items;

			_itemsDone = Items.Where(i => i.Done == true).Count();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName) {
			if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
		}
    }
}
