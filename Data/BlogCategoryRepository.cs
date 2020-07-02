using NetCoreAngularCRUDApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAngularCRUDApp.Data
{
    public class BlogCategoryRepository : IBlogCategoryRepository
    {
        private NetCoreAngularCRUDAppContext context;

        public BlogCategoryRepository(NetCoreAngularCRUDAppContext context)
        {
            this.context = context;
        }

        public IEnumerable<BlogCategory> GetAll()
        {
            return this.context.BlogCategory.AsEnumerable();
        }
    }
}
