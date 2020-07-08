using NetCoreAngularCRUDApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAngularCRUDApp.Data
{
    public interface IBlogPostRepository
    {
        IEnumerable<BlogPost> GetAll();
        BlogPost Get(int id);
        Task<BlogPost> GetAsync(int id);
        void Add(BlogPost entity);
        void Update(BlogPost entity);
        void Delete(BlogPost entity);
        void Save();
        Task<BlogPost> SaveAsync(BlogPost entity);
    }
}
