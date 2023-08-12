using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly FinalProject.Entity.DbContext _context;

        public BasketController(FinalProject.Entity.DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Basket>>> GetBaskets()
        {
            return await _context.Baskets.Include(b => b.Food).ToListAsync();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Basket>>> GetBasketsByUserId(int userId)
        {
            var baskets = await _context.Baskets
                .Where(b => b.UserId == userId)
                .Include(b => b.Food)
                .ToListAsync();

            if (baskets == null || baskets.Count == 0)
            {
                return NotFound();
            }

            return baskets;
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> PostBasket(Basket basket)
        {
            var existingBasketItem = await _context.Baskets
                .FirstOrDefaultAsync(b => b.UserId == basket.UserId && b.FoodId == basket.FoodId);

            if (existingBasketItem != null)
            {
                existingBasketItem.Quintity += basket.Quintity;
                _context.Entry(existingBasketItem).State = EntityState.Modified;
            }
            else
            {
                _context.Baskets.Add(basket);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBasket", new { id = basket.Id }, basket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBasket(int id, Basket basket)
        {
            if (id != basket.Id)
            {
                return BadRequest();
            }

            _context.Entry(basket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BasketExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(int id)
        {
            var basket = await _context.Baskets.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }

            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BasketExists(int id)
        {
            return _context.Baskets.Any(b => b.Id == id);
        }
    }
}