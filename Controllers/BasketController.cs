using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAccess;
using FinalProject.Entity;
using FinalProject.VModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using FinalProject.Interfaces;
using FinalProject.Services.Enum;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BasketController : ControllerBase
    {
        public readonly DbAccess _Context;
        private readonly IJwtService _jwtService;

        public BasketController(DbAccess context, IJwtService jwtService)
        {
            _Context = context;
            _jwtService = jwtService;
        }
        /*
        Register 
        Login
        */

        //View Basket 
        [HttpGet]
        public async Task<List<Basket>> GetBasket()
        {
            return await _Context.Baskets.ToListAsync();
        }





        public async Task<Basket> Check_User_Basket(String UserId)
        {
            //Return the basket or null only
            return await _Context.Baskets.FirstOrDefaultAsync(p => p.UserId == UserId && p.Status == 1);
        }


        public async Task<BasketItems> Check_User_Item_Exisitens(String Basket_Id, String FoodId)
        {
            //Return the basket or null only
            return await _Context.BasketItems.FirstOrDefaultAsync(p => p.BasketId == Basket_Id && p.FoodId == FoodId);
        }

        //  Add Item to basket
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddItem(AddItemVM addItemVM)
        {
            if (HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                var jwtToken = token.ToString().Replace("Bearer ", "");
                var userId = _jwtService.GetUserIdFromToken(jwtToken);
                Basket basket = await Check_User_Basket(userId);
                if (basket != null)
                {
                    BasketItems item = await Check_User_Item_Exisitens(basket.Id, addItemVM.ItemId);
                    if (item != null)
                    {
                        //The Item Exisited 

                        if (item.Quantity != addItemVM.Quantity)
                        {
                            // Update Quantiy
                            item.Quantity = addItemVM.Quantity;
                            await _Context.SaveChangesAsync();
                            //Status 200
                            return Ok();
                        }

                        //do nothing just return the item exisited nothing to do 
                        //Returns 301
                        return StatusCode((int)HttpStatusCodeConstants.HttpStatusCode.ItemAlreadyInTheBasket);
                    }
                    else
                    {
                        //This food item is not exisited in this basket
                        var fooditem = new BasketItems
                        {
                            BasketId = basket.Id,
                            FoodId = addItemVM.ItemId,
                            Quantity = addItemVM.Quantity,
                        };

                        await _Context.AddAsync(fooditem);
                        await _Context.SaveChangesAsync();
                        //Returns 201
                        return Created("New item Added successfully", fooditem); ;

                    }
                }
                else
                {
                    //There is no basket for this user
                    Basket newbasket = new Basket
                    {
                        UserId = userId,
                        //  DateAdded = DateTime.Now,
                        Address = "Not Added Yet",
                        Status = 1,
                        CreatedBy = userId,
                    };

                    _Context.Baskets.Add(newbasket);
                    await _Context.SaveChangesAsync();

                    BasketItems item = new BasketItems
                    {
                        BasketId = newbasket.Id,
                        FoodId = addItemVM.ItemId,
                        Quantity = addItemVM.Quantity,
                        Price = 150

                    };

                    await _Context.BasketItems.AddAsync(item);
                    await _Context.SaveChangesAsync();

                    return Created("New item Added successfully", item); ;
                }

            }
            else
            {
                return BadRequest();
            }
        }

        // Update Item to basket
        [HttpPut("updateBasketItem")]
        public async Task<IActionResult> UpdateBasketItem(BasketItems updatedItem)
        {
            BasketItems existingItem = await _Context.BasketItems.FindAsync(updatedItem.Id);

            if (existingItem == null)
            {
                return NotFound("Item not found");
            }
            existingItem.Quantity = updatedItem.Quantity;
            existingItem.Price = updatedItem.Price;

            await _Context.SaveChangesAsync();

            return Ok();
        }
        //Remove Item from basket 

        [HttpDelete("removeFromBasket/{itemId}")]
        public async Task<IActionResult> RemoveFromBasket(int itemId)
        {
            BasketItems itemToRemove = await _Context.BasketItems.FindAsync(itemId);

            if (itemToRemove == null)
            {
                return NotFound("Item not found");
            }

            _Context.BasketItems.Remove(itemToRemove);
            await _Context.SaveChangesAsync();

            return Ok();
        }

        // View Orders 
        [HttpGet("viewOrders/{userId}")]
        public async Task<IActionResult> ViewOrders(string userId)
        {
            List<Order> orders = await _Context.Orders
                .Where(o => o.OwnerId == userId)
                .ToListAsync();

            return Ok(orders);
        }

        // Create Order 
        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            /*
                        Basket basket = await _Context.Baskets.Include(b => b.BasketItems)
                            .FirstOrDefaultAsync(b => b.Id == order.BasketId);

                        if (basket == null)
                        {
                            return NotFound("Basket not found");
                        }

                        float totalPrice = basket.BasketItems.Sum(item => item.Price);

                        order.TotalPrice = totalPrice;
                        order.CurrentStatus = "Pending";

                        _Context.Orders.Add(order);
                        await _Context.SaveChangesAsync();
            */
            return Ok();
        }




    }
}