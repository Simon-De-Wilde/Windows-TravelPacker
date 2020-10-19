﻿using System.ComponentModel.DataAnnotations;

namespace TravelPackerAPI.Models {
	public abstract class IItem {
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		protected bool _done;
		public virtual bool Done { get { return _done; } set { _done = value; } }

		public IItem(string title) {
			Title = title;
			Done = false;
		}

		protected IItem() {
			// EF
		}
	}
}