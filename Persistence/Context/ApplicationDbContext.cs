using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Seeders;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime) : base(options)
        {
            _dateTime = dateTime;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaymentStatusesSeed());
            modelBuilder.ApplyConfiguration(new RefundStatusesSeed());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedOn = _dateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentData> PaymentsData { get; set; }
        public DbSet<PaymentError> PaymentsErros { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<RefundError> RefundsErrors { get; set; }
        public DbSet<RefundStatus> RefundStatuses { get; set; }
        public DbSet<Terminal> Terminals { get; set; }




    }
}
