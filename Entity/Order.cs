
namespace FinalProject.Entity
{
    public class Order : BaseEntity
    {

        public int Phone { get; set; }

        public float TotalPrice { get; set; }

        public string CurrentStatus { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}