using NetCoreAngularCRUDApp.Models;
using System.Collections.Generic;

namespace NetCoreAngularCRUDApp.Data
{
    public interface IBlogCategoryRepository
    {
        IEnumerable<BlogCategory> GetAll();
        BlogCategory Get(int id);
    }
}
