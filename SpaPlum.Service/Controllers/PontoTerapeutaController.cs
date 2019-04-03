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
    public class PontoTerapeutaController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public PontoTerapeutaController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/PontoTerapeuta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PontoTerapeuta>>> GetPontoTerapeuta()
        {
            return await _context.PontoTerapeuta.ToListAsync();
        }

        // GET: api/PontoTerapeuta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PontoTerapeuta>> GetPontoTerapeuta(int id)
        {
            var pontoTerapeuta = await _context.PontoTerapeuta.FindAsync(id);

            if (pontoTerapeuta == null)
            {
                return NotFound();
            }

            return pontoTerapeuta;
        }

        // PUT: api/PontoTerapeuta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPontoTerapeuta(int id, PontoTerapeuta pontoTerapeuta)
        {
            if (id != pontoTerapeuta.CodigoPonto)
            {
                return BadRequest();
            }

            _context.Entry(pontoTerapeuta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PontoTerapeutaExists(id))
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

        // POST: api/PontoTerapeuta
        [HttpPost]
        public async Task<ActionResult<PontoTerapeuta>> PostPontoTerapeuta(PontoTerapeuta pontoTerapeuta)
        {
            _context.PontoTerapeuta.Add(pontoTerapeuta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPontoTerapeuta", new { id = pontoTerapeuta.CodigoPonto }, pontoTerapeuta);
        }

        // DELETE: api/PontoTerapeuta/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PontoTerapeuta>> DeletePontoTerapeuta(int id)
        {
            var pontoTerapeuta = await _context.PontoTerapeuta.FindAsync(id);
            if (pontoTerapeuta == null)
            {
                return NotFound();
            }

            _context.PontoTerapeuta.Remove(pontoTerapeuta);
            await _context.SaveChangesAsync();

            return pontoTerapeuta;
        }

        private bool PontoTerapeutaExists(int id)
        {
            return _context.PontoTerapeuta.Any(e => e.CodigoPonto == id);
        }
    }
}
