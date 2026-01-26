using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace Platform.Infrastructure.Data;

public class OracleDbContext
{
    private readonly string _connectionString;

    public OracleDbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("OracleConnection") 
            ?? "User Id=SYSTEM;Password=oracle;Data Source=localhost:1521/XE";
    }

    public OracleConnection CreateConnection()
    {
        return new OracleConnection(_connectionString);
    }
}
