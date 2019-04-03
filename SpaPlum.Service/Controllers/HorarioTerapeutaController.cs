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
    public class HorarioTerapeutaController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public HorarioTerapeutaController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/HorarioTerapeuta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioTerapeuta>>> GetHorarioTerapeuta()
        {
            return await _context.HorarioTerapeuta.ToListAsync();
        }

        // GET: api/HorarioTerapeuta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioTerapeuta>> GetHorarioTerapeuta(int id)
        {
            var horarioTerapeuta = await _context.HorarioTerapeuta.FindAsync(id);

            if (horarioTerapeuta == null)
            {
                return NotFound();
            }

            return horarioTerapeuta;
        }

        // PUT: api/HorarioTerapeuta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorarioTerapeuta(int id, HorarioTerapeuta horarioTerapeuta)
        {
            if (id != horarioTerapeuta.CodigoHorarioTerapeuta)
            {
                return BadRequest();
            }

            _context.Entry(horarioTerapeuta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioTerapeutaExists(id))
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

        // POST: api/HorarioTerapeuta
        [HttpPost]
        public async Task<ActionResult<HorarioTerapeuta>> PostHorarioTerapeuta(HorarioTerapeuta horarioTerapeuta)
        {
            _context.HorarioTerapeuta.Add(horarioTerapeuta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHorarioTerapeuta", new { id = horarioTerapeuta.CodigoHorarioTerapeuta }, horarioTerapeuta);
        }

        // DELETE: api/HorarioTerapeuta/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HorarioTerapeuta>> DeleteHorarioTerapeuta(int id)
        {
            var horarioTerapeuta = await _context.HorarioTerapeuta.FindAsync(id);
            if (horarioTerapeuta == null)
            {
                return NotFound();
            }

            _context.HorarioTerapeuta.Remove(horarioTerapeuta);
            await _context.SaveChangesAsync();

            return horarioTerapeuta;
        }

        private bool HorarioTerapeutaExists(int id)
        {
            return _context.HorarioTerapeuta.Any(e => e.CodigoHorarioTerapeuta == id);
        }
    }
}
