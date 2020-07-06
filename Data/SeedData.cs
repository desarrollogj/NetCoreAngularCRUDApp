using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace NetCoreAngularCRUDApp.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app) {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<NetCoreAngularCRUDAppContext>();
                context.Database.Migrate();

                if (!context.BlogCategories.Any())
                {
                    context.BlogCategories.AddRange(
                        new Models.BlogCategory { Name = "Category Alpha" },
                        new Models.BlogCategory { Name = "Category Beta" },
                        new Models.BlogCategory { Name = "Category Gamma" }
                    );

                    context.SaveChanges();
                }

                if (!context.BlogPosts.Any())
                {
                    var categoryAlpha = context.BlogCategories.First(p => p.Name == "Category Alpha");

                    context.BlogPosts.AddRange(
                        new Models.BlogPost { Category = categoryAlpha, Creator = "User One", Title = "My first post", Body = "Hello!", Dt = DateTime.Now },
                        new Models.BlogPost { Category = categoryAlpha, Creator = "User Two", Title = "Otra publicación", Body = "Probando el blog", Dt = DateTime.Now }
                    );

                    context.SaveChanges();
                }

                if (!context.Customers.Any()) {
                    context.Customers.AddRange(
                        new Models.Customer { Name = "Jorge Lopez" },
                        new Models.Customer { Name = "Juan Perez" },
                        new Models.Customer { Name = "Jimena Garcia" },
                        new Models.Customer { Name = "Juana Morales" }
                    );

                    context.SaveChanges();
                }

                if (!context.Items.Any()) {
                    context.Items.AddRange(
                        new Models.Item { Name = "SSD 256 GB", Price = 4500 },
                        new Models.Item { Name = "GeForce RTX 2060", Price = 60000 },
                        new Models.Item { Name = "Ryzen 7 3700", Price = 29000 },
                        new Models.Item { Name = "DDR4 8 GB", Price = 5000 }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
