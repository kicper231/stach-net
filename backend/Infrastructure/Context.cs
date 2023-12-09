using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

 // Zbiory reprezentujące tabele w bazie danych

    public class ShopperContext : DbContext
    {


       
        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<DeliveryRequest> DeliveryRequests { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<CourierCompany> CourierCompanies { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        
        public ShopperContext(DbContextOptions options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Package
        modelBuilder.Entity<Package>()
            .HasKey(p => p.PackageId);
        modelBuilder.Entity<Package>()
            .Property(p => p.Dimensions)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder.Entity<Package>()
            .Property(p => p.Weight)
            .IsRequired();

        // Address
        modelBuilder.Entity<Address>()
            .HasKey(a => a.AddressId);
        modelBuilder.Entity<Address>()
            .Property(a => a.Street)
            .HasMaxLength(200)
            .IsRequired();
        modelBuilder.Entity<Address>()
            .Property(a => a.City)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder.Entity<Address>()
            .Property(a => a.PostalCode)
            .HasMaxLength(20)
            .IsRequired();
        modelBuilder.Entity<Address>()
            .Property(a => a.Country)
            .HasMaxLength(100)
            .IsRequired();

        // DeliveryRequest
        modelBuilder.Entity<DeliveryRequest>()
            .HasKey(dr => dr.RequestId);
        modelBuilder.Entity<DeliveryRequest>()
            .HasOne(dr => dr.User)
            .WithMany()
            .HasForeignKey(dr => dr.UserId)
            .OnDelete(DeleteBehavior.Restrict); 
        modelBuilder.Entity<DeliveryRequest>()
            .HasOne(dr => dr.Package)
            .WithMany() 
            .HasForeignKey(dr => dr.PackageId);

        modelBuilder.Entity<DeliveryRequest>()
               .HasOne(dr => dr.SourceAddress)
               .WithMany()
              .HasForeignKey(dr => dr.SourceAddressId)
              .OnDelete(DeleteBehavior.Restrict);
    
        modelBuilder.Entity<DeliveryRequest>()
            .HasOne(dr => dr.DestinationAddress)
            .WithMany()
            .HasForeignKey(dr => dr.DestinationAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        // Offer
        modelBuilder.Entity<Offer>()
            .HasKey(o => o.OfferId);
       
        modelBuilder.Entity<Offer>()
            .HasOne(o => o.CourierCompany)
            .WithMany() 
            .HasForeignKey(o => o.CourierCompanyId);
        modelBuilder.Entity<Offer>()
        .Property(o => o.Price)
        .HasColumnType("decimal(18, 2)")
        .IsRequired();

        modelBuilder.Entity<Offer>()
           .HasOne(o => o.DeliveryRequest)
           .WithMany()
           .HasForeignKey(o => o.DeliveryRequestId)
            .OnDelete(DeleteBehavior.Restrict);
        // CourierCompany
        modelBuilder.Entity<CourierCompany>()
            .HasKey(cc => cc.CourierCompanyId);
        modelBuilder.Entity<CourierCompany>()
            .Property(cc => cc.Name)
            .HasMaxLength(200)
            .IsRequired();
        modelBuilder.Entity<CourierCompany>()
            .Property(cc => cc.ContactInfo)
            .HasMaxLength(200);

        // Delivery
        modelBuilder.Entity<Delivery>()
            .HasKey(d => d.DeliveryId);
        modelBuilder.Entity<Delivery>()
            .HasOne(d => d.Offer)
            .WithMany()
            .HasForeignKey(d => d.OfferId)
             .OnDelete(DeleteBehavior.Restrict); // nie wiem to bez sensu 
        modelBuilder.Entity<Delivery>()
            .HasOne(d => d.Courier)
            .WithMany() 
            .HasForeignKey(d => d.CourierId);

        // User
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);
        modelBuilder.Entity<User>()
            .Property(u => u.FirstName)
            .HasMaxLength(100);
        modelBuilder.Entity<User>()
            .Property(u => u.LastName)
            .HasMaxLength(100);
        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasMaxLength(200)
            .IsRequired();


       // indeksy??
        modelBuilder.Entity<DeliveryRequest>()
            .HasIndex(dr => dr.UserId);

       
        modelBuilder.Entity<Offer>()
            .HasIndex(o => o.CourierCompanyId);

       
        modelBuilder.Entity<Delivery>()
            .HasIndex(d => d.CourierId);





        //data seed 

        modelBuilder.Entity<User>().HasData(
        new User { UserId = 1, Auth0Id = "auth0-id-1", FirstName = "Jan", LastName = "Kowalski", Email = "jan.kowalski@example.com" },
        new User { UserId = 2, Auth0Id = "auth0-id-2", FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@example.com" }
       
    );
    }




}



