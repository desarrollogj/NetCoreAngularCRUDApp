using NetCoreAngularCRUDApp.Models;
using System.Collections.Generic;

namespace NetCoreAngularCRUDApp.Data
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAll();
        Item Get(int id);
    }
}
