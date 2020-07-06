using NetCoreAngularCRUDApp.Models;
using System.Collections.Generic;

namespace NetCoreAngularCRUDApp.Service
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        Order Get(int id);

        Order Add(Order entity);

        void Save();
    }
}
