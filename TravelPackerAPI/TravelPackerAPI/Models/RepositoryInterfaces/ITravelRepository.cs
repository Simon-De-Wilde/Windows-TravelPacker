using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models.RepositoryInterfaces {
	public interface ITravelRepository {

		IEnumerable<Travel> GetAll();

		Travel GetById(int id);

		void Add(Travel t);

		void Delete(Travel t);

		void Update(Travel t);

		void SaveChanges();
	}
}
