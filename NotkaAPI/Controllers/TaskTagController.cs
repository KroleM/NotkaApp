using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Data;
using NotkaAPI.Models.Notes;

namespace NotkaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTagController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public TaskTagController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/TaskTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskTag>>> GetTaskTag()
        {
            return await _context.TaskTag.ToListAsync();
        }

        // GET: api/TaskTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskTag>> GetTaskTag(int id)
        {
            var taskTag = await _context.TaskTag.FindAsync(id);

            if (taskTag == null)
            {
                return NotFound();
            }

            return taskTag;
        }

        // PUT: api/TaskTags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskTag(int id, TaskTag taskTag)
        {
            if (id != taskTag.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskTagExists(id))
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

        // POST: api/TaskTags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskTag>> PostTaskTag(TaskTag taskTag)
        {
            _context.TaskTag.Add(taskTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskTag", new { id = taskTag.Id }, taskTag);
        }

        // DELETE: api/TaskTags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskTag(int id)
        {
            var taskTag = await _context.TaskTag.FindAsync(id);
            if (taskTag == null)
            {
                return NotFound();
            }

            _context.TaskTag.Remove(taskTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskTagExists(int id)
        {
            return _context.TaskTag.Any(e => e.Id == id);
        }
    }
}
