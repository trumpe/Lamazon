using Lamazon.Domain.Enums;
using Lamazon.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lamazon.DataAccess
{
    public class LamazonDbContext : IdentityDbContext<User>
    {
        public LamazonDbContext(DbContextOptions opts) : base(opts) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrdersProducts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrdersProducts)
                .WithOne(op => op.Order)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrdersProducts)
                .WithOne(op => op.Product)
                .HasForeignKey(op => op.ProductId);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Order)
                .WithOne();

            //Data Seed Start

            //Seeding Roles
            //Generate Ids for each Role
            string adminRoleId = Guid.NewGuid().ToString();
            string supplierRoleId = Guid.NewGuid().ToString();
            string customerRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = supplierRoleId, Name = "supplier", NormalizedName = "SUPPLIER" },
                new IdentityRole { Id = customerRoleId, Name = "customer", NormalizedName = "CUSTOMER" }
            );

            //Seeding Users
            //Generate Ids for each User
            string saUserId = Guid.NewGuid().ToString();
            string stojancheUserId = Guid.NewGuid().ToString();
            string dejanBlazheskiUserId = Guid.NewGuid().ToString();
            string dejanJovanovUserId = Guid.NewGuid().ToString();
            //Generate a hasher, we need this hasher to hash our password in the database
            var hasher = new PasswordHasher<User>();

            modelBuilder.Entity<User>().HasData(
                new User { Id = saUserId,
                    FullName = "System Admin",
                    UserName = "sa",
                    NormalizedUserName = "SA",
                    Email = "sa@sa.com",
                    NormalizedEmail = "SA@SA.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "admin123"),
                    SecurityStamp = string.Empty
                },
                new User
                {
                    Id = stojancheUserId,
                    FullName = "Stojanche Mitrevski",
                    UserName = "stojanche.m",
                    NormalizedUserName = "STOJANCHE.M",
                    Email = "stojanche.m@mail.com",
                    NormalizedEmail = "STOJANCHE.M@MAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "supplier123"),
                    SecurityStamp = string.Empty
                },
                new User
                {
                    Id = dejanBlazheskiUserId,
                    FullName = "Dejan Blazheski",
                    UserName = "dejan.blazheski",
                    NormalizedUserName = "DEJAN.BLAZHESKI",
                    Email = "dejan.blazheski@mail.com",
                    NormalizedEmail = "DEJAN.BLAZHESKI@MAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "customer123"),
                    SecurityStamp = string.Empty
                },
                new User
                {
                    Id = dejanJovanovUserId,
                    FullName = "Dejan Jovanov",
                    UserName = "dejan.jovanov",
                    NormalizedUserName = "DEJAN.JOVANOV",
                    Email = "dejan.jovanov@mail.com",
                    NormalizedEmail = "DEJAN.JOVANOV@MAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "customer123"),
                    SecurityStamp = string.Empty
                }
            );

            //Seeding UserRoles, assigning a Role for each User
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = adminRoleId, UserId = saUserId },
                new IdentityUserRole<string> { RoleId = supplierRoleId, UserId = stojancheUserId },
                new IdentityUserRole<string> { RoleId = customerRoleId, UserId = dejanBlazheskiUserId },
                new IdentityUserRole<string> { RoleId = customerRoleId, UserId = dejanJovanovUserId }
            );

            //Seeding Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, DateCreated = DateTime.UtcNow, Status = StatusType.Init, UserId = dejanBlazheskiUserId },
                new Order { Id = 2, DateCreated = DateTime.UtcNow, Status = StatusType.Confirmed, UserId = dejanBlazheskiUserId },
                new Order { Id = 3, DateCreated = DateTime.UtcNow, Status = StatusType.Processing, UserId = dejanJovanovUserId }
            );

            //Seeding Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Epilator", Description = "A small tool for removing unwanted hair in unwanted places", Category = CategoryType.Electronics, Price = 30 },
                new Product { Id = 2, Name = "Headphones", Description = "For IPhone X", Category = CategoryType.Electronics, Price = 5 },
                new Product { Id = 3, Name = "Exploding Kittens", Description = "A board game", Category = CategoryType.Other, Price = 20 },
                new Product { Id = 4, Name = "Martini", Description = "A cool drink delivered to your door", Category = CategoryType.Drinks, Price = 10 },
                new Product { Id = 5, Name = "Hamburger", Description = "Meat, Salads, Fries", Category = CategoryType.Food, Price = 5 },
                new Product { Id = 6, Name = "Enterprise Integration Patterns", Description = "by Gregor Hohpe and Bobby Woolf", Category = CategoryType.Books, Price = 50 }
            );

            //Seeding OrderProduct
            modelBuilder.Entity<OrderProduct>().HasData(
                //Order 1
                new OrderProduct { OrderId = 1, ProductId = 1 },
                new OrderProduct { OrderId = 1, ProductId = 3 },
                new OrderProduct { OrderId = 1, ProductId = 5 },

                //Order 2
                new OrderProduct { OrderId = 2, ProductId = 1 },
                new OrderProduct { OrderId = 2, ProductId = 2 },

                //Order 3
                new OrderProduct { OrderId = 3, ProductId = 4 },
                new OrderProduct { OrderId = 3, ProductId = 5 },
                new OrderProduct { OrderId = 3, ProductId = 6 }
            );
        }
    }
}
