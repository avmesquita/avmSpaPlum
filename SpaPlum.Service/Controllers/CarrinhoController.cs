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
    public class CarrinhoController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public CarrinhoController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/Carrinho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrinho>>> GetCarrinho()
        {
            return await _context.Carrinho.ToListAsync();
        }

        // GET: api/Carrinho/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrinho>> GetCarrinho(long id)
        {
            var carrinho = await _context.Carrinho.FindAsync(id);

            if (carrinho == null)
            {
                return NotFound();
            }

            return carrinho;
        }

        // PUT: api/Carrinho/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrinho(long id, Carrinho carrinho)
        {
            if (id != carrinho.CodigoCarrinho)
            {
                return BadRequest();
            }

            _context.Entry(carrinho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrinhoExists(id))
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

        // POST: api/Carrinho
        [HttpPost]
        public async Task<ActionResult<Carrinho>> PostCarrinho(Carrinho carrinho)
        {
            _context.Carrinho.Add(carrinho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrinho", new { id = carrinho.CodigoCarrinho }, carrinho);
        }

        // DELETE: api/Carrinho/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Carrinho>> DeleteCarrinho(long id)
        {
            var carrinho = await _context.Carrinho.FindAsync(id);
            if (carrinho == null)
            {
                return NotFound();
            }

            _context.Carrinho.Remove(carrinho);
            await _context.SaveChangesAsync();

            return carrinho;
        }

        private bool CarrinhoExists(long id)
        {
            return _context.Carrinho.Any(e => e.CodigoCarrinho == id);
        }
    }
}
