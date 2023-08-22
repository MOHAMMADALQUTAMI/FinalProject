using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Entity;
using FinalProject.DAccess;
using FinalProject.VModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FinalProject.Controllers
{
    [Route("[controller]/[action]")]
    public class FoodController : ControllerBase
    {
        private readonly DbAccess _dbcontext;
        public FoodController(DbAccess dbcontext)
        {
            _dbcontext = dbcontext;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodVM>>> GetFood()
        {
            var foods = await _dbcontext.Foods.Include(p => p.User)
            .ToListAsync();
            return foods.Select(e => new FoodVM
            {
                Id = e.Id,
                Name = e.Name,
                Price = e.Price,
                Description = e.Description,
                UserId = e.UserId

            }).ToList();

        }

        [HttpGet]
        public async Task<FoodVM> GetById(int FoodId)
        {
            var foods = await _dbcontext.Foods.FindAsync(FoodId);

            return new FoodVM
            {
                Id = foods.Id,
                Name = foods.Name,
                Price = foods.Price,
                Description = foods.Description,
                UserId = foods.UserId,
                User = new UserVM
                {

                }


            };

        }

        [HttpPost]
        public async Task<IActionResult> AddFood(AddFoodVM model)
        {
            var foods = new Food
            {
                Name = model.Name,
                UserId = model.UserId,
                Price = model.Price,
                Description = model.Description
            };
            _dbcontext.Foods.Add(foods);
            await _dbcontext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFood(FoodVM model)
        {
            var foods = await _dbcontext.Foods.FindAsync(model.Id);
            if (foods != null)
            {
                foods.Id = model.Id;
                foods.Name = model.Name;
                foods.Price = model.Price;
                foods.Description = model.Description;
                await _dbcontext.SaveChangesAsync();

                return Ok();

            }
            else
            {
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int Id)
        {
            var foods = await _dbcontext.Foods.FindAsync(Id);

            if (foods == null)
            {
                return NotFound();
            }

            _dbcontext.Foods.Remove(foods);
            await _dbcontext.SaveChangesAsync();

            return NoContent();


        }



    }
}