using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace SampleMvcproject.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(80, MinimumLength = 3, ErrorMessage = "Product name is required")]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = string.Empty;

        [Required, Range(0.01, 10000.00, ErrorMessage = "Price must be between $0.01 and $10,000.00")]
        public decimal Price { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }
    }
}
