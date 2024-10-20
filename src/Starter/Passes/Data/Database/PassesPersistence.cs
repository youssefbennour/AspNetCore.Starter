namespace Starter.Passes.Data.Database;

internal sealed class PassesPersistence(DbContextOptions<PassesPersistence> options) : DbContext(options)
{
    private const string Schema = "Passes";

    public DbSet<Pass> Passes => Set<Pass>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new PassEntityConfiguration());
    }
}
