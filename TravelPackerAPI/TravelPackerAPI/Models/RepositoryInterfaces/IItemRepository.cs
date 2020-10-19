using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models.RepositoryInterfaces {
	public interface IItemRepository {

		IEnumerable<Item> GetAll();

		Item GetById(int id);

		void Add(Item i);

		void Delete(Item i);

		void Update(Item i);
	}
}
