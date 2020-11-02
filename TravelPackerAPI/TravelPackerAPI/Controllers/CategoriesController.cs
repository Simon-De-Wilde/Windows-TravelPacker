using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPackerAPI.Data;
using TravelPackerAPI.Models;
using TravelPackerAPI.Models.RepositoryInterfaces;

namespace TravelPackerAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class CategoriesController : ControllerBase {
		private readonly TravelPackerDbContext _context;
		private readonly ICategoryRepository _categoryRepo;

		public CategoriesController(TravelPackerDbContext context,
			ICategoryRepository categoryRepo) {
			_context = context;
			_categoryRepo = categoryRepo;
		}

		/// <summary>
		/// Get all Categories
		/// </summary>
		/// <returns></returns>
		// GET: api/Categories
		[HttpGet]
		public IEnumerable<Category> GetCategories() {
			return _categoryRepo.GetAll();
		}

		/// <summary>
		/// Get category by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// GET: api/Categories/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Category>> GetCategory(int id) {
			var category = _categoryRepo.GetById(id);

			if (category == null) {
				return NotFound();
			}

			return category;
		}

		/// <summary>
		/// Update an existing categorie
		/// </summary>
		/// <param name="id"></param>
		/// <param name="category"></param>
		/// <returns></returns>
		// PUT: api/Categories/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCategory(int id, Category category) {
			if (id != category.Id) {
				return BadRequest();
			}

			try {
				_categoryRepo.Update(category);
				_categoryRepo.SaveChanges();
			}
			catch (DbUpdateConcurrencyException) {
				if (!CategoryExists(id)) {
					return NotFound();
				}
				else {
					throw;
				}
			}

			return NoContent();
		}

		/// <summary>
		/// Create a new category
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		// POST: api/Categories
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<ActionResult<Category>> PostCategory(Category category) {
			_categoryRepo.Add(category);
			_categoryRepo.SaveChanges();

			return CreatedAtAction("GetCategory", new { id = category.Id }, category);
		}

		/// <summary>
		/// Delete a category with a specific id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// DELETE: api/Categories/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Category>> DeleteCategory(int id) {
			var category = _categoryRepo.GetById(id);
			if (category == null) {
				return NotFound();
			}

			_categoryRepo.Delete(category);
			_categoryRepo.SaveChanges();

			return category;
		}

		private bool CategoryExists(int id) {
			return _context.Categories.Any(e => e.Id == id);
		}
	}
}
