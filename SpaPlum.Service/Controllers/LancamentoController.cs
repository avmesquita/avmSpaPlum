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
    public class LancamentoController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public LancamentoController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/Lancamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetLancamento()
        {
            return await _context.Lancamento.ToListAsync();
        }

        // GET: api/Lancamento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lancamento>> GetLancamento(int id)
        {
            var lancamento = await _context.Lancamento.FindAsync(id);

            if (lancamento == null)
            {
                return NotFound();
            }

            return lancamento;
        }

        // PUT: api/Lancamento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLancamento(int id, Lancamento lancamento)
        {
            if (id != lancamento.CodigoLancamento)
            {
                return BadRequest();
            }

            _context.Entry(lancamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LancamentoExists(id))
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

        // POST: api/Lancamento
        [HttpPost]
        public async Task<ActionResult<Lancamento>> PostLancamento(Lancamento lancamento)
        {
            _context.Lancamento.Add(lancamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLancamento", new { id = lancamento.CodigoLancamento }, lancamento);
        }

        // DELETE: api/Lancamento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lancamento>> DeleteLancamento(int id)
        {
            var lancamento = await _context.Lancamento.FindAsync(id);
            if (lancamento == null)
            {
                return NotFound();
            }

            _context.Lancamento.Remove(lancamento);
            await _context.SaveChangesAsync();

            return lancamento;
        }

        private bool LancamentoExists(int id)
        {
            return _context.Lancamento.Any(e => e.CodigoLancamento == id);
        }
    }
}
