using Platform.Core.Domain.Entities;
using Platform.Core.Domain.Interfaces;

namespace Platform.Core.Services;

public class UserService
{
    private readonly IRepository<User> _userRepository;
    private readonly AuthService _authService;

    public UserService(IRepository<User> userRepository, AuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
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
}
