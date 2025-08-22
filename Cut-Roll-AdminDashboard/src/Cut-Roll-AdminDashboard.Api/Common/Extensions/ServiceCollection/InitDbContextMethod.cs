namespace Cut_Roll_AdminDashboard.Api.Common.Extensions.ServiceCollection;

using System;
using Cut_Roll_AdminDashboard.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;

public static class InitDbContextMethod
{
    public static void InitDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlConnection")
            ?? throw new SystemException("connectionString is not set");

        serviceCollection.AddDbContext<CutRollAdminDashboardDbContext>(options =>
            options.UseNpgsql(connectionString));
    }
}
