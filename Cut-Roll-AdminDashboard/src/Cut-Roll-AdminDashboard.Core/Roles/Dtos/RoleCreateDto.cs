namespace Cut_Roll_AdminDashboard.Core.Roles.Dtos;

public class RoleCreateDto
{
    public required string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }
}
