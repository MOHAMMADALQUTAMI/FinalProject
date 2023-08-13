using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace FinalProject.Entity
{
    public class DbContext:IdentityDbContext<User>
    {
        public DbContext(DbContextOptions<DbContext>options):base(options)
        {

        }
        public DbSet<Food> Foods {get;set;}
        public DbSet<Shop> Shops {get;set;}
        public DbSet<Category>categorys {get;set;}
        public DbSet<SubCategory> subcategories {get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            // builder.UseNpgsql("host=localhost ;port=5432; Database=FinalProject; username=postgres; password=xxxxxx ;IncludeErrorDetail=true;");
            builder.LogTo(Console.WriteLine);
        }
      

    }
}