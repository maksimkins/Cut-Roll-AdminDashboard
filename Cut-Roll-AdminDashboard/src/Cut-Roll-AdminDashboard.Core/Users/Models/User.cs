using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cut_Roll_AdminDashboard.Core.Common.Models.Base;

namespace Cut_Roll_AdminDashboard.Core.Users.Models;

public class User : IBanable, IMuteable
{
    [Key]
    public required string Id { get; set; }
    public required string RoleId { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }

    [DefaultValue(false)]
    public bool IsBanned { get; set; }

    [DefaultValue(false)]
    public bool IsMuted { get; set; }
}