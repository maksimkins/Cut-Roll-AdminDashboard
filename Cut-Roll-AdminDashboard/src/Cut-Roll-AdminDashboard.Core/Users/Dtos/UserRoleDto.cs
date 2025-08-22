using Cut_Roll_AdminDashboard.Core.Roles.Enums;

namespace Cut_Roll_AdminDashboard.Core.Users.Dtos;

public class UserRoleDto
{
    public string? UserId { get; set; }
    public UserRoles Role { get; set; }
}
