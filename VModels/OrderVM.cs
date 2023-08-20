
using FinalProject.Entity;

namespace FinalProject.VModels
{
    public class OrderVM : BaseEntity
    {
        public int Phone { get; set; }
        public float TotalPrice { get; set; }
        public string BasketId { get; set; }
        public Basket Basket { get; set; }
        public string CurrentStatus { get; set; }
        public string OwnerId { get; set; }
        public User OwnerData { get; set; }
        public DateTime CreatedAt { get; set; }

    }

}
