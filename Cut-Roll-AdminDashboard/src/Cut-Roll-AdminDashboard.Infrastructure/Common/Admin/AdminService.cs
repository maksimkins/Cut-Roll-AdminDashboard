using Cut_Roll_AdminDashboard.Core.Common.Admin.Services;
using Cut_Roll_AdminDashboard.Core.Common.Dtos;
using Cut_Roll_AdminDashboard.Core.Common.Services;
using Cut_Roll_AdminDashboard.Core.Roles.Services;
using Cut_Roll_AdminDashboard.Core.Users.Dtos;
using Cut_Roll_AdminDashboard.Core.Users.Services;
using Microsoft.EntityFrameworkCore;

namespace Cut_Roll_AdminDashboard.Infrastructure.Common.Adminl;
public class AdminService : IAdminService
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IMessageBrokerService _messageBrokerService;

    public AdminService(IUserService userService, IRoleService roleService,
        IMessageBrokerService messageBrokerService)
    {
        _userService = userService;
        _roleService = roleService;
        _messageBrokerService = messageBrokerService;
    }

    public async Task<string> AssignRoleToUserAsync(UserRoleDto dto)
    {
        var foundRole = await _roleService.GetRoleByNameAsync(dto.Role)
            ?? throw new ArgumentException($"Role '{dto.Role}' not found.");


        var id = await _userService.AssignRoleToUserAsync(new UserRoleIdDto
        {
            UserId = dto.UserId,
            RoleId = foundRole.Id
        });

        await _messageBrokerService.PushAsync("role_update_identity", new
        {
            Id = dto.UserId,
            RoleId = foundRole.Id,
        });
        
        return id;
    }

    public async Task<int> GetFilteredUserCountAsync(UserSearchDto dto)
    {
        var pagedRes = await _userService.SearchUsersAsync(dto);
        return pagedRes.TotalCount;
    }

    public async Task<string> GetRoleByEmailAsync(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email)
            ?? throw new ArgumentException($"User with email '{email}' not found.");

        return user.Role 
            ?? throw new ArgumentException($"User with email '{email}' has no roles assigned.");
    }

    public async Task<string> GetRoleByUsernameAsync(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username)
            ?? throw new ArgumentException($"User with username' {username}' not found.");
            
        return user.Role
            ?? throw new ArgumentException($"User with username '{username}' has no roles assigned.");
    }
    public async Task<string> GetRoleByUserIdAsync(string userId)
    {
        var user = await _userService.GetUserByIdAsync(userId)
            ?? throw new ArgumentException($"User with id'{userId}' not found.");
            
        return user.Role
            ?? throw new ArgumentException($"User with username '{userId}' has no roles assigned.");
    }

    public async Task<UserResponseDto> GetUserByEmailAsync(string username)
    {
        var user = await _userService.GetUserByEmailAsync(username)
            ?? throw new ArgumentException($"User with email '{username}' not found.");

        return user;
    }

    public async Task<UserResponseDto> GetUserByIdAsync(string userId)
    {
        var user = await _userService.GetUserByIdAsync(userId)
            ?? throw new ArgumentException($"User with id '{userId}' not found.");

        return user;
    }

    public async Task<UserResponseDto> GetUserByUsernameAsync(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username)
            ?? throw new ArgumentException($"User with username'{username}' not found.");

        return user;
    }

    public async Task<PagedResult<UserResponseDto>> GetUsersFilteredAsync(UserSearchDto dto)
    {
        return await _userService.SearchUsersAsync(dto);
    }

    public async Task<string> RemoveRoleFromUserAsync(UserRoleDto dto)
    {
        var foundRole = await _roleService.GetRoleByNameAsync(dto.Role)
            ?? throw new ArgumentException($"Role '{dto.Role}' not found.");

        var id = await _userService.AssignRoleToUserAsync(new UserRoleIdDto
        {
            UserId = dto.UserId,
            RoleId = null 
        });

        await _messageBrokerService.PushAsync("role_update_identity", new
        {
            Id = dto.UserId,
            RoleId = (string?)null
        });

        return id;
    }

    public async Task<string> ToggleBanUserAsync(string userId)
    {
        var result = await _userService.ToggleBanUserAsync(userId);

        var user = await _userService.GetUserByIdAsync(userId)
            ?? throw new ArgumentException($"User with ID '{userId}' not found.");
        
        await _messageBrokerService.PushAsync("user_toggleban_identity", new
        {
            Id = userId,
            IsBanned = user.IsBanned
        });

        return result;
    }

    public async Task<string> ToggleMuteUserAsync(string userId)
    {
        var result = await _userService.ToggleMuteUserAsync(userId);
        var user = await _userService.GetUserByIdAsync(userId)
            ?? throw new ArgumentException($"User with ID '{userId}' not found.");

        await _messageBrokerService.PushAsync("user_togglemute_identity", new
        {
            Id = userId,
            IsMuted = user.IsMuted
        });

        return result;
    }
}