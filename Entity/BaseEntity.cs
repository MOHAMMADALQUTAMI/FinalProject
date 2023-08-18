namespace FinalProject.Entity
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}