using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPacker.Model {
	public class User {
		public int Id { get; set; }
		[Required]
		public string FirstName { get; }
		[Required]
		public string LastName { get; }
		[Required]
		public string Email { get; }

		[Required]
		public IList<Travel> Travels { get; set; }

		public User(string firstname, string lastname, string email) {
			FirstName = firstname;
			LastName = lastname;
			Email = email;

			Travels = new List<Travel>();
		}

	}
}
