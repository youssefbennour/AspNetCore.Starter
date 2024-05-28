using System.Data;

namespace Starter.Reports.DataAccess;

internal interface IDatabaseConnectionFactory : IDisposable
{
    IDbConnection Create();
}
