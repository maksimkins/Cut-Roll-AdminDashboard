namespace Cut_Roll_AdminDashboard.Api.Common.Extensions.ServiceCollection;

using Cut_Roll_AdminDashboard.Core.Common.Admin.Services;
using Cut_Roll_AdminDashboard.Core.Common.Services;
using Cut_Roll_AdminDashboard.Core.Roles.Repositories;
using Cut_Roll_AdminDashboard.Core.Roles.Services;
using Cut_Roll_AdminDashboard.Core.Users.Repositories;
using Cut_Roll_AdminDashboard.Core.Users.Services;
using Cut_Roll_AdminDashboard.Infrastructure.Common.Adminl;
using Cut_Roll_AdminDashboard.Infrastructure.Common.Services;
using Cut_Roll_AdminDashboard.Infrastructure.Roles.Repositories;
using Cut_Roll_AdminDashboard.Infrastructure.Roles.Services;
using Cut_Roll_AdminDashboard.Infrastructure.Users.BackgroundServices;
using Cut_Roll_AdminDashboard.Infrastructure.Users.Repositories;
using Cut_Roll_AdminDashboard.Infrastructure.Users.Services;

public static class RegisterDependencyInjectionMethod
{
    public static void RegisterDependencyInjection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUserService, UserService>();
        serviceCollection.AddTransient<IRoleService, RoleService>();

        serviceCollection.AddTransient<IAdminService, AdminService>();

        serviceCollection.AddTransient<IUserRepository, UserEfCoreRepository>();
        serviceCollection.AddTransient<IRoleRepository, RoleEfCoreRepository>();

        serviceCollection.AddTransient<IMessageBrokerService, RabbitMqService>();

        serviceCollection.AddHostedService<UserRabbitMqService>();


    }
}