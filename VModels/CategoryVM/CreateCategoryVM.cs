using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModel
{
    public class CreateCategoryVM
    {
        [Required]
        public string Name { get; set; }
    }
}