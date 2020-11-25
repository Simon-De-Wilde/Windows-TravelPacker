using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPackerAPI.Models;
using TravelPackerAPI.Models.RepositoryInterfaces;

namespace TravelPackerAPI.Data.Repositories {
	public class TravelRepository : ITravelRepository {

		private readonly TravelPackerDbContext _dbContext;
		private readonly DbSet<Travel> _travels;

		public TravelRepository(TravelPackerDbContext dbcontext) {
			this._dbContext = dbcontext;
			this._travels = _dbContext.Travels;
		}

		public void Add(Travel t) {
			_travels.Add(t);
		}

		public void Delete(Travel t) {
			_travels.Remove(t);
		}

		public IEnumerable<Travel> GetAll() {
			return _travels
				.Include(t => t.Categories).ThenInclude(c => c.Items)
				.Include(t => t.Tasks)
				.Include(t => t.Itineraries);

		}

		public Travel GetById(int id) {
			return _travels
				.Include(t => t.Categories).ThenInclude(c => c.Items)
				.Include(t => t.Tasks)
				.Include(t => t.Itineraries)
				.FirstOrDefault(t => t.Id == id);
		}

		public void SaveChanges() {
			_dbContext.SaveChanges();
		}

		public void Update(Travel t) {
			_travels.Update(t);
		}
	}
}
