using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAngularCRUDApp.Models.ViewModels
{
    public class OrderGETViewModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PaymentType { get; set; }
        public DateTime Date { get; set; }

        public OrderGETViewModel()
        {
        }

        public OrderGETViewModel(Order order)
        {
            this.OrderId = order.OrderId;
            this.CustomerId = order.Customer.CustomerId;
            this.CustomerName = order.Customer.Name;
            this.PaymentType = order.PaymentType;
            this.Date = order.Date;
        }
    }
}
