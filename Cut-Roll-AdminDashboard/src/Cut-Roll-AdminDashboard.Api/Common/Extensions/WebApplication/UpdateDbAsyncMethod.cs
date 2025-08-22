namespace Cut_Roll_AdminDashboard.Api.Common.Extensions.WebApplication;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Cut_Roll_AdminDashboard.Infrastructure.Common.Data;

public static class UpdateDbAsyncMethod
{
    public async static Task UpdateDbAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<CutRollAdminDashboardDbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}