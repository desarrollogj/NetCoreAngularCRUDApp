using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAngularCRUDApp.Models.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PaymentType { get; set; }
        public List<OrderItemViewModel> Items { get; set; }

        public OrderViewModel()
        { 
        }

        public OrderViewModel(Order order)
        {
            this.OrderId = order.OrderId;
            this.CustomerId = order.Customer.CustomerId;
            this.CustomerName = order.Customer.Name;
            this.PaymentType = order.PaymentType;

            this.Items = order.Items.Select(p => new OrderItemViewModel(p)).ToList();
        }
    }
}
