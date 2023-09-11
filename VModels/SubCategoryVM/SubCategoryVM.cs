using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModel
{
    public class SubCategoriesVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CategoryVM Category { get; set; }
    }
}