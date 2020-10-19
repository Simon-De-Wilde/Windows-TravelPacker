using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models.RepositoryInterfaces {
	public interface IUserRepository {

		IEnumerable<User> GetAll();

		User GetById(int id);

		User GetByEmail(string email);

		void Add(User u);

		void Delete(User u);

		void Update(User u);

	}
}
