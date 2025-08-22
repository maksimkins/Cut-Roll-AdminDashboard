namespace Cut_Roll_AdminDashboard.Api.Common.Extensions.WebApplicationBuilder;

using Cut_Roll_AdminDashboard.Core.Common.Options;
using Microsoft.AspNetCore.Builder;


public static class ConfigureMessageBrokerMethod
{
    public static void ConfigureMessageBroker(this WebApplicationBuilder builder)
    {
        var rabbitMqSection = builder.Configuration.GetSection("RabbitMq");
        builder.Services.Configure<RabbitMqOptions>(rabbitMqSection);
    }
}
