using NetCoreAngularCRUDApp.Models;
using System.Collections.Generic;

namespace NetCoreAngularCRUDApp.Service
{
    public interface IItemService
    {
        IEnumerable<Item> GetAll();
        Item Get(int id);
    }
}
