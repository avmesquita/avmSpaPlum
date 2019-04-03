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
    public class TerapeutaController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public TerapeutaController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/Terapeuta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Terapeuta>>> GetTerapeuta()
        {
            return await _context.Terapeuta.ToListAsync();
        }

        // GET: api/Terapeuta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Terapeuta>> GetTerapeuta(int id)
        {
            var terapeuta = await _context.Terapeuta.FindAsync(id);

            if (terapeuta == null)
            {
                return NotFound();
            }

            return terapeuta;
        }

        // PUT: api/Terapeuta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTerapeuta(int id, Terapeuta terapeuta)
        {
            if (id != terapeuta.CodigoTerapeuta)
            {
                return BadRequest();
            }

            _context.Entry(terapeuta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerapeutaExists(id))
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

        // POST: api/Terapeuta
        [HttpPost]
        public async Task<ActionResult<Terapeuta>> PostTerapeuta(Terapeuta terapeuta)
        {
            _context.Terapeuta.Add(terapeuta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTerapeuta", new { id = terapeuta.CodigoTerapeuta }, terapeuta);
        }

        // DELETE: api/Terapeuta/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Terapeuta>> DeleteTerapeuta(int id)
        {
            var terapeuta = await _context.Terapeuta.FindAsync(id);
            if (terapeuta == null)
            {
                return NotFound();
            }

            _context.Terapeuta.Remove(terapeuta);
            await _context.SaveChangesAsync();

            return terapeuta;
        }

        private bool TerapeutaExists(int id)
        {
            return _context.Terapeuta.Any(e => e.CodigoTerapeuta == id);
        }
    }
}
