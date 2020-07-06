using NetCoreAngularCRUDApp.Data;
using NetCoreAngularCRUDApp.Models;
using System.Collections.Generic;

namespace NetCoreAngularCRUDApp.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repo;

        public OrderService(IOrderRepository repo)
        {
            this.repo = repo;
        }

        public Order Add(Order entity)
        {
            repo.Add(entity);
            repo.Save();

            return entity;
        }

        public Order Get(int id)
        {
            return repo.Get(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return repo.GetAll();
        }

        public void Save()
        {
            repo.Save();
        }
    }
}
