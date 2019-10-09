using Lamazon.DataAccess;
using Lamazon.DataAccess.Interfaces;
using Lamazon.DataAccess.Repositories;
using Lamazon.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lamazon.Services.Helpers
{
    public class DiModule
    {
        public static IServiceCollection RegisterModules(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LamazonDbContext>(ob => ob.UseSqlServer(
                connectionString
            ));

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequireNonAlphanumeric = true;
            })
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<LamazonDbContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<IUserRepository<User>, UserRespository>();
            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IRepository<Order>, OrderRepository>();
            services.AddTransient<IRepository<OrderProduct>, OrderProductRepository>();
            services.AddTransient<IRepository<Invoice>, InvoiceRepository>();

            return services;
        }
    }
}
