using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FinalProject.ViewModel.CategoryVM
{
    public class SubCategoryVM
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public CategoryVM Category { get; set; }
    }
}