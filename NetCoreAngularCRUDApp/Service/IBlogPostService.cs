using NetCoreAngularCRUDApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreAngularCRUDApp.Service
{
    public interface IBlogPostService
    {
        IEnumerable<BlogPost> GetAll();
        BlogPost Get(int id);
        Task<BlogPost> GetAsync(int id);
        void Add(BlogPost entity);
        void Update(BlogPost entity);
        void Delete(BlogPost entity);
    }
}
