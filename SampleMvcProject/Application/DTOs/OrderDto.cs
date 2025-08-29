using SampleMvcproject.Application.DTOs;
using SampleMvcproject.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SampleMvcProject.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }
        
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public CustomerDto CustomerDto { get; set; }

        [Required]
        public  Decimal TotalAmount { get; set; }

        [Required]
        public ICollection<ProductDto> OrderItems { get; set; } = new List<ProductDto>();
    }
}
