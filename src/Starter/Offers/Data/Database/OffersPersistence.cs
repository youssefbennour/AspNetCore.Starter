using Starter.Common.DataAccess.Orms.EfCore.DbContexts;

namespace Starter.Offers.Data.Database;

internal sealed class OffersPersistence(DbContextOptions<OffersPersistence> options) : OutboxPersistence(options)
{
    private const string Schema = "Offers";

    public DbSet<Offer> Offers => Set<Offer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new OfferEntityConfiguration());
    }
}
