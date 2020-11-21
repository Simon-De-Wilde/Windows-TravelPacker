using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPackerAPI.Models;
using TravelPackerAPI.Models.RepositoryInterfaces;

namespace TravelPackerAPI.Data.Repositories {
	public class UserRepository : IUserRepository {

		private readonly TravelPackerDbContext _dbContext;
		private readonly DbSet<User> _users;

		public UserRepository(TravelPackerDbContext dbcontext) {
			this._dbContext = dbcontext;
			this._users = _dbContext.Users;
		}

		public void Add(User u) {
			_users.Add(u);
		}

		public void Delete(User u) {
			_users.Remove(u);
		}

		public IEnumerable<User> GetAll() {
			return _users.Include(u => u.Travels);
		}

		public User GetByEmail(string email) {
			return _users
				.Include(u => u.Travels).ThenInclude(t => t.Categories).ThenInclude(c => c.Items)
				.Include(u => u.Travels).ThenInclude(t => t.Categories).ThenInclude(c => c.Tasks)
				.Include(u => u.Travels).ThenInclude(t => t.Itineraries)
				.FirstOrDefault(u => u.Email == email);
		}

		public User GetById(int id) {
			return _users.Include(u => u.Travels).FirstOrDefault(u => u.Id == id);
		}

		public void SaveChanges() {
			_dbContext.SaveChanges();
		}

		public void Update(User u) {
			_users.Update(u);
		}
	}
}
