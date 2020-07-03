using NetCoreAngularCRUDApp.Models;
using System.Collections.Generic;

namespace NetCoreAngularCRUDApp.Service
{
    public interface IBlogCategoryService
    {
        IEnumerable<BlogCategory> GetAll();
        BlogCategory Get(int id);
    }
}
