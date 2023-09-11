
using System.ComponentModel.DataAnnotations;

namespace FinalProject.VModels
{
    public class AddFoodVM
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public string Description { get; set; }

    }
}