using NetCoreAngularCRUDApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAngularCRUDApp.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly NetCoreAngularCRUDAppContext context;

        public CustomerRepository(NetCoreAngularCRUDAppContext context) 
        {
            this.context = context;
        }

        public Customer Get(int id)
        {
            return context.Set<Customer>().Find(id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return context.Set<Customer>().AsEnumerable();
        }
    }
}
