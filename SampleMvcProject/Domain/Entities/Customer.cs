using System.ComponentModel.DataAnnotations;

namespace SampleMvcProject.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = " Name is required.")]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(100, ErrorMessage = " Email is required.")]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(15, ErrorMessage = " Phone is required.")]
        public string Phone { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
