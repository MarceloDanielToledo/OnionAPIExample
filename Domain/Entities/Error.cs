using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Error : BaseEntity
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        [StringLength(100)]
        public string? Title { get; set; }
        [StringLength(300)]
        public string? Message { get; set; }
    }
}
