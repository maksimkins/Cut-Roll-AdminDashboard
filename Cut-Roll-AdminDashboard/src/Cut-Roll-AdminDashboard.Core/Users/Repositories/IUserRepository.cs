using Cut_Roll_AdminDashboard.Core.Common.Dtos;
using Cut_Roll_AdminDashboard.Core.Common.Repositories.Interfaces;
using Cut_Roll_AdminDashboard.Core.Users.Dtos;
using Cut_Roll_AdminDashboard.Core.Users.Models;

namespace Cut_Roll_AdminDashboard.Core.Users.Repositories;

public interface IUserRepository :
    ICreateAsync<UserCreateDto, string?>,
    IGetByIdAsync<string, UserResponseDto?>,
    IUpdateAsync<UserUpdateDto, string?>,
    IDeleteByIdAsync<string, string?>
{
    Task<UserResponseDto?> GetUserByUsernameAsync(string username);
    Task<UserResponseDto?> GetUserByEmailAsync(string email);
    Task<int> CountUsersByRoleAsync(string roleId);
    Task<PagedResult<UserResponseDto>> SearchUsersAsync(UserSearchDto dto);
    Task<bool> UserExistsByUsernameAsync(string username);
    Task<bool> UserExistsByEmailAsync(string email);
    Task<string?> ToggleBanAsync(string userId);
    Task<string?> ToggleMuteAsync(string userId);
    Task<string?> UpdateAvatarAsync(UserUpdateAvatarDto dto);
    Task<string?> AssignRoleAsync(UserRoleIdDto dto);
    Task<IQueryable<User>> GetUsersAsQueryableAsync();
}
