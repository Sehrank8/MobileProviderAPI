using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MobileProviderAPI.Model;

namespace MobileProviderAPI.Context
{
    public class BillingContext : DbContext
    {
        public BillingContext(DbContextOptions options) : base(options) { }

        public DbSet<BillUsage> Usages { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    Username = "admin",
                    Password = "password",
                    EmailAddress = "admin@example.com",
                    GivenName = "Admin",
                    Surname = "User",
                    Role = "Administrator"
                },
                new UserModel
                {
                    Username = "user",
                    Password = "1234",
                    EmailAddress = "user@example.com",
                    GivenName = "Standard",
                    Surname = "User",
                    Role = "User"
                }
            );
        }
    }
}