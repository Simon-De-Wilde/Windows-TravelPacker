using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPackerAPI.Data;
using TravelPackerAPI.Models;
using TravelPackerAPI.Models.RepositoryInterfaces;

namespace TravelPackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
	public class ItemController : ControllerBase
    {
		private readonly TravelPackerDbContext _context;
		private readonly IItemRepository _itemRepository;

		public ItemController(TravelPackerDbContext context, IItemRepository itemRepository)
		{
			_context = context;
			_itemRepository = itemRepository;
		}

		/// <summary>
		/// Get all Items
		/// </summary>
		/// <returns></returns>
		// GET: api/Items
		[HttpGet]
		public IEnumerable<Item> GetItems()
		{
			return _itemRepository.GetAll();
		}

		/// <summary>
		/// Get item by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// GET: api/Items/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Item>> GetItem(int id)
		{
			var item = _itemRepository.GetById(id);

			if (item == null)
			{
				return NotFound();
			}
			return item;
		}


		/// <summary>
		/// Update an existing item
		/// </summary>
		/// <param name="id"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		// PUT: api/Items/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutItem(int id, Item item)
		{
			if (id != item.Id)
			{
				return BadRequest();
			}
			try
			{
				_itemRepository.Update(item);
				_itemRepository.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.Items.Any(e => e.Id == id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return NoContent();
		}

		/// <summary>
		/// Create a new item
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		// POST: api/Items
		[HttpPost]
		public async Task<ActionResult<Item>> PostItem(Item item)
		{
			_itemRepository.Add(item);
			_itemRepository.SaveChanges();

			return CreatedAtAction("GetItem", new { id = item.Id }, item);
		}

		/// <summary>
		/// Delete an item with a specific id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// DELETE: api/Items/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Item>> DeleteItem(int id)
		{
			var item = _itemRepository.GetById(id);
			if (item == null)
			{
				return NotFound();
			}
			_itemRepository.Delete(item);
			_itemRepository.SaveChanges();
			return item;
		}
	}
}
