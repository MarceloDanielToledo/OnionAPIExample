using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PaymentData : BaseEntity
    {
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        [Required]
        [StringLength(100)]
        public string CardType { get; set; }
        [Required]
        [StringLength(100)]
        public string CardBrand { get; set; }
        [Required]
        [StringLength(4)]
        public string LastFourDigits { get; set; }
    }
}
