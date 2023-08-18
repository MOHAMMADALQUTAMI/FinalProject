
namespace FinalProject.Entity
{
    public class Order : BaseEntity
    {

        public int Phone { get; set; }


        public float TotalPrice { get; set; }

        public int  BasketId { get; set; }

        public Basket Basket { get; set; }

        public string CurrentStatus { get; set; }

        public string OwnerId { get; set; }

        public User OwnerData { get; set; }

        public DateTime CreatedAt { get; set; }




    }
}




















 /*
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        // Simulated data storage, replace with actual data storage logic
        private static List<Basket> baskets = new List<Basket>();
        private static List<Order> orders = new List<Order>();

        [HttpPost("addToBasket")]
        public IActionResult AddToBasket([FromBody] AddToBasketRequest request)
        {
            // Implement logic to add item to basket
            // Find or create a basket for the user
            Basket userBasket = baskets.Find(b => b.UserId == request.UserId);
            if (userBasket == null)
            {
                userBasket = new Basket
                {
                    UserId = request.UserId,
                    BasketItems = new List<BasketItem>(),
                    DateAdded = DateTime.Now
                };
                baskets.Add(userBasket);
            }

            // Add the item to the basket
            userBasket.BasketItems.Add(new BasketItem
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity
            });

            return Ok("Item added to basket successfully");
        }

        [HttpPut("updateBasketItem")]
        public IActionResult UpdateBasketItem([FromBody] UpdateBasketItemRequest request)
        {
            // Implement logic to update item in basket
            Basket userBasket = baskets.Find(b => b.UserId == request.UserId);
            if (userBasket == null)
            {
                return NotFound("Basket not found");
            }

            BasketItem itemToUpdate = userBasket.BasketItems.Find(i => i.ProductId == request.ProductId);
            if (itemToUpdate == null)
            {
                return NotFound("Item not found in the basket");
            }

            itemToUpdate.Quantity = request.NewQuantity;
            return Ok("Basket item updated successfully");
        }

        [HttpDelete("removeFromBasket")]
        public IActionResult RemoveFromBasket(string userId, int productId)
        {
            // Implement logic to remove item from basket
            Basket userBasket = baskets.Find(b => b.UserId == userId);
            if (userBasket == null)
            {
                return NotFound("Basket not found");
            }

            BasketItem itemToRemove = userBasket.BasketItems.Find(i => i.ProductId == productId);
            if (itemToRemove == null)
            {
                return NotFound("Item not found in the basket");
            }

            userBasket.BasketItems.Remove(itemToRemove);
            return Ok("Item removed from basket successfully");
        }

        [HttpGet("viewOrders")]
        public IActionResult ViewOrders(string userId)
        {
            // Implement logic to retrieve and return order history for the user
            List<Order> userOrders = orders.FindAll(o => o.OwnerId == userId);
            return Ok(userOrders);
        }

        [HttpPost("createOrder")]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            // Implement logic to create an order
            Basket userBasket = baskets.Find(b => b.UserId == request.UserId);
            if (userBasket == null || userBasket.BasketItems.Count == 0)
            {
                return BadRequest("Empty basket. Cannot create an order.");
            }

            Order newOrder = new Order
            {
                Phone = request.Phone,
                TotalPrice = CalculateTotalPrice(userBasket.BasketItems), // You need to implement this calculation
                BasketId = userBasket.BasketId,
                CurrentStatus = "Pending",
                OwnerId = userBasket.UserId,
                CreatedAt = DateTime.Now
            };

            orders.Add(newOrder);
            return Ok("Order created successfully");
        }

        private float CalculateTotalPrice(List<BasketItem> basketItems)
        {
            // Implement logic to calculate the total price based on basket items
            // Return the calculated total price
            return 0; // Placeholder, replace with actual calculation
        }
    }
}
Please note that this is a simplified example, and you'll need to replace the simulated data storage with actual data storage mechanisms like a database. Also, you'll need to implement the missing parts such as the data models (Basket, BasketItem, Order, etc.) and the actual calculations for the total price.

Make sure to adjust the namespace, routes, and other elements to match your application's structure and requirements.






*/