using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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


		public IList<Travel> Travels { get; set; }

		public User(string firstname, string lastname, string email) {
			FirstName = firstname;
			LastName = lastname;
			Email = email;

			Travels = new List<Travel>();
		}

		protected User() {
			// EF
		}
	}
}
