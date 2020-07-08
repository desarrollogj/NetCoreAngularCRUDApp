using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAngularCRUDApp.Models;
using NetCoreAngularCRUDApp.Service;

namespace NetCoreAngularCRUDApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService service;

        public CustomerController(ICustomerService service) {
            this.service = service;
        }

        // GET: api/Customer
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return service.GetAll();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer([FromRoute] int id)
        {
            var customer = service.Get(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
    }
}
