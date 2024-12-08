using System.Data.Common;
using Npgsql;
using Respawn;

namespace Starter.IntegrationTests.Common;

[Collection(nameof(SharedDatabase))]
public class IntegrationTest( 
    DatabaseContainer databaseContainer): IAsyncLifetime
{
    
    private DbConnection _connection = default!;
    private Respawner _respawner = default!;

    public async Task InitializeAsync()
    {
        _connection = new NpgsqlConnection(databaseContainer.ConnectionString);
        await _connection.OpenAsync();
        
        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions()
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["Offers", "Passes", "Contracts"]
        });

    }

    public async Task DisposeAsync()
    {
        await _respawner.ResetAsync(_connection);
        await _connection.CloseAsync();
    }
}