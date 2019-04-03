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
    public class CarrinhoItemController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public CarrinhoItemController(AgendamentoContexto context)
        {
            _context = context;
        }

        // GET: api/CarrinhoItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarrinhoItem>>> GetCarrinhoItem()
        {
            return await _context.CarrinhoItem.ToListAsync();
        }

        // GET: api/CarrinhoItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarrinhoItem>> GetCarrinhoItem(long id)
        {
            var carrinhoItem = await _context.CarrinhoItem.FindAsync(id);

            if (carrinhoItem == null)
            {
                return NotFound();
            }

            return carrinhoItem;
        }

        // PUT: api/CarrinhoItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrinhoItem(long id, CarrinhoItem carrinhoItem)
        {
            if (id != carrinhoItem.CodigoCarrinhoItem)
            {
                return BadRequest();
            }

            _context.Entry(carrinhoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrinhoItemExists(id))
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

        // POST: api/CarrinhoItem
        [HttpPost]
        public async Task<ActionResult<CarrinhoItem>> PostCarrinhoItem(CarrinhoItem carrinhoItem)
        {
            _context.CarrinhoItem.Add(carrinhoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrinhoItem", new { id = carrinhoItem.CodigoCarrinhoItem }, carrinhoItem);
        }

        // DELETE: api/CarrinhoItem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarrinhoItem>> DeleteCarrinhoItem(long id)
        {
            var carrinhoItem = await _context.CarrinhoItem.FindAsync(id);
            if (carrinhoItem == null)
            {
                return NotFound();
            }

            _context.CarrinhoItem.Remove(carrinhoItem);
            await _context.SaveChangesAsync();

            return carrinhoItem;
        }

        private bool CarrinhoItemExists(long id)
        {
            return _context.CarrinhoItem.Any(e => e.CodigoCarrinhoItem == id);
        }
    }
}
