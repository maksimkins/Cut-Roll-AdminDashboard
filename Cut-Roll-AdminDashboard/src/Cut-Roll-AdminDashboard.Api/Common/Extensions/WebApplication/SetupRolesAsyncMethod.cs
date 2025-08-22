namespace Cut_Roll_AdminDashboard.Api.Common.Extensions.WebApplication;

using Cut_Roll_AdminDashboard.Core.Roles.Services;
using Microsoft.AspNetCore.Builder;
public static class SetupRolesAsyncMethod
{
    public async static Task SetupRolesAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var roleService = scope.ServiceProvider.GetRequiredService<IRoleService>();
            await roleService.SetupRolesAsync();
        }
    }
}