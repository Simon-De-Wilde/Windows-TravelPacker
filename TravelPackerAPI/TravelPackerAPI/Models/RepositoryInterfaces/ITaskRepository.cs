using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models.RepositoryInterfaces {
	public interface ITaskRepository {
		IEnumerable<TravelTask> GetAll();

		TravelTask GetById(int id);

		void Add(TravelTask t);

		void Delete(TravelTask t);

		void Update(TravelTask t);

		void SaveChanges();
	}
}
