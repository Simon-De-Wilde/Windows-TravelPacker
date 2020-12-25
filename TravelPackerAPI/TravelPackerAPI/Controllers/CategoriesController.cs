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
		private readonly ITravelRepository _travelRepo;

		public CategoriesController(
			TravelPackerDbContext context,
			ICategoryRepository categoryRepo,
			ITravelRepository travelRepo) {
			_context = context;
			_categoryRepo = categoryRepo;
			_travelRepo = travelRepo;
		}

		/// <summary>
		/// Get all Categories from a travel
		/// </summary>
		/// <param name="travelId"></param>
		/// <returns></returns>
		// GET: api/Categories
		[HttpGet("[action]/{travelId}")]
		public ActionResult<IEnumerable<Category>> GetCategoriesFromTravel(int travelId) {

			Travel travel = _travelRepo.GetById(travelId);

			if (travel == null) {
				return NotFound("Travel not found");
			}

			return travel.Categories.ToList();
			;
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
				return NotFound("Categorie not found");
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
				return BadRequest("Id's don't match");
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
		/// Create a new category to a travel
		/// </summary>
		/// <param name="category"></param>
		/// <param name="travelId"></param>
		/// <returns></returns>
		// POST: api/Categories
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost("[action]/{travelId}")]
		public async Task<ActionResult<Category>> PostCategoryToTravel(int travelId, Category category) {

			Travel travel = _travelRepo.GetById(travelId);

			if (travel == null) {
				return NotFound("Travel not found");
			}

			travel.Categories.Add(category);
			_travelRepo.SaveChanges();

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
