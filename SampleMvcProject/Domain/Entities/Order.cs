using SampleMvcproject.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleMvcProject.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public Decimal TotalAmount { get; set; }

        [Required]
        public ICollection<Product> OrderItems { get; set; } = new List<Product>();
    }
}
