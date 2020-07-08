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
    public class ItemController : ControllerBase
    {
        private readonly IItemService service;
        public ItemController(IItemService service)
        {
            this.service = service;
        }

        // GET: api/Item
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            return service.GetAll();
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem([FromRoute] int id)
        {
            var item = service.Get(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
    }
}
