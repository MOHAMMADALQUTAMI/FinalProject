
using Microsoft.AspNetCore.Mvc;
using FinalProject.Entity;
using Microsoft.EntityFrameworkCore;
using FinalProject.DAccess;
using FinalProject.VModels;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class BasketController : ControllerBase
    {
        private readonly DbAccess _context;

        public BasketController(DbAccess context)
        {
            _context = context;
        }


        [HttpGet]
        [AllowAnonymous]

        public async Task<ActionResult<IEnumerable<BasketItemVM>>> AddItem(AddItemVM AddItemVM)
        {
            var basket = await _context.Baskets
                   .Include(b => b.BasketItems)
                   .FirstOrDefaultAsync(b => b.status == 1 && b.UserId == AddItemVM.UserId);


            if (basket != null)
            {
                //Basket Does  Exisit


                var existingBasketItem = basket.BasketItems
                .FirstOrDefault(item => item.FoodId == AddItemVM.ItemId);
                if (existingBasketItem != null)
                {
                    return Ok("Item already exists in the basket.");
                }
                else
                {

                    var food = await _context.Foods.FindAsync(AddItemVM.ItemId);

                    if (food != null)
                    {
                        var newBasketItem = new BasketItems
                        {
                            BasketId = basket.Id,
                            FoodId = food.Id,
                            Quantity = AddItemVM.Quantity,
                            Price = food.Price
                        };

                        basket.BasketItems.Add(newBasketItem);
                        await _context.SaveChangesAsync();

                        return Ok(existingBasketItem);
                    }
                    else
                    {

                        return NotFound("Food item not Available.");
                    }
                }


            }
            else
            {
                //Basket Does Not Exisit

                var newbasket = new Basket
                {
                    UserId = AddItemVM.UserId,
                    DateAdded = DateTime.Now,
                    status = 1,
                    BasketItems = new List<BasketItems>()
                };

                var food = await _context.Foods.FindAsync(AddItemVM.ItemId);
                if (food != null)
                {
                    var basketitem = new BasketItems
                    {
                        BasketId = newbasket.Id,
                        FoodId = food.Id,
                        Quantity = AddItemVM.Quantity,
                        Price = food.Price
                    };
                    _context.BasketItems.Add(basketitem);
                }

                _context.Baskets.Add(newbasket);
                await _context.SaveChangesAsync();
                return Ok(newbasket);

            }
        }



    }
}