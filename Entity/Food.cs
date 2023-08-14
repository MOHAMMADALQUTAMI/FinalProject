namespace FinalProject.Entity
{
    public class Food : BaseEntity
    {
        public string Name { get; set; }
        public string ShopId { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
    }
}