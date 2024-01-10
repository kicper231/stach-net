using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ShopperContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ShopperContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
           .HasKey(u => u.UserId);
            modelBuilder.Entity<User>()
            .Property(u => u.FirstName)
            .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.LastName);
        }
    }
    

}
