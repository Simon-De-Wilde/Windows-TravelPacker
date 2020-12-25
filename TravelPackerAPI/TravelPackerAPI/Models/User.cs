using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models {
	public class User {
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Email { get; set; }


		public ObservableCollection<Travel> Travels { get; set; }

		public User(string firstname, string lastname, string email) {
			FirstName = firstname;
			LastName = lastname;
			Email = email;

			Travels = new ObservableCollection<Travel>();
		}

		protected User() {
			// EF
		}

		[JsonConstructor]
		protected User(int id, string firstName, string lastName, string email, ObservableCollection<Travel> travels) {
			// Deserializeren
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Travels = travels;
		}
	}
}
