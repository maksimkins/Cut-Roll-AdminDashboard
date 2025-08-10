using Cut_Roll_AdminDashboard.Core.Common.Dtos;
using Cut_Roll_AdminDashboard.Core.Roles.Dtos;
using Cut_Roll_AdminDashboard.Core.Roles.Enums;
using Cut_Roll_AdminDashboard.Core.Users.Dtos;
using Cut_Roll_AdminDashboard.Core.Users.Models;

namespace Cut_Roll_AdminDashboard.Core.Roles.Services;

public interface IRoleService
{
    public Task<RoleResponseDto> GetRoleByIdAsync(string? id);
    public Task<string> CreateRoleAsync(RoleCreateDto? dto);
    public Task<string> DeleteRoleByIdAsync(string? id);
    public Task<string> UpdateRoleAsync(RoleUpdateDto? dto);
    public Task<RoleResponseDto?> GetRoleByNameAsync(UserRoles? role);
    public Task<PagedResult<RoleResponseDto>> GetAllRolesAsync(RolePaginationDto? dto); 
    public Task<int> SetupRolesAsync(); 
    public Task<bool> RoleExistsAsync(UserRoles? role);
    public Task<PagedResult<User>> GetUsersInRoleAsync(UserGetByRoleIdDto? dto); 
    public Task<int> CountUsersInRoleAsync(string? roleId); 
}
