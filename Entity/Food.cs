namespace FinalProject.Entity
{
    public class Food : BaseEntity2
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }




    }
}