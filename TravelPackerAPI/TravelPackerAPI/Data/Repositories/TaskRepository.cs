using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelPackerAPI.Models;
using TravelPackerAPI.Models.RepositoryInterfaces;

namespace TravelPackerAPI.Data.Repositories {
	public class TaskRepository : ITaskRepository {

		private readonly TravelPackerDbContext _dbContext;
		private readonly DbSet<TravelTask> _tasks;

		public TaskRepository(TravelPackerDbContext dbcontext) {
			this._dbContext = dbcontext;
			this._tasks = _dbContext.Tasks;
		}

		public void Add(TravelTask t) {
			_tasks.Add(t);
		}

		public void Delete(TravelTask t) {
			_tasks.Remove(t);
		}

		public IEnumerable<TravelTask> GetAll() {
			return _tasks;
		}

		public Models.TravelTask GetById(int id) {
			return _tasks.FirstOrDefault(t => t.Id == id);
		}

		public void SaveChanges() {
			_dbContext.SaveChanges();
		}

		public void Update(TravelTask t) {
			_tasks.Update(t);
		}
	}
}
