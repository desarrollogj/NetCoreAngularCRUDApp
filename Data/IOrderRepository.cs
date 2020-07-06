using NetCoreAngularCRUDApp.Models;
using System.Collections.Generic;

namespace NetCoreAngularCRUDApp.Data
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order Get(int id);

        void Add(Order entity);

        void Save();
    }
}
