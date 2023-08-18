using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAccess;
using FinalProject.Entity;
using FinalProject.VModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    [Route("[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        public readonly DbAccess _Context;
        public CustomerController (DbAccess context)
        {
            _Context = context;
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
    [HttpPost]
    public async Task <ActionResult>AddItem(AddItemVM addItemVM)
    {
            return Ok();
    }
    
    //  Add Item to basket
     [HttpPost("addToBasket")]
        public async Task<IActionResult> AddToBasket(BasketItems basketItem)
        {
            
            Basket userBasket = await _Context.Baskets.FirstOrDefaultAsync(b => b.Id == basketItem.BasketId);
            if (userBasket == null)
            {
                userBasket = new Basket
                {
                    UserId = basketItem.Id.ToString(),
                    DateAdded = DateTime.Now,
                    Status = 1 
                };
                _Context.Baskets.Add(userBasket);
            }
            userBasket.BasketItems.Add(basketItem);

            await _Context.SaveChangesAsync();

            return Ok();
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

            return Ok();
        }
}
}