using NetCoreAngularCRUDApp.Data;
using NetCoreAngularCRUDApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAngularCRUDTest.IntegrationTests
{
    public static class DBSeeder
    {
        public static void InitializeDbForTests(NetCoreAngularCRUDAppContext db)
        {
            db.BlogCategories.AddRange(GetSeedingBlogCategories());
            db.SaveChanges();
            db.BlogPosts.AddRange(GetSeedingBlogPosts(db));
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(NetCoreAngularCRUDAppContext db)
        {
            db.BlogPosts.RemoveRange(db.BlogPosts);
            db.BlogCategories.RemoveRange(db.BlogCategories);
            InitializeDbForTests(db);
        }

        public static List<BlogCategory> GetSeedingBlogCategories()
        {
            return new List<BlogCategory>()
            {
                new BlogCategory { CategoryId = 1, Name = "Category Alpha" },
                new BlogCategory { CategoryId = 2, Name = "Category Beta" },
                new BlogCategory { CategoryId = 3 ,Name = "Category Gamma" }
            };
        }

        public static List<BlogPost> GetSeedingBlogPosts(NetCoreAngularCRUDAppContext db)
        {
            var categoryAlpha = db.BlogCategories.First(p => p.Name == "Category Alpha");

            return new List<BlogPost>()
            {
                new BlogPost { PostId = 1, Category = categoryAlpha, Creator = "User One", Title = "My first post", Body = "Hello!", Dt = DateTime.Now },
                new BlogPost { PostId = 2, Category = categoryAlpha, Creator = "User Two", Title = "Otra publicación", Body = "Probando el blog", Dt = DateTime.Now }
            };
        }
    }
}
