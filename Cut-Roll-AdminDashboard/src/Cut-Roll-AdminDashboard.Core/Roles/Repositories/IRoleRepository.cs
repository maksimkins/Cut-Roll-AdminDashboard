using Cut_Roll_AdminDashboard.Core.Common.Dtos;
using Cut_Roll_AdminDashboard.Core.Common.Repositories.Interfaces;
using Cut_Roll_AdminDashboard.Core.Roles.Dtos;
using Cut_Roll_AdminDashboard.Core.Roles.Enums;
using Cut_Roll_AdminDashboard.Core.Users.Dtos;
using Cut_Roll_AdminDashboard.Core.Users.Models;

namespace Cut_Roll_AdminDashboard.Core.Roles.Repositories;
public interface IRoleRepository :
    IGetByIdAsync<string, RoleResponseDto?>,
    ICreateAsync<RoleCreateDto, string?>,
    IDeleteByIdAsync<string, string?>
{
    Task<RoleResponseDto?> GetByNameAsync(UserRoles role);
    Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync(); 
    Task<int> SetupRolesAsync(); 
    Task<bool> RoleExistsAsync(UserRoles role);
    Task<PagedResult<UserResponseDto>> GetUsersInRoleAsync(UserGetByRoleIdDto dto); 
    Task<int> CountUsersInRoleAsync(string roleId); 
}