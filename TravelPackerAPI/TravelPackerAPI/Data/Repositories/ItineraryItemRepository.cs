using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPackerAPI.Models;
using TravelPackerAPI.Models.RepositoryInterfaces;

namespace TravelPackerAPI.Data.Repositories {
	public class ItineraryItemRepository : IItineraryItemRepository {


		private readonly TravelPackerDbContext _dbContext;
		private readonly DbSet<ItineraryItem> _itineraryItems;

		public ItineraryItemRepository(TravelPackerDbContext dbcontext) {
			this._dbContext = dbcontext;
			this._itineraryItems = _dbContext.ItineraryItems;
		}

		public void Add(ItineraryItem i) {
			_itineraryItems.Add(i);
		}

		public void Delete(ItineraryItem i) {
			_itineraryItems.Remove(i);
		}

		public IEnumerable<ItineraryItem> GetAll() {
			return _itineraryItems;
		}

		public ItineraryItem GetById(int id) {
			return _itineraryItems.FirstOrDefault(i => i.Id == id);
		}

		public void SaveChanges() {
			_dbContext.SaveChanges();
		}

		public void Update(ItineraryItem i) {
			_itineraryItems.Update(i);
		}
	}
}
