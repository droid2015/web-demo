using Platform.Core.Domain.Entities;
using Platform.Core.Domain.Interfaces;
using Platform.Infrastructure.Data;
using Dapper;

namespace Platform.Core.Services;

public class UserService
{
    private readonly IRepository<User> _userRepository;
    private readonly AuthService _authService;
    private readonly OracleDbContext _dbContext;

    public UserService(IRepository<User> userRepository, AuthService authService, OracleDbContext dbContext)
    {
        _userRepository = userRepository;
        _authService = authService;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> CreateUserAsync(User user, string password)
    {
        user.PasswordHash = _authService.HashPassword(password);
        return await _userRepository.AddAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
    }

    public async Task<User?> ValidateUserAsync(string username, string password)
    {
        var users = await _userRepository.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Username == username && u.IsActive);

        if (user != null && _authService.VerifyPassword(password, user.PasswordHash))
        {
            return user;
        }

        return null;
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
