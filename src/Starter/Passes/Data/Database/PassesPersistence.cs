using Starter.Common.DataAccess.Orms.EfCore.DbContexts;

namespace Starter.Passes.Data.Database;

internal sealed class PassesPersistence(DbContextOptions<PassesPersistence> options) : OutboxPersistence(options)
{
    private const string Schema = "Passes";

    public DbSet<Pass> Passes => Set<Pass>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new PassEntityConfiguration());
    }
}
