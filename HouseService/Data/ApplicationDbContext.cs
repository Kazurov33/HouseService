using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HouseService.Models.Entities;
namespace HouseService.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity(typeof(AdressModel))
            .HasOne(typeof(Region), "Region")
            .WithMany()
            .HasForeignKey("RegionId")
            .OnDelete(DeleteBehavior.Restrict); // no ON DELETE
                                                //
            modelbuilder.Entity(typeof(AdressModel))
            .HasOne(typeof(CompanyModel), "Company")
            .WithMany()
            .HasForeignKey("CompanyId")
            .OnDelete(DeleteBehavior.Restrict); // no ON DELETE
                                                //
            modelbuilder.Entity(typeof(AdressModel))
            .HasOne(typeof(City), "City")
            .WithMany()
            .HasForeignKey("CityId")
            .OnDelete(DeleteBehavior.Restrict); // no ON DELETE
                                             //
            modelbuilder.Entity(typeof(AdressModel))
            .HasOne(typeof(Street), "Street")
            .WithMany()
            .HasForeignKey("StreetId")
            .OnDelete(DeleteBehavior.Restrict); // no ON DELETE

            modelbuilder.Entity(typeof(AdressModel))
            .HasOne(typeof(House), "House")
            .WithMany()
            .HasForeignKey("HouseId")
            .OnDelete(DeleteBehavior.Restrict); // no ON DELETE

            modelbuilder.Entity(typeof(AdressModel))
            .HasOne(typeof(Room), "Room")
            .WithMany()
            .HasForeignKey("RoomId")
            .OnDelete(DeleteBehavior.Restrict); // no ON DELETE


        }
        public DbSet<CompanyModel> Companies { get; set; }

        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Room> Rooms { get; set; }


        public DbSet<Request> Requests { get; set; }

        public DbSet<AdressModel> Adresses { get; set; }
        public DbSet<RequestStatusModel> RequestStatuses { get; set; }
        public DbSet<RequestTypeModel> RequestTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
