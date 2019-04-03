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
    public class PlanoController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public PlanoController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/Plano
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plano>>> GetPlano()
        {
            return await _context.Plano.ToListAsync();
        }

        // GET: api/Plano/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plano>> GetPlano(int id)
        {
            var plano = await _context.Plano.FindAsync(id);

            if (plano == null)
            {
                return NotFound();
            }

            return plano;
        }

        // PUT: api/Plano/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlano(int id, Plano plano)
        {
            if (id != plano.CodigoPlano)
            {
                return BadRequest();
            }

            _context.Entry(plano).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanoExists(id))
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

        // POST: api/Plano
        [HttpPost]
        public async Task<ActionResult<Plano>> PostPlano(Plano plano)
        {
            _context.Plano.Add(plano);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlano", new { id = plano.CodigoPlano }, plano);
        }

        // DELETE: api/Plano/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Plano>> DeletePlano(int id)
        {
            var plano = await _context.Plano.FindAsync(id);
            if (plano == null)
            {
                return NotFound();
            }

            _context.Plano.Remove(plano);
            await _context.SaveChangesAsync();

            return plano;
        }

        private bool PlanoExists(int id)
        {
            return _context.Plano.Any(e => e.CodigoPlano == id);
        }
    }
}
