using Dapper;
using Platform.Core.Domain.Entities;
using Platform.Infrastructure.Data;

namespace Platform.Infrastructure.Data.Repositories;

public class UserRepository : GenericRepository<User>
{
    public UserRepository(OracleDbContext context) : base(context, "USERS")
    {
    }

    public override async Task<User> AddAsync(User entity)
    {
        using var connection = _context.CreateConnection();
        var query = @"INSERT INTO USERS (Username, Email, PasswordHash, IsActive, CreatedAt) 
                      VALUES (:Username, :Email, :PasswordHash, :IsActive, :CreatedAt)
                      RETURNING Id INTO :Id";
        
        var parameters = new DynamicParameters();
        parameters.Add("Username", entity.Username);
        parameters.Add("Email", entity.Email);
        parameters.Add("PasswordHash", entity.PasswordHash);
        parameters.Add("IsActive", entity.IsActive ? 1 : 0);
        parameters.Add("CreatedAt", entity.CreatedAt);
        parameters.Add("Id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        await connection.ExecuteAsync(query, parameters);
        entity.Id = parameters.Get<int>("Id");
        
        return entity;
    }

    public override async Task UpdateAsync(User entity)
    {
        using var connection = _context.CreateConnection();
        var query = @"UPDATE USERS 
                      SET Username = :Username, Email = :Email, IsActive = :IsActive 
                      WHERE Id = :Id";
        
        await connection.ExecuteAsync(query, new
        {
            entity.Username,
            entity.Email,
            IsActive = entity.IsActive ? 1 : 0,
            entity.Id
        });
    }
}
