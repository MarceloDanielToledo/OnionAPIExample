using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Seeders
{
    public class RefundStatusesSeed : IEntityTypeConfiguration<RefundStatus>
    {
        public void Configure(EntityTypeBuilder<RefundStatus> builder)
        {
            builder.HasData(
                new PaymentStatus { Id = 1, Status = "REQUEST", Details = "Refund pending confirmation." },
                new PaymentStatus { Id = 2, Status = "CANCELLED", Details = "Cancelled Refund." },
                new PaymentStatus { Id = 3, Status = "CONFIRMED", Details = "Refund confirmed." },
                new PaymentStatus { Id = 4, Status = "UNDONE", Details = "Refund undone." }
            );
        }
    }
}
