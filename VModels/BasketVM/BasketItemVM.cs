using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.VModels
{
    public class BasketItemVM
    {
        public int BasketId { get; set; }
        public BasketVM Basket { get; set; }
        public int FoodId { get; set; }
        public FoodVM Food { get; set; }
        public int Quantity { get; set; }

        public float Price { get; set; }
    }
}