using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FinalProject.Entity;

namespace FinalProject.DbAccess
{
    public class AplicationsDbContext : IdentityDbContext<User>
    {
        public AplicationsDbContext(DbContextOptions<AplicationsDbContext> options) : base(options)
        {

        }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Shop> Shops { get; set; }
       
        
    }
}