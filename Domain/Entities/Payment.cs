using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string ExternalId { get; set; }

        public int PaymentStatusId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(100)]
        public string Details { get; set; }

    }
}
