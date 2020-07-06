using Microsoft.EntityFrameworkCore;
using NetCoreAngularCRUDApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAngularCRUDApp.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NetCoreAngularCRUDAppContext context;

        public OrderRepository(NetCoreAngularCRUDAppContext context) 
        {
            this.context = context;
        }

        public void Add(Order entity)
        {
            context.Orders.Add(entity);
        }

        public Order Get(int id)
        {
            return context.Orders
                .Include(p => p.Customer)
                .Include(p => p.Items)
                .Include("Items.Item")
                .FirstOrDefault(p => p.OrderId == id);
        }

        public IEnumerable<Order> GetAll()
        {
            return context.Orders.AsEnumerable();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
