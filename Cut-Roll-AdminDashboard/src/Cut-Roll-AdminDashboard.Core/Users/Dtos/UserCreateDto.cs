namespace Cut_Roll_AdminDashboard.Core.Users.Dtos;

public class UserCreateDto
{
    public string RoleId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
}