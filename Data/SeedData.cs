using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NetCoreAngularCRUDApp.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app) {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<NetCoreAngularCRUDAppContext>();
                context.Database.Migrate();

                if (!context.BlogCategory.Any())
                {
                    context.BlogCategory.AddRange(
                        new Models.BlogCategory { Name = "Category Alpha" },
                        new Models.BlogCategory { Name = "Category Beta" },
                        new Models.BlogCategory { Name = "Category Gamma" }
                    );

                    context.SaveChanges();
                }

                if (!context.BlogPost.Any())
                {
                    context.BlogPost.AddRange(
                        new Models.BlogPost { Creator = "User One", Title = "My first post", Body = "Hello!", Dt = DateTime.Now },
                        new Models.BlogPost { Creator = "User Two", Title = "Otra publicación", Body = "Probando el blog", Dt = DateTime.Now }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
