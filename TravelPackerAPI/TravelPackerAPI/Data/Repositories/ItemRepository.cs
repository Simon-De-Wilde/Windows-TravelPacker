using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPackerAPI.Models;
using TravelPackerAPI.Models.RepositoryInterfaces;

namespace TravelPackerAPI.Data.Repositories {
	public class ItemRepository : IItemRepository {

		private readonly TravelPackerDbContext _dbContext;
		private readonly DbSet<Item> _items;

		public ItemRepository(TravelPackerDbContext dbcontext) {
			this._dbContext = dbcontext;
			this._items = _dbContext.Items;
		}


		public void Add(Item i) {
			_items.Add(i);
		}

		public void Delete(Item i) {
			_items.Remove(i);
		}

		public IEnumerable<Item> GetAll() {
			return _items;
		}

		public Item GetById(int id) {
			return _items.FirstOrDefault(i => i.Id == id);
		}

		public void SaveChanges() {
			_dbContext.SaveChanges();
		}

		public void Update(Item i) {
			_items.Update(i);
		}
	}
}
