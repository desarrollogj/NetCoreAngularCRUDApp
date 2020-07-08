using NetCoreAngularCRUDApp.Data;
using NetCoreAngularCRUDApp.Models;
using System.Collections.Generic;

namespace NetCoreAngularCRUDApp.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository repo;

        public CustomerService(ICustomerRepository repo)
        {
            this.repo = repo;
        }

        public Customer Get(int id)
        {
            return repo.Get(id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return repo.GetAll();
        }
    }
}
