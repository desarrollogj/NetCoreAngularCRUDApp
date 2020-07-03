using NetCoreAngularCRUDApp.Data;
using NetCoreAngularCRUDApp.Models;
using System.Collections.Generic;

namespace NetCoreAngularCRUDApp.Service
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IBlogCategoryRepository repo;

        public BlogCategoryService(IBlogCategoryRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<BlogCategory> GetAll()
        {
            return repo.GetAll();
        }

        public BlogCategory Get(int id)
        {
            return repo.Get(id);
        }
    }
}
