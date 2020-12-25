using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPackerAPI.Models;
using TravelPackerAPI.Models.RepositoryInterfaces;

namespace TravelPackerAPI.Data.Repositories {
	public class CategoryRepository : ICategoryRepository {

		private readonly TravelPackerDbContext _dbContext;
		private readonly DbSet<Category> _categories;

		public CategoryRepository(TravelPackerDbContext dbcontext) {
			this._dbContext = dbcontext;
			this._categories = _dbContext.Categories;
		}


		public void Add(Category c) {
			_categories.Add(c);
		}

		public void Delete(Category c) {
			_categories.Remove(c);
		}

		public IEnumerable<Category> GetAll() {
			return _categories.Include(c => c.Items);
		}

		public Category GetById(int id) {
			return _categories.Include(c => c.Items).FirstOrDefault(c => c.Id == id);
		}

		public void SaveChanges() {
			_dbContext.SaveChanges();
		}

		public void Update(Category c) {
			_categories.Update(c);
		}
	}
}
