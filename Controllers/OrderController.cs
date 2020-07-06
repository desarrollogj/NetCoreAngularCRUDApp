using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAngularCRUDApp.Models;
using NetCoreAngularCRUDApp.Models.ViewModels;
using NetCoreAngularCRUDApp.Service;

namespace NetCoreAngularCRUDApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly ICustomerService customerService;
        private readonly IItemService itemService;

        public OrderController(IOrderService orderService, ICustomerService customerService, IItemService itemService)
        {
            this.orderService = orderService;
            this.customerService = customerService;
            this.itemService = itemService;
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public ActionResult<OrderViewModel> GetOrder([FromRoute] int id)
        {
            var order = orderService.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            return new OrderViewModel(order);
        }

        // POST: api/Order
        [HttpPost]
        public ActionResult<OrderViewModel> PostOrder(OrderPOSTViewModel orderVm)
        {
            if (orderVm == null)
            {
                return BadRequest();
            }

            var customer = customerService.Get(orderVm.CustomerId);
            if (customer == null) 
            {
                return BadRequest("Customer not found");
            }

            Order order = new Order
            {
                Customer = customer,
                PaymentType = orderVm.PaymentType,
                Items = new List<OrderItem>()
            };

            if (orderVm.Items.Count == 0)
            {
                return BadRequest("Order is empty");
            }

            foreach (OrderPOSTItemViewModel itemVm in orderVm.Items)
            {
                var item = itemService.Get(itemVm.ItemId);
                if (item == null) 
                {
                    return BadRequest("Item not found");
                }

                if (itemVm.Quantity <= 0) 
                {
                    return BadRequest("Quantity should be 1 or more");
                }

                if (itemVm.Quantity < 0)
                {
                    return BadRequest("Quantity should be greater or equal than zero");
                }

                var orderItem = new OrderItem()
                {
                    Order = order,
                    Item = item,
                    Quantity = itemVm.Quantity,
                    Price = itemVm.Price
                };

                order.Items.Add(orderItem);
            }

            order.Total = order.Items.Sum(p => p.Quantity * p.Price);

            orderService.Add(order);

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, new OrderViewModel(order));
        }
    }
}
