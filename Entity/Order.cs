
namespace FinalProject.Entity
{
    public class Order : BaseEntity2
    {

        public int Phone { get; set; }


        public float TotalPrice { get; set; }

        public int BasketId { get; set; }

        public Basket Basket { get; set; }

        public string CurrentStatus { get; set; }

        public string OwnerId { get; set; }

        public User OwnerData { get; set; }

        public DateTime CreatedAt { get; set; }




    }
}