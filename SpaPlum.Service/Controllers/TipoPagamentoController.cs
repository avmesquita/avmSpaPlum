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
    public class TipoPagamentoController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public TipoPagamentoController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/TipoPagamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoPagamento>>> GetTipoPagamento()
        {
            return await _context.TipoPagamento.ToListAsync();
        }

        // GET: api/TipoPagamento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoPagamento>> GetTipoPagamento(int id)
        {
            var tipoPagamento = await _context.TipoPagamento.FindAsync(id);

            if (tipoPagamento == null)
            {
                return NotFound();
            }

            return tipoPagamento;
        }

        // PUT: api/TipoPagamento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoPagamento(int id, TipoPagamento tipoPagamento)
        {
            if (id != tipoPagamento.CodigoTipoPagamento)
            {
                return BadRequest();
            }

            _context.Entry(tipoPagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoPagamentoExists(id))
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

        // POST: api/TipoPagamento
        [HttpPost]
        public async Task<ActionResult<TipoPagamento>> PostTipoPagamento(TipoPagamento tipoPagamento)
        {
            _context.TipoPagamento.Add(tipoPagamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoPagamento", new { id = tipoPagamento.CodigoTipoPagamento }, tipoPagamento);
        }

        // DELETE: api/TipoPagamento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoPagamento>> DeleteTipoPagamento(int id)
        {
            var tipoPagamento = await _context.TipoPagamento.FindAsync(id);
            if (tipoPagamento == null)
            {
                return NotFound();
            }

            _context.TipoPagamento.Remove(tipoPagamento);
            await _context.SaveChangesAsync();

            return tipoPagamento;
        }

        private bool TipoPagamentoExists(int id)
        {
            return _context.TipoPagamento.Any(e => e.CodigoTipoPagamento == id);
        }
    }
}
