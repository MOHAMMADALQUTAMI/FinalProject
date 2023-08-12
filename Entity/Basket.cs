using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Entity
{
    public class Basket : BaseEntity
    {



        public int Id { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public String Address { get; set; }
        public int Quintity { get; set; }

        public DateTime DateAdded { get; set; }


    }
}