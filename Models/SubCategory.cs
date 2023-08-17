using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class SubCategory
    {
          public int Id { get; set; }
        public string name { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}