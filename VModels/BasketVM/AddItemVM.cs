using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.VModels
{
    public class AddItemVM
    {
        public string UserId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}