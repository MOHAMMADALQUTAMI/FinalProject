using FinalProject.Entity;

namespace FinalProject.VModels
{
    public class FoodVM : BaseEntity
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}