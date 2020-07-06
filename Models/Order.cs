using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreAngularCRUDApp.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public string PaymentType { get; set; }
        [Required]
        [Column(TypeName = "decimal(15, 2)")]
        public decimal Total { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
