using Hotel.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data
{
    public class HotelContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer("Server = RAHUL\\SQLEXPRESS; Database = HotelNewApp; Trusted_Connection = True; TrustServerCertificate = True;");
            optionsBuilder.UseSqlServer("Server=localhost;Database=HotelNewApp;User Id=sa;Password=Rahul@123;TrustServerCertificate=True;");
        }

        public DbSet<Customer> customers {  get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Domain.Models.Hotel> hotels { get; set; }
        public DbSet<Payment> payment { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<RoomType> roomTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasIndex(c => c.Email).IsUnique();
        }

    }
}
 