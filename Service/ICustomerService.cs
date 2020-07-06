using NetCoreAngularCRUDApp.Models;
using System.Collections.Generic;

namespace NetCoreAngularCRUDApp.Service
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
    }
}
