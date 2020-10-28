using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelPackerAPI.Models;
using TravelPackerAPI.Models.RepositoryInterfaces;

namespace TravelPackerAPI.Data.Repositories {
	public class TaskRepository : ITaskRepository {

		private readonly TravelPackerDbContext _dbContext;
		private readonly DbSet<Task> _tasks;

		public TaskRepository(TravelPackerDbContext dbcontext) {
			this._dbContext = dbcontext;
			this._tasks = _dbContext.Tasks;
		}

		public void Add(Task t) {
			_tasks.Add(t);
		}

		public void Delete(Task t) {
			_tasks.Remove(t);
		}

		public IEnumerable<Task> GetAll() {
			return _tasks;
		}

		public Models.Task GetById(int id) {
			return _tasks.FirstOrDefault(t => t.Id == id);
		}

		public void SaveChanges() {
			_dbContext.SaveChanges();
		}

		public void Update(Task t) {
			_tasks.Update(t);
		}
	}
}
