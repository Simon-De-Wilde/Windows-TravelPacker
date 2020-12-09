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
	public class TaskController : ControllerBase
    {
		private readonly TravelPackerDbContext _context;
		private readonly ITravelRepository _travelRepository;
		private readonly ITaskRepository _taskRepository;
		
		public TaskController(TravelPackerDbContext context, ITravelRepository travelRepository, ITaskRepository taskRepository)
		{
			_context = context;
			_travelRepository = travelRepository;
			_taskRepository = taskRepository;
		}

		/// <summary>
		/// Get all Tasks
		/// </summary>
		/// <returns></returns>
		// GET: api/Tasks

		[HttpGet]
		public IEnumerable<TravelTask> GetTravelTasks()
		{
			return _taskRepository.GetAll();
		}

		/// <summary>
		/// Get task by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// GET: api/Tasks/5
		[HttpGet("{id}")]
		public async Task<ActionResult<TravelTask>> GetTravelTask(int id)
		{
			var task = _taskRepository.GetById(id);
			
			if (task == null)
			{
				return NotFound();
			}
			return task;
		}


		/// <summary>
		/// Update an existing task
		/// </summary>
		/// <param name="id"></param>
		/// <param name="task"></param>
		/// <returns></returns>
		// PUT: api/Tasks/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTask(int id, TravelTask task)
		{
			if (id != task.Id)
			{
				return BadRequest();
			}
			try
			{
				_taskRepository.Update(task);
				_taskRepository.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.Tasks.Any(e => e.Id == id))
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
		/// Create a new task for a trip
		/// </summary>
		/// <param name="task"></param>
		/// <returns></returns>
		// POST: api/Tasks
		[HttpPost("{id}")]
		public async Task<ActionResult<TravelTask>> PostTask(TravelTask task, int id)
		{
			Travel t = _travelRepository.GetById(id);
			t.Tasks.Add(task);
			_taskRepository.Add(task);
			_taskRepository.SaveChanges();
			_travelRepository.SaveChanges();

			return CreatedAtAction("GetTravelTask", new { id = task.Id }, task);
		}

		/// <summary>
		/// Delete a task with a specific id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// DELETE: api/Tasks/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<TravelTask>> DeleteTask(int id)
		{
			var task = _taskRepository.GetById(id);
			if (task == null)
			{
				return NotFound();
			}
			_taskRepository.Delete(task);
			_taskRepository.SaveChanges();
			return task;
		}
	}
}
