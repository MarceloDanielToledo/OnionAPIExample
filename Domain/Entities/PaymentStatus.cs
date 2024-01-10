using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PaymentStatus
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Status { get; set; }
        [Required]
        [StringLength(100)]
        public string Details { get; set; }
    }
}
