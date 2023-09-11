using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Entity;
using FinalProject.DAccess;
using FinalProject.VModels;
using Microsoft.AspNetCore.Authorization;
using FinalProject.VModels.UpdateFood;
using FinalProject.Interfaces;


namespace FinalProject.Controllers

{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class FoodController : ControllerBase
    {
        private readonly DbAccess _dbcontext;
        private readonly IJwtService _jwtService;
        public FoodController(DbAccess dbcontext, IJwtService jwtService)
        {
            _dbcontext = dbcontext;
            _jwtService = jwtService;

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
                UserId = e.UserId,
                User = new UserVM
                {
                    Id = e.User.Id,
                    UserName = e.User.UserName,
                }
            }).ToList();

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodVM>>> GetFoodByOwner()
        {

            if (HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                var jwtToken = token.ToString().Replace("Bearer ", "");
                var userId = _jwtService.GetUserIdFromToken(jwtToken);

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
            else
            {
                return BadRequest();
            }

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

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<FoodVM>> AddFood([FromBody] AddFoodVM model)
        {
            if (HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                var jwtToken = token.ToString().Replace("Bearer ", "");
                var userId = _jwtService.GetUserIdFromToken(jwtToken);

                if (ModelState.IsValid)
                {
                    Food foods = new Food
                    {
                        Name = model.Name,
                        UserId = userId,
                        Price = model.Price,
                        Description = model.Description
                    };
                    _dbcontext.Foods.Add(foods);
                    await _dbcontext.SaveChangesAsync();

                    var foodVM = new FoodVM
                    {
                        Id = foods.Id,
                        Name = foods.Name,
                        UserId = foods.UserId,
                        Price = foods.Price,
                        Description = foods.Description
                    };
                    return Ok(foodVM);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest("Authorization header not found.");
            }
        }





        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> UpdateFood(UpdateFood model)
        {
            if (HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                var jwtToken = token.ToString().Replace("Bearer ", "");
                var userId = _jwtService.GetUserIdFromToken(jwtToken);

                var foods = await _dbcontext.Foods.FindAsync(model.Id);
                if (foods != null)
                {
                    if (foods.UserId == userId)
                    {

                        foods.Id = model.Id;
                        foods.Name = model.Name;
                        foods.Price = model.Price;
                        foods.Description = model.Description;
                        //foods.ModifiedBy = userId;
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