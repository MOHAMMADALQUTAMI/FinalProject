using Microsoft.AspNetCore.Mvc;
using FinalProject.Entity;
using Microsoft.EntityFrameworkCore;
using FinalProject.VModels;
using FinalProject.DAccess;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly DbAccess _context;
        public FoodController(DbAccess context)
        {
            _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FoodVM>>> GetFood()
        {
            var foods = await _context.Foods.Include(p => p.Owner)
            .ToListAsync();
            return foods.Select(e => new FoodVM
            {
                Name = e.Name,
                Price = e.Price,
                Description = e.Description,
                OwnerId = e.Owner.Id

            }).ToList();

        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<FoodVM> GetById(int ownerId)
        {
            var foods = await _context.Foods.FindAsync(ownerId);

            return new FoodVM
            {
                Name = foods.Name,
                Price = foods.Price,
                Description = foods.Description,
                OwnerId = foods.Owner.Id
            };

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> AddFood(FoodVM model)
        {
            var foods = new Food
            {


                OwnerId = model.Owner.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description

            };
            _context.Foods.Add(foods);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [AllowAnonymous]
        [HttpPut]
        public async Task<ActionResult> UpdateFood(FoodVM model)
        {
            var foods = await _context.Foods.FindAsync(model.Id);
            if (foods != null)
            {
                foods.Id = model.Id;
                foods.Name = model.Name;
                foods.Price = model.Price;
                foods.Description = model.Description;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task DeleteFood(int Id)
        {
            var foods = await _context.Foods.FindAsync(Id);

            _context.Foods.Remove(foods);
            await _context.SaveChangesAsync();

        }
    }
}