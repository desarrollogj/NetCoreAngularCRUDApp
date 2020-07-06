using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAngularCRUDApp.Models.ViewModels
{
    public class OrderItemViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public OrderItemViewModel()
        { 
        }

        public OrderItemViewModel(OrderItem item)
        {
            this.ItemId = item.Item.ItemId;
            this.ItemName = item.Item.Name;
            this.Quantity = item.Quantity;
            this.Price = item.Price;
        }
    }
}
