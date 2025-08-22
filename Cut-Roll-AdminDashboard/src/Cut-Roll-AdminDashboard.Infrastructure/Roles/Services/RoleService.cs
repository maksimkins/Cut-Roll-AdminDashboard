namespace Cut_Roll_AdminDashboard.Infrastructure.Roles.Services;

using System.Threading.Tasks;
using Cut_Roll_AdminDashboard.Core.Common.Dtos;
using Cut_Roll_AdminDashboard.Core.Roles.Dtos;
using Cut_Roll_AdminDashboard.Core.Roles.Enums;
using Cut_Roll_AdminDashboard.Core.Roles.Repositories;
using Cut_Roll_AdminDashboard.Core.Roles.Services;
using Cut_Roll_AdminDashboard.Core.Users.Dtos;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<int> CountUsersInRoleAsync(string? roleId)
    {
        if (string.IsNullOrWhiteSpace(roleId))
            throw new ArgumentNullException("RoleId cannot be null or empty.");
        

        var role = await _roleRepository.GetByIdAsync(roleId) ?? 
            throw new ArgumentException($"Role with ID '{roleId}' not found.");

        return await _roleRepository.CountUsersInRoleAsync(roleId);
    }

    public async Task<string> CreateRoleAsync(RoleCreateDto? dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "Role creation data cannot be null.");
            
        if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Id))
            throw new ArgumentNullException($"{nameof(dto.Id)}, {nameof(dto.Name)}");

        return await _roleRepository.CreateAsync(dto) ??
            throw new InvalidOperationException("Failed to create role.");
    }

    public async Task<string> DeleteRoleByIdAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentNullException(nameof(id), "Role ID cannot be null or empty.");

        var role = await _roleRepository.GetByIdAsync(id) ??
            throw new ArgumentException($"Role with ID '{id}' not found.");

        return await _roleRepository.DeleteByIdAsync(role.Id) ??
            throw new InvalidOperationException($"Role with ID '{id}' not found or could not be deleted.");
    }

    public async Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync()
    {
        return await _roleRepository.GetAllRolesAsync();
    }

    public async Task<RoleResponseDto?> GetRoleByIdAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentNullException(nameof(id), "Role ID cannot be null or empty.");
            
        return await _roleRepository.GetByIdAsync(id);
    }

    public async Task<RoleResponseDto?> GetRoleByNameAsync(UserRoles? role)
    {
        if (role == null)
            throw new ArgumentNullException(nameof(role), "Role cannot be null or empty.");

        return await _roleRepository.GetByNameAsync(role.Value);
    }

    public async Task<PagedResult<UserResponseDto>> GetUsersInRoleAsync(UserGetByRoleIdDto? dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "User retrieval data cannot be null.");
        if (string.IsNullOrWhiteSpace(dto.RoleId))
            throw new ArgumentNullException(nameof(dto.RoleId), "Role ID cannot be null or empty.");

        return await _roleRepository.GetUsersInRoleAsync(dto);
    }

    public async Task<bool> RoleExistsAsync(UserRoles? role)
    {
        if (role == null)
            throw new ArgumentNullException(nameof(role), "Role cannot be null or empty.");

        return await _roleRepository.RoleExistsAsync(role.Value);
    }

    public async Task<int> SetupRolesAsync()
    {
        return await _roleRepository.SetupRolesAsync();
    }
}
