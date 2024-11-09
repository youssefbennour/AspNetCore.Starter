using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Starter.Common.EventualConsistency.Outbox;

namespace Starter.Common.DataAccess.Orms.EfCore.DbContexts;

public class OutboxPersistence : DbContext{

    public OutboxPersistence(DbContextOptions<OutboxPersistence> options)
        : base(options) {
    }
    protected OutboxPersistence(DbContextOptions options)
        : base(options) {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new OutBoxMessageConfiguration());
    }
}

internal class OutBoxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Message)
            .HasColumnType("json")
            .IsRequired();
        builder.Property(m => m.ExecutedOn)
            .IsRequired(false);
        builder.Property(m => m.Type)
            .IsRequired();
        builder.HasIndex(m => m.ExecutedOn);
    }
}