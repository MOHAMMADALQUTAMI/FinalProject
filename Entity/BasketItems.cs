using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Entity
{
    public class BasketItems : BaseEntity
    {

        public string BasketId { get; set; }
        public Basket Basket { get; set; }
        public String FoodId { get; set; }
        public Food Food { get; set; }
        public int Quantity { get; set; }

        public float Price { get; set; }

    }
}