using Starter.Common.DataAccess.Orms.EfCore.DbContexts;

namespace Starter.Contracts.Data.Database;

internal sealed class ContractsPersistence(DbContextOptions<ContractsPersistence> options) : OutboxPersistence(options)
{
    private const string Schema = "Contracts";

    public DbSet<Contract> Contracts => Set<Contract>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new ContractEntityConfiguration());
    }
}
