namespace FinalProject.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? CreatedON { get; set; }
        public int? ModifiedOn { get; set; }
    }
}