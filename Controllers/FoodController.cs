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
using FinalProject.VModels.UpdateFood;
using System.IdentityModel.Tokens.Jwt;

namespace FinalProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class FoodController : ControllerBase
    {
        private readonly DbAccess _dbcontext;
        public FoodController(DbAccess dbcontext)
        {
            _dbcontext = dbcontext;
        }


        [AllowAnonymous]
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodVM>>> GetFoodByOwner()
        {

            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
            {
                return Unauthorized("UserId claim not found in the token.");
            }
            string userId = userIdClaim.Value;

            var foods = await _dbcontext.Foods.Where(food => food.UserId == userId).Include(p => p.User).ToListAsync();
            return foods.Select(e => new FoodVM
            {
                Id = e.Id,
                Name = e.Name,
                Price = e.Price,
                Description = e.Description,
                UserId = e.UserId

            }).ToList();

        }

        [AllowAnonymous]
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

        [Authorize]
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


        private string GetUserIdFromToken(string jwtToken)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);

            var userIdClaim = token.Claims.FirstOrDefault(claim => claim.Type == "UserId");

            if (userIdClaim != null)
            {
                return userIdClaim.Value;
            }
            else
            {
                throw new ApplicationException("UserId claim not found in JWT token.");
            }
        }


        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> UpdateFood(UpdateFood model)
        {
            if (HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                // Remove "Bearer " from the token to get the actual JWT token
                var jwtToken = token.ToString().Replace("Bearer ", "");

                // Extract UserId from the JWT token
                var userId = GetUserIdFromToken(jwtToken);

                var foods = await _dbcontext.Foods.FindAsync(model.Id);
                if (foods != null)
                {
                    string currentUserId = Convert.ToString(HttpContext.User.FindFirst("UserId"));

                    if (foods.UserId == userId)
                    {

                        foods.Id = model.Id;
                        foods.Name = model.Name;
                        foods.Price = model.Price;
                        foods.Description = model.Description;
                        await _dbcontext.SaveChangesAsync();

                        return Ok();
                    }
                    return Unauthorized();
                }
                else
                {
                    return NoContent();
                }

            }
            else
            {
                // Handle the case where the "Authorization" header is not present
                return BadRequest("Authorization header not found.");
            }

        }





        [Authorize]
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