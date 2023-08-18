using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Entity
{
    public class SubCategory:BaseEntity
    {
         public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }
        
    }
}