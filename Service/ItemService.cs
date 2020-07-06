using NetCoreAngularCRUDApp.Data;
using NetCoreAngularCRUDApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAngularCRUDApp.Service
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository repo;

        public ItemService(IItemRepository repo) {
            this.repo = repo;
        }

        public Item Get(int id)
        {
            return repo.Get(id);
        }

        public IEnumerable<Item> GetAll()
        {
            return repo.GetAll();
        }
    }
}
