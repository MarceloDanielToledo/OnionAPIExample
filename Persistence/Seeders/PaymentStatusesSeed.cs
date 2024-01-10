using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Seeders
{
    public class PaymentStatusesSeed : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder)
        {
            builder.HasData(
                new PaymentStatus { Id = 1, Status = "REQUEST", Details = "Payment pending confirmation." },
                new PaymentStatus { Id = 2, Status = "CANCELLED", Details = "Cancelled payment." },
                new PaymentStatus { Id = 3, Status = "CONFIRMED", Details = "Payment confirmed." },
                new PaymentStatus { Id = 4, Status = "UNDONE", Details = "Payment undone." }
            );
        }
    }
}
