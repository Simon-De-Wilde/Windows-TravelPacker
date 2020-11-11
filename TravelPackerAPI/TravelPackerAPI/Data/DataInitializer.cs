using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPackerAPI.Models;

namespace TravelPackerAPI.Data {
	public class DataInitializer {

		private readonly TravelPackerDbContext _dbContext;
		private readonly UserManager<IdentityUser> _usermanager;

		public DataInitializer(TravelPackerDbContext dbContext, UserManager<IdentityUser> userManager) {
			_dbContext = dbContext;
			_usermanager = userManager;
		}

		public async System.Threading.Tasks.Task InitializeData() {
			_dbContext.Database.EnsureDeleted();
			if (_dbContext.Database.EnsureCreated()) {
				//if (!_dbContext.Travels.Any()) {


				Travel travel = new Travel("Quartier Latin", "Paris");

				Category category = new Category("BathroomStuff");
				travel.Categories.Add(category);

				Item item = new Item("toothbrush");
				category.Items.Add(item);
				Models.Task task = new Models.Task("Refill shampoo", new TimeSpan(0, 20, 0));
				category.Tasks.Add(task);

				ItineraryItem ii = new ItineraryItem("Board", DateTime.Now.AddDays(1), new TimeSpan(0, 30, 0));
				travel.Itineraries.Add(ii);

				_dbContext.Travels.Add(travel);
				_dbContext.SaveChanges();

			}
		}
	}
}
