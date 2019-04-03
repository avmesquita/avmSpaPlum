using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaPlum.Entity;
using SpaPlum.Service.Context;

namespace SpaPlum.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilialController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public FilialController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/Filial
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filial>>> GetFilial()
        {
            return await _context.Filial.ToListAsync();
        }

        // GET: api/Filial/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Filial>> GetFilial(int id)
        {
            var filial = await _context.Filial.FindAsync(id);

            if (filial == null)
            {
                return NotFound();
            }

            return filial;
        }

        // PUT: api/Filial/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilial(int id, Filial filial)
        {
            if (id != filial.CodigoFilial)
            {
                return BadRequest();
            }

            _context.Entry(filial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilialExists(id))
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

        // POST: api/Filial
        [HttpPost]
        public async Task<ActionResult<Filial>> PostFilial(Filial filial)
        {
            _context.Filial.Add(filial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilial", new { id = filial.CodigoFilial }, filial);
        }

        // DELETE: api/Filial/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Filial>> DeleteFilial(int id)
        {
            var filial = await _context.Filial.FindAsync(id);
            if (filial == null)
            {
                return NotFound();
            }

            _context.Filial.Remove(filial);
            await _context.SaveChangesAsync();

            return filial;
        }

        private bool FilialExists(int id)
        {
            return _context.Filial.Any(e => e.CodigoFilial == id);
        }
    }
}
