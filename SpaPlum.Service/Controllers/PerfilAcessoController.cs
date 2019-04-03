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
    public class PerfilAcessoController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public PerfilAcessoController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/PerfilAcesso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfilAcesso>>> GetPerfilAcesso()
        {
            return await _context.PerfilAcesso.ToListAsync();
        }

        // GET: api/PerfilAcesso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerfilAcesso>> GetPerfilAcesso(int id)
        {
            var perfilAcesso = await _context.PerfilAcesso.FindAsync(id);

            if (perfilAcesso == null)
            {
                return NotFound();
            }

            return perfilAcesso;
        }

        // PUT: api/PerfilAcesso/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerfilAcesso(int id, PerfilAcesso perfilAcesso)
        {
            if (id != perfilAcesso.CodigoPerfilAcesso)
            {
                return BadRequest();
            }

            _context.Entry(perfilAcesso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilAcessoExists(id))
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

        // POST: api/PerfilAcesso
        [HttpPost]
        public async Task<ActionResult<PerfilAcesso>> PostPerfilAcesso(PerfilAcesso perfilAcesso)
        {
            _context.PerfilAcesso.Add(perfilAcesso);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PerfilAcessoExists(perfilAcesso.CodigoPerfilAcesso))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPerfilAcesso", new { id = perfilAcesso.CodigoPerfilAcesso }, perfilAcesso);
        }

        // DELETE: api/PerfilAcesso/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PerfilAcesso>> DeletePerfilAcesso(int id)
        {
            var perfilAcesso = await _context.PerfilAcesso.FindAsync(id);
            if (perfilAcesso == null)
            {
                return NotFound();
            }

            _context.PerfilAcesso.Remove(perfilAcesso);
            await _context.SaveChangesAsync();

            return perfilAcesso;
        }

        private bool PerfilAcessoExists(int id)
        {
            return _context.PerfilAcesso.Any(e => e.CodigoPerfilAcesso == id);
        }
    }
}
