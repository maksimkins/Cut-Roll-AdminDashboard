namespace Cut_Roll_AdminDashboard.Core.Users.Services;

using Cut_Roll_AdminDashboard.Core.Common.Dtos;
using Cut_Roll_AdminDashboard.Core.Users.Dtos;

public interface IUserService
{
    Task<string> CreateUserAsync(UserCreateDto? userCreateDto);
    Task<string> UpdateUserAsync(UserUpdateDto? userUpdateDto);
    Task<UserResponseDto?> GetUserByIdAsync(string? userId);
    Task<string> DeleteUserByIdAsync(string? userId);
    Task<UserResponseDto?> GetUserByUsernameAsync(string? username);
    Task<UserResponseDto?> GetUserByEmailAsync(string? email);
    Task<int> CountUsersByRoleAsync(string? roleId);
    Task<PagedResult<UserResponseDto>> SearchUsersAsync(UserSearchDto? dto);
    Task<bool> UserExistsByUsernameAsync(string? username);
    Task<bool> UserExistsByEmailAsync(string? email);
    Task<string?> ToggleBanUserAsync(string? userId);
    Task<string?> ToggleMuteUserAsync(string? userId);
}
