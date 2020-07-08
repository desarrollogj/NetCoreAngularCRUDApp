using System.Collections.Generic;

namespace NetCoreAngularCRUDApp.Models.ViewModels
{
    public class OrderPOSTViewModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string PaymentType { get; set; }
        public List<OrderPOSTItemViewModel> Items { get; set; }
    }
}
