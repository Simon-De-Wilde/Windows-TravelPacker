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

		public async Task InitializeData()
		{
			_dbContext.Database.EnsureDeleted();
			if (_dbContext.Database.EnsureCreated())
			{
				if (!_dbContext.Users.Any())
				{

					User student = new User("Student", "McStudent", "student@hogent.be");
					User prof = new User("Prof", "McProf", "prof@hogent.be");

					IList<User> users = new List<User>() { student, prof };

					_dbContext.Users.AddRange(users);

					await RegisterUsers(users);


					Travel travelQL = new Travel("Quartier Latin - summer 2023", "Paris", null);
					Travel travelKC = new Travel("Kings Cross - spring 2024", "London", null);

					student.Travels.Add(travelQL);
					prof.Travels.Add(travelKC);
					_dbContext.SaveChanges();

					Category catBathroomStuff = new Category("Bathroom Stuff");
					Category catElectronics = new Category("Electronics");
					travelQL.Categories.Add(catBathroomStuff);
					travelQL.Categories.Add(catElectronics);

					Item toothbrush = new Item("toothbrush");
					toothbrush.Done = true;
					catBathroomStuff.Items.Add(toothbrush);

					Item laptop = new Item("laptop");
					Item phone = new Item("phone");
					catElectronics.Items.Add(laptop);
					catElectronics.Items.Add(phone);

					TravelTask refillShampoo = new TravelTask("Refill shampoo", new TimeSpan(0, 20, 0));
					travelQL.Tasks.Add(refillShampoo);

					ItineraryItem itinBoard = new ItineraryItem("Board", DateTime.Now.AddDays(1), new TimeSpan(0, 30, 0));
					travelQL.Itineraries.Add(itinBoard);

					_dbContext.SaveChanges();

					Category catSnacks = new Category("Snacks");
					travelKC.Categories.Add(catSnacks);

					Item chips = new Item("chips", 3);
					catSnacks.Items.Add(chips);
					TravelTask makePopcorn = new TravelTask("Make popcorn", new TimeSpan(0, 20, 0));
					makePopcorn.Done = true;
					travelKC.Tasks.Add(makePopcorn);

					ItineraryItem itinTaxi = new ItineraryItem("Taxi ride", DateTime.Now.AddDays(1), new TimeSpan(0, 30, 0));
					travelKC.Itineraries.Add(itinTaxi);

					_dbContext.SaveChanges();
				}

			}
		}

		private async Task RegisterUsers(IList<User> users) {
			foreach (User u in users) {
				var user = new IdentityUser() { UserName = u.Email, Email = u.Email };
				await _usermanager.CreateAsync(user, "Root1234");
			}
		}
	}
}
