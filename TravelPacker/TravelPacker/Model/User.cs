﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPacker.Model {
	public class User {
		public int Id { get; set; }
		[Required]
		private string FirstName { get; }
		[Required]
		private string LastName { get; }
		[Required]
		private string Email { get; }

		[Required]
		private IList<Travel> _travels;
		public IList<Travel> Travels { get { return _travels; } }

		public User(string firstname, string lastname, string email) {
			FirstName = firstname;
			LastName = lastname;
			Email = email;

			_travels = new List<Travel>();
		}

		public void AddTravelToList(Travel newTravel) {
			_travels.Add(newTravel);
		}

		public void RemoveTravelFromList(Travel travel) {
			_travels.Remove(travel);
		}
	}
}
