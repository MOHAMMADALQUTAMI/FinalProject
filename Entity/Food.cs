namespace FinalProject.Entity
{
    public class Food:BaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShopId {get;set;}
        public double Price {get;set;}
        public string Description {get;set;}
    }
}