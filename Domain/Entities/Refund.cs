using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Refund : BaseEntity
    {
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        [Required]
        [StringLength(50)]
        public string ExternalId { get; set; }
        [Required]
        [StringLength(100)]
        public string Reason { get; set; }
    }
}
