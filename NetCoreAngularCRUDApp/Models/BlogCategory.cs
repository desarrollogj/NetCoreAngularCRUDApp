using System.ComponentModel.DataAnnotations;

namespace NetCoreAngularCRUDApp.Models
{
    public class BlogCategory
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
