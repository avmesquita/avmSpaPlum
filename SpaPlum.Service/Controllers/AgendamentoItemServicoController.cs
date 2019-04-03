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
    public class AgendamentoItemServicoController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public AgendamentoItemServicoController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/AgendamentoItemServico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgendamentoItemServico>>> GetAgendamentoItemServico()
        {
            return await _context.AgendamentoItemServico.ToListAsync();
        }

        // GET: api/AgendamentoItemServico/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgendamentoItemServico>> GetAgendamentoItemServico(int id)
        {
            var agendamentoItemServico = await _context.AgendamentoItemServico.FindAsync(id);

            if (agendamentoItemServico == null)
            {
                return NotFound();
            }

            return agendamentoItemServico;
        }

        // PUT: api/AgendamentoItemServico/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgendamentoItemServico(int id, AgendamentoItemServico agendamentoItemServico)
        {
            if (id != agendamentoItemServico.CodigoAgendamentoItemServico)
            {
                return BadRequest();
            }

            _context.Entry(agendamentoItemServico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendamentoItemServicoExists(id))
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

        // POST: api/AgendamentoItemServico
        [HttpPost]
        public async Task<ActionResult<AgendamentoItemServico>> PostAgendamentoItemServico(AgendamentoItemServico agendamentoItemServico)
        {
            _context.AgendamentoItemServico.Add(agendamentoItemServico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgendamentoItemServico", new { id = agendamentoItemServico.CodigoAgendamentoItemServico }, agendamentoItemServico);
        }

        // DELETE: api/AgendamentoItemServico/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AgendamentoItemServico>> DeleteAgendamentoItemServico(int id)
        {
            var agendamentoItemServico = await _context.AgendamentoItemServico.FindAsync(id);
            if (agendamentoItemServico == null)
            {
                return NotFound();
            }

            _context.AgendamentoItemServico.Remove(agendamentoItemServico);
            await _context.SaveChangesAsync();

            return agendamentoItemServico;
        }

        private bool AgendamentoItemServicoExists(int id)
        {
            return _context.AgendamentoItemServico.Any(e => e.CodigoAgendamentoItemServico == id);
        }
    }
}
