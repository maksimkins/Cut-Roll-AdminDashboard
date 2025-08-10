using System.Text.Json.Serialization;
using Cut_Roll_AdminDashboard.Core.Users.Models;

namespace Cut_Roll_AdminDashboard.Core.Roles.Models;
public class Role
{
    public required string Id { get; set; }
    public required string Name { get; set; }

    [JsonIgnore]
    public ICollection<User> Users { get; set; } = [];
}