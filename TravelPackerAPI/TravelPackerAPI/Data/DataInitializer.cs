using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Data {
	public class DataInitializer {

		private readonly TravelPackerDbContext _dbContext;
		private readonly UserManager<IdentityUser> _usermanager;

		public DataInitializer(TravelPackerDbContext dbContext, UserManager<IdentityUser> userManager) {
			_dbContext = dbContext;
			_usermanager = userManager;
		}

		public async Task InitializeData() {
			_dbContext.Database.EnsureDeleted();
			if (_dbContext.Database.EnsureCreated()) {
				Console.WriteLine("DB created");
			}
		}
	}
}
