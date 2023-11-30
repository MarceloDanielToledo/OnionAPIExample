using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class RefundStatus : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Status { get; set; }
        [Required]
        [StringLength(100)]
        public string Details { get; set; }
    }
}
