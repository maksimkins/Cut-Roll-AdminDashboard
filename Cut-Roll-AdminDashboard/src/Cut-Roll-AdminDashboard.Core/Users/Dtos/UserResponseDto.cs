namespace Cut_Roll_AdminDashboard.Core.Users.Dtos;
public class UserResponseDto
{
    public required string Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public IEnumerable<string> Roles { get; set; } = new List<string>();
    public bool IsBanned { get; set; }
    public bool IsMuted { get; set; }
    public DateTime CreatedAt { get; set; }
}