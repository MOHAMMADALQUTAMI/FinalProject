using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FinalProject.Entity;

namespace FinalProject.DAccess

{
    public class DbAccess : IdentityDbContext<User>
    {

        public DbSet<Food> Foods { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItems> BasketItems { get; set; }
        public DbAccess(DbContextOptions<DbAccess> options) : base(options)
        {

        }

    }
}