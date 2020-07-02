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

        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<BlogCategory> BlogCategory { get; set; }
    }
}
