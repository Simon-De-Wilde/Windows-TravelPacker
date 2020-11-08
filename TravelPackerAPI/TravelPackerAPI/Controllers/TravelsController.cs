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
	public class TravelsController : ControllerBase {
		private readonly TravelPackerDbContext _context;
		private readonly ITravelRepository _travelRepo;

		public TravelsController(TravelPackerDbContext context,
		   ITravelRepository travelRepo) {
			_context = context;
			_travelRepo = travelRepo;
		}

		/// <summary>
		/// Get all Travels
		/// </summary>
		/// <returns></returns>
		// GET: api/Travels
		[HttpGet]
		public IEnumerable<Travel> GetTravels() {
			return _travelRepo.GetAll();
		}

		/// <summary>
		/// Get travel with id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// GET: api/Travels/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Travel>> GetTravel(int id) {
			var travel = _travelRepo.GetById(id);

			if (travel == null) {
				return NotFound();
			}

			return travel;
		}

		/// <summary>
		/// Update an existing travel
		/// </summary>
		/// <param name="id"></param>
		/// <param name="travel"></param>
		/// <returns></returns>
		// PUT: api/Travels/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTravel(int id, Travel travel) {
			if (id != travel.Id) {
				return BadRequest();
			}


			try {

				_travelRepo.Update(travel);
				_travelRepo.SaveChanges();

			}
			catch (DbUpdateConcurrencyException) {
				if (!TravelExists(id)) {
					return NotFound();
				}
				else {
					throw;
				}
			}

			return NoContent();
		}

		/// <summary>
		/// Add a new travel
		/// </summary>
		/// <param name="travel"></param>
		/// <returns></returns>
		// POST: api/Travels
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<ActionResult<Travel>> PostTravel(Travel travel) {
			_travelRepo.Add(travel);
			_travelRepo.SaveChanges();

			return CreatedAtAction("GetTravel", new { id = travel.Id }, travel);
		}

		/// <summary>
		/// Delete a travel with a specific id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// DELETE: api/Travels/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Travel>> DeleteTravel(int id) {
			var travel = _travelRepo.GetById(id);
			if (travel == null) {
				return NotFound();
			}

			_travelRepo.Delete(travel);
			_travelRepo.SaveChanges();

			return travel;
		}

		private bool TravelExists(int id) {
			return _travelRepo.GetAll().Any(e => e.Id == id);
		}
	}
}
