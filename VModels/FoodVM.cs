using FinalProject.Entity;

namespace FinalProject.VModels
{
    public class FoodVM:BaseEntity
    {
        public string Name { get; set; }
        public int ShopId { get; set; }
        public string Shop { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public ShopVM ShopVM { get; set; }
    }
}