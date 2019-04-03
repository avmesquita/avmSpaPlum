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
    public class PromocaoClienteController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public PromocaoClienteController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/PromocaoCliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromocaoCliente>>> GetPromocaoCliente()
        {
            return await _context.PromocaoCliente.ToListAsync();
        }

        // GET: api/PromocaoCliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PromocaoCliente>> GetPromocaoCliente(int id)
        {
            var promocaoCliente = await _context.PromocaoCliente.FindAsync(id);

            if (promocaoCliente == null)
            {
                return NotFound();
            }

            return promocaoCliente;
        }

        // PUT: api/PromocaoCliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromocaoCliente(int id, PromocaoCliente promocaoCliente)
        {
            if (id != promocaoCliente.CodigoPromocaoCliente)
            {
                return BadRequest();
            }

            _context.Entry(promocaoCliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromocaoClienteExists(id))
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

        // POST: api/PromocaoCliente
        [HttpPost]
        public async Task<ActionResult<PromocaoCliente>> PostPromocaoCliente(PromocaoCliente promocaoCliente)
        {
            _context.PromocaoCliente.Add(promocaoCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPromocaoCliente", new { id = promocaoCliente.CodigoPromocaoCliente }, promocaoCliente);
        }

        // DELETE: api/PromocaoCliente/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PromocaoCliente>> DeletePromocaoCliente(int id)
        {
            var promocaoCliente = await _context.PromocaoCliente.FindAsync(id);
            if (promocaoCliente == null)
            {
                return NotFound();
            }

            _context.PromocaoCliente.Remove(promocaoCliente);
            await _context.SaveChangesAsync();

            return promocaoCliente;
        }

        private bool PromocaoClienteExists(int id)
        {
            return _context.PromocaoCliente.Any(e => e.CodigoPromocaoCliente == id);
        }
    }
}
