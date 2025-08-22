namespace Cut_Roll_AdminDashboard.Infrastructure.Users.BackgroundServices;

using System.Text.Json;
using Cut_Roll_AdminDashboard.Core.Common.BackgroundServices;
using Cut_Roll_AdminDashboard.Core.Common.Options;
using Cut_Roll_AdminDashboard.Core.Users.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Cut_Roll_AdminDashboard.Core.Users.Repositories;
using Microsoft.Extensions.Hosting;
using Cut_Roll_AdminDashboard.Core.Users.Services;

public class UserRabbitMqService : BaseRabbitMqService, IHostedService
{
    public UserRabbitMqService(IOptions<RabbitMqOptions> optionsSnapshot, IServiceScopeFactory serviceScopeFactory) :
        base(optionsSnapshot, serviceScopeFactory)
    {
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        base.StartListening("user_create_admin", async message => {
            using (var scope = base.serviceScopeFactory.CreateScope())
            {
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                var newUser = JsonSerializer.Deserialize<UserCreateDto>(message)!;

                await userRepository.CreateAsync(newUser);
            }
        });

        base.StartListening("user_update_admin", async message => {

            using (var scope = base.serviceScopeFactory.CreateScope())
            {
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserService>();

                var updateDto = JsonSerializer.Deserialize<UserUpdateDto>(message)!;

                if (updateDto.Id is null)
                {
                    throw new ArgumentException("User ID cannot be null for update operation.");
                }

                var userToUpdate = await userRepository.GetUserByIdAsync(updateDto.Id) ?? throw new ArgumentException($"there is no user with id: {updateDto.Id}");

                userToUpdate.Email = updateDto.Email is null ? userToUpdate.Email : updateDto.Email;
                userToUpdate.Username = updateDto.UserName is null ? userToUpdate.Username : updateDto.UserName;
                await userRepository.UpdateUserAsync(new UserUpdateDto
                {
                    Id = userToUpdate.Id,
                    RoleId = updateDto.RoleId,
                    UserName = userToUpdate.Username,
                    Email = userToUpdate.Email
                });
            }
        });

        base.StartListening("user_update_avatar_admin", async message => {
            using (var scope = base.serviceScopeFactory.CreateScope())
            {
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserService>();

                var dto = JsonSerializer.Deserialize<UserUpdateAvatarDto>(message)!;

                await userRepository.UpdateUserAvatarAsync(dto);
            }
        });

        base.StartListening("user_delete_admin", async message => {
            using (var scope = base.serviceScopeFactory.CreateScope())
            {
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserService>();

                var dto = JsonSerializer.Deserialize<UserDeleteDto>(message)!;

                await userRepository.DeleteUserByIdAsync(dto.UserId);
            }
        });


        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
