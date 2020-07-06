using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCoreAngularCRUDApp.Models;

namespace NetCoreAngularCRUDApp.Data
{
    public class NetCoreAngularCRUDAppContext : DbContext
    {
        private readonly ILoggerFactory logger;

        public NetCoreAngularCRUDAppContext (DbContextOptions<NetCoreAngularCRUDAppContext> options)
            : base(options) {
            logger = LoggerFactory.Create(b => { b.AddConsole(); }); // Enabled console logger to trace SQL queries
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(logger).EnableSensitiveDataLogging();
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
