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
    public class TerapeutaServicoController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public TerapeutaServicoController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/TerapeutaServico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TerapeutaServico>>> GetTerapeutaServico()
        {
            return await _context.TerapeutaServico.ToListAsync();
        }

        // GET: api/TerapeutaServico/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TerapeutaServico>> GetTerapeutaServico(int id)
        {
            var terapeutaServico = await _context.TerapeutaServico.FindAsync(id);

            if (terapeutaServico == null)
            {
                return NotFound();
            }

            return terapeutaServico;
        }

        // PUT: api/TerapeutaServico/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTerapeutaServico(int id, TerapeutaServico terapeutaServico)
        {
            if (id != terapeutaServico.CodigoTerapeutaServico)
            {
                return BadRequest();
            }

            _context.Entry(terapeutaServico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerapeutaServicoExists(id))
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

        // POST: api/TerapeutaServico
        [HttpPost]
        public async Task<ActionResult<TerapeutaServico>> PostTerapeutaServico(TerapeutaServico terapeutaServico)
        {
            _context.TerapeutaServico.Add(terapeutaServico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTerapeutaServico", new { id = terapeutaServico.CodigoTerapeutaServico }, terapeutaServico);
        }

        // DELETE: api/TerapeutaServico/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TerapeutaServico>> DeleteTerapeutaServico(int id)
        {
            var terapeutaServico = await _context.TerapeutaServico.FindAsync(id);
            if (terapeutaServico == null)
            {
                return NotFound();
            }

            _context.TerapeutaServico.Remove(terapeutaServico);
            await _context.SaveChangesAsync();

            return terapeutaServico;
        }

        private bool TerapeutaServicoExists(int id)
        {
            return _context.TerapeutaServico.Any(e => e.CodigoTerapeutaServico == id);
        }
    }
}
