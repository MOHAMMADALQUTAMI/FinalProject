namespace FinalProject.Entity
{
    public class Food : BaseEntity
    {
        
        public string Name { get; set; }
        public Shop Shop { get; set; }
        public int ShopId { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}