﻿using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model {
	public class Item : IItem {
		[Required]
		public int Amount { get; }

		public Item(string title, int amount = 1) : base(title) {
			Amount = amount;
		}
	}
}