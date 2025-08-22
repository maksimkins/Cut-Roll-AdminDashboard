using Cut_Roll_AdminDashboard.Core.Common.Dtos;
using Cut_Roll_AdminDashboard.Core.Users.Dtos;

namespace Cut_Roll_AdminDashboard.Core.Common.Admin.Services;
public interface IAdminService
{
    Task<string> AssignRoleToUserAsync(UserRoleDto dto);
    Task<string> RemoveRoleFromUserAsync(UserRoleDto dto);
    Task<string> ToggleBanUserAsync(string userId);
    Task<string> ToggleMuteUserAsync(string userId);
    Task<string> GetRoleByUsernameAsync(string username);
    Task<string> GetRoleByEmailAsync(string email);
    Task<UserResponseDto> GetUserByUsernameAsync(string username);
    Task<UserResponseDto> GetUserByEmailAsync(string username);
    Task<UserResponseDto> GetUserByIdAsync(string userId);
    Task<PagedResult<UserResponseDto>> GetUsersFilteredAsync(UserSearchDto dto);
    Task<int> GetFilteredUserCountAsync(UserSearchDto dto);
    Task<string> GetRoleByUserIdAsync(string userId);
}