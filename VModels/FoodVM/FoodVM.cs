namespace FinalProject.VModels
{
    public class FoodVM
    {
        public String Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

        public UserVM User { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
    }
}