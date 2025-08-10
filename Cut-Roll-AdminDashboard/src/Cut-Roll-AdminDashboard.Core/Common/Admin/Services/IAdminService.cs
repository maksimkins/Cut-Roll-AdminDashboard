using Cut_Roll_AdminDashboard.Core.Common.Dtos;
using Cut_Roll_AdminDashboard.Core.Roles.Enums;
using Cut_Roll_AdminDashboard.Core.Users.Dtos;

namespace Cut_Roll_AdminDashboard.Core.Common.Admin.Services;
public interface IAdminService
{
    Task AssignRoleToUserAsync(string userId, UserRoles role);
    Task RemoveRoleFromUserAsync(string userId, UserRoles role);
    Task<string> GetRoleByUsernameAsync(string username);
    Task<string> GetRoleByEmailAsync(string email);
    Task<UserResponseDto> GetUserByUsernameAsync(string username);
    Task<UserResponseDto> GetUserByEmailAsync(string username);
    Task<UserResponseDto> GetUserByIdAsync(string userId);
    Task ToggleBanUserAsync(string userId);
    Task ToggleMuteUserAsync(string userId);
    Task<PagedResult<UserResponseDto>> GetUsersFilteredAsync(UserSearchDto dto);
    Task<int> GetFilteredUserCountAsync(UserSearchDto dto);
    Task<IEnumerable<string>> GetRolesByUserIdAsync(string userId);
    Task BulkBanUsersAsync(IEnumerable<string> userIds);
    Task BulkUnbanUsersAsync(IEnumerable<string> userIds);
    Task BulkMuteUsersAsync(IEnumerable<string> userIds);
    Task BulkUnmuteUsersAsync(IEnumerable<string> userIds);
}