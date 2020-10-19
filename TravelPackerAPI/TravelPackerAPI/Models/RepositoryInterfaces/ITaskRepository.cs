using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models.RepositoryInterfaces {
	public interface ITaskRepository {
		IEnumerable<Task> GetAll();

		Task GetById(int id);

		void Add(Task t);

		void Delete(Task t);

		void Update(Task t);
	}
}
