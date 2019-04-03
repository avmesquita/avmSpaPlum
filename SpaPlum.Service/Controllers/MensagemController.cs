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
    public class MensagemController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public MensagemController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/Mensagem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mensagem>>> GetMensagem()
        {
            return await _context.Mensagem.ToListAsync();
        }

        // GET: api/Mensagem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mensagem>> GetMensagem(int id)
        {
            var mensagem = await _context.Mensagem.FindAsync(id);

            if (mensagem == null)
            {
                return NotFound();
            }

            return mensagem;
        }

        // PUT: api/Mensagem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMensagem(int id, Mensagem mensagem)
        {
            if (id != mensagem.CodigoMensagem)
            {
                return BadRequest();
            }

            _context.Entry(mensagem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MensagemExists(id))
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

        // POST: api/Mensagem
        [HttpPost]
        public async Task<ActionResult<Mensagem>> PostMensagem(Mensagem mensagem)
        {
            _context.Mensagem.Add(mensagem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMensagem", new { id = mensagem.CodigoMensagem }, mensagem);
        }

        // DELETE: api/Mensagem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Mensagem>> DeleteMensagem(int id)
        {
            var mensagem = await _context.Mensagem.FindAsync(id);
            if (mensagem == null)
            {
                return NotFound();
            }

            _context.Mensagem.Remove(mensagem);
            await _context.SaveChangesAsync();

            return mensagem;
        }

        private bool MensagemExists(int id)
        {
            return _context.Mensagem.Any(e => e.CodigoMensagem == id);
        }
    }
}
