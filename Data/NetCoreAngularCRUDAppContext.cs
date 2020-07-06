using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreAngularCRUDApp.Models;

namespace NetCoreAngularCRUDApp.Data
{
    public class NetCoreAngularCRUDAppContext : DbContext
    {
        public NetCoreAngularCRUDAppContext (DbContextOptions<NetCoreAngularCRUDAppContext> options)
            : base(options) {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
