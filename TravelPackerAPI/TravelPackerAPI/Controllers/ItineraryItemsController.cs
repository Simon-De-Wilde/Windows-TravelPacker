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
	public class ItineraryItemsController : ControllerBase {
		private readonly TravelPackerDbContext _context;
		private readonly IItineraryItemRepository _iRepo;
		private readonly ITravelRepository _travelRepo;

		public ItineraryItemsController(TravelPackerDbContext context,
			IItineraryItemRepository iRepo,
			ITravelRepository travelRepo) {
			_context = context;
			_iRepo = iRepo;
			_travelRepo = travelRepo;
		}

		// GET: api/ItineraryItems
		[HttpGet]
		public ActionResult<IEnumerable<ItineraryItem>> GetItineraryItems() {
			return _iRepo.GetAll().ToList();
		}

		// GET: api/ItineraryItems/5
		[HttpGet("{id}")]
		public ActionResult<ItineraryItem> GetItineraryItem(int id) {
			var itineraryItem = _iRepo.GetById(id);

			if (itineraryItem == null) {
				return NotFound();
			}

			return itineraryItem;
		}

		// PUT: api/ItineraryItems/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut("{id}")]
		public IActionResult PutItineraryItem(int id, ItineraryItem itineraryItem) {
			if (id != itineraryItem.Id) {
				return BadRequest();
			}

			_iRepo.Update(itineraryItem);

			try {
				_iRepo.SaveChanges();
			}
			catch (DbUpdateConcurrencyException) {
				if (!ItineraryItemExists(id)) {
					return NotFound();
				}
				else {
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/ItineraryItems
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost("{travelId}")]
		public async Task<ActionResult<ItineraryItem>> PostItineraryItem(int travelId, ItineraryItem itineraryItem) {
			Travel t = _travelRepo.GetById(travelId);

			if (t == null) {
				return NotFound();
			}

			t.Itineraries.Add(itineraryItem);
			_travelRepo.SaveChanges();

			return CreatedAtAction("GetItineraryItem", new { id = itineraryItem.Id }, itineraryItem);
		}

		// DELETE: api/ItineraryItems/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<ItineraryItem>> DeleteItineraryItem(int id) {
			var itineraryItem = _iRepo.GetById(id);
			if (itineraryItem == null) {
				return NotFound();
			}

			_iRepo.Delete(itineraryItem);
			_iRepo.SaveChanges();

			return itineraryItem;
		}

		private bool ItineraryItemExists(int id) {
			return _context.ItineraryItems.Any(e => e.Id == id);
		}
	}
}
