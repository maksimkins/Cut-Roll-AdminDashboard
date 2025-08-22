using Cut_Roll_AdminDashboard.Core.Common.Dtos;
using Cut_Roll_AdminDashboard.Core.Common.Services;
using Cut_Roll_AdminDashboard.Core.Users.Dtos;
using Cut_Roll_AdminDashboard.Core.Users.Models;
using Cut_Roll_AdminDashboard.Core.Users.Repositories;
using Cut_Roll_AdminDashboard.Core.Users.Services;

namespace Cut_Roll_AdminDashboard.Infrastructure.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageBrokerService _messageBrokerService;

    public UserService(IUserRepository userRepository, IMessageBrokerService messageBrokerService)
    {
        _userRepository = userRepository;
        _messageBrokerService = messageBrokerService;
    }

    public async Task<string> AssignRoleToUserAsync(UserRoleIdDto? dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "User role assignment data cannot be null.");

        if (string.IsNullOrWhiteSpace(dto.UserId) || string.IsNullOrWhiteSpace(dto.RoleId))
            throw new ArgumentNullException($"{nameof(dto.UserId)}, {nameof(dto.RoleId)}");

        var changedId = await _userRepository.AssignRoleAsync(dto) ??
            throw new InvalidOperationException("Failed to assign role to user.");
            
        await _messageBrokerService.PushAsync("role_update_identity", new
        {
            Id = dto.UserId,
            RoleId = dto.RoleId,
        });

        
        return changedId;
    }

    public async Task<int> CountUsersByRoleAsync(string? roleId)
    {
        if (string.IsNullOrWhiteSpace(roleId))
            throw new ArgumentNullException(nameof(roleId), "Role ID cannot be null or empty.");

        return await _userRepository.CountUsersByRoleAsync(roleId);
    }

    public async Task<string> CreateUserAsync(UserCreateDto? userCreateDto)
    {
        if (userCreateDto == null)
            throw new ArgumentNullException(nameof(userCreateDto), "User creation data cannot be null.");
        if (string.IsNullOrWhiteSpace(userCreateDto.UserName) || string.IsNullOrWhiteSpace(userCreateDto.Email)
         || string.IsNullOrWhiteSpace(userCreateDto.RoleId) || string.IsNullOrWhiteSpace(userCreateDto.Id))
            throw new ArgumentNullException($"{nameof(userCreateDto.UserName)}, {nameof(userCreateDto.Email)}, {nameof(userCreateDto.RoleId)}, {nameof(userCreateDto.Id)}");

        return await _userRepository.CreateAsync(userCreateDto) ??
            throw new InvalidOperationException("Failed to create user.");
    }

    public async Task<string> DeleteUserByIdAsync(string? userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");
            
        return await _userRepository.DeleteByIdAsync(userId) ??
            throw new InvalidOperationException("Failed to delete user.");
    }

    public async Task<UserResponseDto?> GetUserByEmailAsync(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");

        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(string? userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");

        return await _userRepository.GetByIdAsync(userId);
    }

    public async Task<UserResponseDto?> GetUserByUsernameAsync(string? username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentNullException(nameof(username), "Username cannot be null or empty.");

        return await _userRepository.GetUserByUsernameAsync(username);
    }

    public async Task<IQueryable<User>> GetUsersAsQueryableAsync()
    {
        return await _userRepository.GetUsersAsQueryableAsync();
    }

    public async Task<PagedResult<UserResponseDto>> SearchUsersAsync(UserSearchDto? dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "User search data cannot be null.");

        return await _userRepository.SearchUsersAsync(dto);   
    }

    public async Task<string> ToggleBanUserAsync(string? userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");

        var id = await _userRepository.ToggleBanAsync(userId) ??
            throw new InvalidOperationException("Failed to toggle ban status for user.");

        var user = await _userRepository.GetByIdAsync(userId) ??
            throw new ArgumentException("User not found.");

        await _messageBrokerService.PushAsync("user_toggleban_identity", new
        {
            Id = user.Id,
            IsBanned = user.IsBanned,
        });

        return id;
    }

    public async Task<string> ToggleMuteUserAsync(string? userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");

        var id = await _userRepository.ToggleMuteAsync(userId) ??
            throw new InvalidOperationException("Failed to toggle mute status for user.");
            
        var user = await _userRepository.GetByIdAsync(userId) ??
            throw new ArgumentException("User not found.");

        await _messageBrokerService.PushAsync("user_togglemute_identity", new
        {
            Id = user.Id,
            IsMuted = user.IsMuted,
        });

        return id;
    }

    public async Task<string> UpdateUserAsync(UserUpdateDto? userUpdateDto)
    {
        if (userUpdateDto == null)
            throw new ArgumentNullException(nameof(userUpdateDto), "User update data cannot be null.");
        if (string.IsNullOrWhiteSpace(userUpdateDto.Id))
            throw new ArgumentNullException(nameof(userUpdateDto.Id), "User ID cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(userUpdateDto.UserName) && string.IsNullOrWhiteSpace(userUpdateDto.Email))
            throw new ArgumentNullException($"{nameof(userUpdateDto.UserName)}, {nameof(userUpdateDto.Email)}");

        return await _userRepository.UpdateAsync(userUpdateDto) ??
            throw new InvalidOperationException("Failed to update user.");
        
    }

    public async Task<string> UpdateUserAvatarAsync(UserUpdateAvatarDto? dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "User avatar update data cannot be null.");
        if (string.IsNullOrWhiteSpace(dto.Id) || string.IsNullOrWhiteSpace(dto.AvatarPath))
            throw new ArgumentNullException($"{nameof(dto.Id)}, {nameof(dto.AvatarPath)}");

        var user = await _userRepository.GetByIdAsync(dto.Id) ??
            throw new ArgumentException("User not found.");
            
        return await _userRepository.UpdateAvatarAsync(dto) ??
            throw new InvalidOperationException("Failed to update user avatar.");
    }

    public Task<bool> UserExistsByEmailAsync(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");
        
        return _userRepository.UserExistsByEmailAsync(email);
    }

    public async Task<bool> UserExistsByUsernameAsync(string? username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentNullException(nameof(username), "Username cannot be null or empty.");

        return await _userRepository.UserExistsByUsernameAsync(username);
    }
}
