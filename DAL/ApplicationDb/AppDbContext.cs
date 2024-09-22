using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ApplicationDb
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
            public DbSet<Product> Product_ { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Description = "A test product for testing end points.",
                    Price = 34.5M,
                    Category = "Test Cat",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = null,
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

