using Dapper;
using Platform.Core.Domain.Interfaces;
using Platform.Infrastructure.Data;

namespace Platform.Infrastructure.Data.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly OracleDbContext _context;
    protected readonly string _tableName;

    public GenericRepository(OracleDbContext context, string tableName)
    {
        _context = context;
        _tableName = tableName;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var query = $"SELECT * FROM {_tableName}";
        return await connection.QueryAsync<T>(query);
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = $"SELECT * FROM {_tableName} WHERE Id = :Id";
        return await connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        using var connection = _context.CreateConnection();
        // This is a simplified implementation
        // In production, you would use proper INSERT statements with RETURNING clause
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        using var connection = _context.CreateConnection();
        // This is a simplified implementation
        // In production, you would use proper UPDATE statements
        await Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = $"DELETE FROM {_tableName} WHERE Id = :Id";
        await connection.ExecuteAsync(query, new { Id = id });
    }
}
