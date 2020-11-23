using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPacker.Model {
	public class User {
		[JsonProperty("id")]
		public int Id { get; set; }
		[Required]
		[JsonProperty("firstName")]
		private string FirstName { get; }
		[Required]
		[JsonProperty("lastName")]
		private string LastName { get; }
		[Required]
		[JsonProperty("email")]
		private string Email { get; }

		[Required]
		[JsonProperty("travels")]
		public IList<Travel> Travels { get; set; }

		public User(string firstname, string lastname, string email) {
			FirstName = firstname;
			LastName = lastname;
			Email = email;

			Travels = new List<Travel>();
		}

		[JsonConstructor]
		protected User(int id, string firstName, string lastName, string email, IList<Travel> travels) {
			// Deserializeren
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Travels = travels;
		}
	}
}
