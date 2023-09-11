using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Entity
{
    public class Basket : BaseEntity
    {



        public List<BasketItems>? BasketItems { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime? DateAdded { get; set; }

        public string Address { get; set; }

        public int? Phone { get; set; }
        public int Status { get; set; }


    }
}