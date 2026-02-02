using Dapper;
using Platform.Core.Domain.Entities;
using Platform.Infrastructure.Data;

namespace Platform.Infrastructure.Services;

public class UserPermissionService
{
    private readonly OracleDbContext _dbContext;

    public UserPermissionService(OracleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
    {
        using var connection = _dbContext.CreateConnection();
        var query = @"SELECT r.Name 
                     FROM ROLES r
                     INNER JOIN USER_ROLES ur ON r.Id = ur.RoleId
                     WHERE ur.UserId = :UserId";
        
        return await connection.QueryAsync<string>(query, new { UserId = userId });
    }

    public async Task<IEnumerable<Module>> GetUserModulesAsync(int userId)
    {
        using var connection = _dbContext.CreateConnection();
        var query = @"SELECT DISTINCT m.Id, m.Name, m.Version, m.IsEnabled, m.LoadOrder
                     FROM MODULES m
                     INNER JOIN ROLE_MODULES rm ON m.Id = rm.ModuleId
                     INNER JOIN USER_ROLES ur ON rm.RoleId = ur.RoleId
                     WHERE ur.UserId = :UserId AND m.IsEnabled = 1
                     ORDER BY m.LoadOrder";
        
        var modules = await connection.QueryAsync<Module>(query, new { UserId = userId });
        return modules.Select(m => 
        {
            m.IsEnabled = true; // Already filtered by IsEnabled = 1
            return m;
        });
    }
}
