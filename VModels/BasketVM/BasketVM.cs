using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.VModels;

namespace FinalProject.VModels
{
    public class BasketVM
    {
        public List<BasketItemVM> BasketItems { get; set; }

        public string UserId { get; set; }

        public UserVM User { get; set; }

        public DateTime DateAdded { get; set; }

        public string Address { get; set; }

        public int? Phone { get; set; }
        public int status { get; set; }


    }
}