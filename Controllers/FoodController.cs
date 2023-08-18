using Microsoft.AspNetCore.Mvc;
using FinalProject.Entity;
using Microsoft.EntityFrameworkCore;
using FinalProject.DAccess;
using FinalProject.VModels;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    [Route("[controller]/[action]")]
    public class FoodController
    {
        private readonly DbAccess _dbcontext;
        public FoodController(DbAccess dbcontext)
        {
            _dbcontext = dbcontext;
        }



        /*  [HttpGet]
          public async Task<ActionResult<IEnumerable<FoodVM>>> GetFood()
          {
              var foods = await _dbcontext.Foods.Include(p => p.Shop)
              .ToListAsync();
              return foods.Select(e => new FoodVM
              {
                  Name = e.Name,
                  Price = e.Price,
                  Description = e.Description,
                  ShopId = e.Shop.Id
              }).ToList();

          }

          [HttpGet("{id}")]
          public async Task<FoodVM> GetById(int shopId)
          {
              var foods = await _dbcontext.Foods.FindAsync(shopId);

              return new FoodVM
              {
                  Name = foods.Name,
                  Price = foods.Price,
                  Description = foods.Description,
                  ShopId = foods.Shop.Id
              };

          }
          [HttpPost]
          public async Task<ActionResult> AddFood(FoodVM model)
          {
              var foods = new Food
              {


                  ShopId = model.Id,
                  Name = model.Name,
                  Price = model.Price,
                  Description = model.Description

              };
              _dbcontext.Foods.Add(foods);
              await _dbcontext.SaveChangesAsync();
              return Ok();
          }
          [HttpPut]
          public async Task<ActionResult> UpdateFood(FoodVM model)
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

          */
        [HttpDelete("{id}")]
        public async Task DeleteFood(int Id)
        {
            var foods = await _dbcontext.Foods.FindAsync(Id);

            _dbcontext.Foods.Remove(foods);
            await _dbcontext.SaveChangesAsync();

        }

        [HttpGet]
        public string Getdata()
        {
            return "asdasdasdasd";

        }


    }
}