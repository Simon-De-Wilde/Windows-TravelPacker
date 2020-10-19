using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models.RepositoryInterfaces {
	public interface ICategoryRepository {

		IEnumerable<Category> GetAll();

		Category GetById(int id);

		void Add(Category c);

		void Delete(Category c);

		void Update(Category c);
	}
}
