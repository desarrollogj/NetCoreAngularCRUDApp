using Microsoft.EntityFrameworkCore;
using NetCoreAngularCRUDApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAngularCRUDApp.Data
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private NetCoreAngularCRUDAppContext context;

        public BlogPostRepository(NetCoreAngularCRUDAppContext context)
        {
            this.context = context;
        }
        public IEnumerable<BlogPost> GetAll()
        {
            return context.BlogPost
                .Include("Category")
                .OrderByDescending(p => p.PostId).AsEnumerable();  
        }

        public BlogPost Get(int id)
        {
            return context.BlogPost
                .Include("Category")
                .FirstOrDefault(p => p.PostId == id);
        }

        public async Task<BlogPost> GetAsync(int id)
        {
            return await context.BlogPost
                .Include("Category")
                .FirstOrDefaultAsync(p => p.PostId == id);
        }

        public void Add(BlogPost entity)
        {
            context.Set<BlogPost>().Add(entity);
        }

        public void Update(BlogPost entity)
        {
            //context.Set<BlogPost>().Attach(entity);
            //context.Entry(entity).State = EntityState.Modified;
            context.Set<BlogPost>().Update(entity);
        }

        public void Delete(BlogPost entity)
        {
            context.Set<BlogPost>().Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task<BlogPost> SaveAsync(BlogPost entity)
        {
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
