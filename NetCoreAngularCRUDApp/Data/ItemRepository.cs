using NetCoreAngularCRUDApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAngularCRUDApp.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly NetCoreAngularCRUDAppContext context;
        public ItemRepository(NetCoreAngularCRUDAppContext context)
        {
            this.context = context;
        }


        public Item Get(int id)
        {
            return context.Set<Item>().Find(id);
        }

        public IEnumerable<Item> GetAll()
        {
            return context.Set<Item>().AsEnumerable();
        }
    }
}
