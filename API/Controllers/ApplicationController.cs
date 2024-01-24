using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ApplicationController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Application
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationModel>>> GetApplications()
        {
          if (_context.Applications == null)
          {
              return NotFound();
          }
            return await _context.Applications.ToListAsync();
        }

        // GET: api/Application/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationModel>> GetApplicationModel(int id)
        {
          if (_context.Applications == null)
          {
              return NotFound();
          }
            var applicationModel = await _context.Applications.FindAsync(id);

            if (applicationModel == null)
            {
                return NotFound();
            }

            return applicationModel;
        }

        // PUT: api/Application/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationModel(int id, ApplicationModel applicationModel)
        {
            if (id != applicationModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicationModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationModelExists(id))
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

        // POST: api/Application
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationModel>> PostApplicationModel(ApplicationModel applicationModel)
        {
          if (_context.Applications == null)
          {
              return Problem("Entity set 'DatabaseContext.Applications'  is null.");
          }
            _context.Applications.Add(applicationModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationModel", new { id = applicationModel.Id }, applicationModel);
        }

        // DELETE: api/Application/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationModel(int id)
        {
            if (_context.Applications == null)
            {
                return NotFound();
            }
            var applicationModel = await _context.Applications.FindAsync(id);
            if (applicationModel == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(applicationModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationModelExists(int id)
        {
            return (_context.Applications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
