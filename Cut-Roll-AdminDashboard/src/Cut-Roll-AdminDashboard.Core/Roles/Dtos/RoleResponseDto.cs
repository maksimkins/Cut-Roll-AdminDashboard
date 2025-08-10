namespace Cut_Roll_AdminDashboard.Core.Roles.Dtos;
public class RoleResponseDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public int UserCount { get; set; }
}