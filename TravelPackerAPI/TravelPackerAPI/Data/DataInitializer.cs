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
			//_dbContext.Database.EnsureDeleted();
			//if (_dbContext.Database.EnsureCreated()) {
			if (!_dbContext.Travels.Any()) {


				Travel travel1 = new Travel("Quartier Latin", "Paris", null);

				Category category1 = new Category("BathroomStuff");
				travel1.Categories.Add(category1);

				Item iter1 = new Item("toothbrush");
				category1.Items.Add(iter1);
				Models.TravelTask task1 = new Models.TravelTask("Refill shampoo", new TimeSpan(0, 20, 0));
				category1.Tasks.Add(task1);

				ItineraryItem ii1 = new ItineraryItem("Board", DateTime.Now.AddDays(1), new TimeSpan(0, 30, 0));
				travel1.Itineraries.Add(ii1);

				_dbContext.Travels.Add(travel1);
				_dbContext.SaveChanges();
				/////////////////////////////

				Travel travel2 = new Travel("Kings Cross", "London", null);

				Category category2 = new Category("Snacks");
				travel2.Categories.Add(category2);

				Item item2 = new Item("chips");
				category2.Items.Add(item2);
				Models.TravelTask task2 = new Models.TravelTask("Make popcorn", new TimeSpan(0, 20, 0));
				category2.Tasks.Add(task2);

				ItineraryItem ii2 = new ItineraryItem("Taxi ride", DateTime.Now.AddDays(1), new TimeSpan(0, 30, 0));
				travel2.Itineraries.Add(ii2);

				_dbContext.Travels.Add(travel2);
				_dbContext.SaveChanges();

			}
		}
	}
}
