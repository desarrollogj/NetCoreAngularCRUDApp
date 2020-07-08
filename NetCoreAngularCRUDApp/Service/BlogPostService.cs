using NetCoreAngularCRUDApp.Data;
using NetCoreAngularCRUDApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreAngularCRUDApp.Service
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository repo;

        public BlogPostService(IBlogPostRepository repo) {
            this.repo = repo;
        }

        public BlogPost Get(int id)
        {
            return repo.Get(id);
        }

        public async Task<BlogPost> GetAsync(int id)
        {
            return await repo.GetAsync(id);
        }

        public IEnumerable<BlogPost> GetAll()
        {
            return repo.GetAll();
        }

        public void Add(BlogPost blogPost)
        {
            repo.Add(blogPost);
            repo.Save();
        }

        public void Delete(BlogPost blogPost)
        {
            repo.Delete(blogPost);
            repo.Save();
        }

        public void Update(BlogPost blogPost)
        {
            repo.Update(blogPost);
            repo.Save();
        }
    }
}
