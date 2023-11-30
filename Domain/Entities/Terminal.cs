using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Terminal : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string ExternalId { get; set; }
    }
}
