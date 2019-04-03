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
    public class PromocaoController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public PromocaoController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/Promocao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promocao>>> GetPromocao()
        {
            return await _context.Promocao.ToListAsync();
        }

        // GET: api/Promocao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Promocao>> GetPromocao(int id)
        {
            var promocao = await _context.Promocao.FindAsync(id);

            if (promocao == null)
            {
                return NotFound();
            }

            return promocao;
        }

        // PUT: api/Promocao/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromocao(int id, Promocao promocao)
        {
            if (id != promocao.CodigoPromocao)
            {
                return BadRequest();
            }

            _context.Entry(promocao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromocaoExists(id))
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

        // POST: api/Promocao
        [HttpPost]
        public async Task<ActionResult<Promocao>> PostPromocao(Promocao promocao)
        {
            _context.Promocao.Add(promocao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPromocao", new { id = promocao.CodigoPromocao }, promocao);
        }

        // DELETE: api/Promocao/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Promocao>> DeletePromocao(int id)
        {
            var promocao = await _context.Promocao.FindAsync(id);
            if (promocao == null)
            {
                return NotFound();
            }

            _context.Promocao.Remove(promocao);
            await _context.SaveChangesAsync();

            return promocao;
        }

        private bool PromocaoExists(int id)
        {
            return _context.Promocao.Any(e => e.CodigoPromocao == id);
        }
    }
}
