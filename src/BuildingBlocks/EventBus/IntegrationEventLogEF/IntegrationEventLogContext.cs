using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mCore.BuildingBlocks.IntegrationEventLogEF
{
    public class IntegrationEventLogContext: DbContext
    {
        public IntegrationEventLogContext(DbContextOptions<IntegrationEventLogContext> options) : base(options)
        {
        }

        public DbSet<IntegrationEventLogEntry> IntegrationEventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IntegrationEventLogEntry>(ConfigureIntegrationEventLogEntry);
        }

        private void ConfigureIntegrationEventLogEntry(EntityTypeBuilder<IntegrationEventLogEntry> builder)
        {
            builder.ToTable("IntegrationEventLog");

            builder.HasKey(m => m.EventId);
            builder.Property(m => m.EventId).IsRequired();
            builder.Property(m => m.Content).IsRequired();
            builder.Property(m => m.CreationTime).IsRequired();
            builder.Property(m => m.State).IsRequired();
            builder.Property(m => m.TimesSent).IsRequired();
            builder.Property(m => m.EventTypeName).IsRequired();
        }
    }
}
