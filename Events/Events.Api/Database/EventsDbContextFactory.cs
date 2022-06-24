using Kvpbldsck.NastolochkiAPI.Common.Contract.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Database;

public sealed class EventsDbContextFactory : IDesignTimeDbContextFactory<EventsDbContext>
{
    public EventsDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .AddEnvFile("Properties/.env")
            .Build();

        var connectionString = CreateConnectionStringFromEnv(configuration);

        var options = new DbContextOptionsBuilder<EventsDbContext>();
        options.UseNpgsql(connectionString);

        return new (options.Options);
    }

    public static string CreateConnectionStringFromEnv(IConfigurationRoot configuration)
    {
        return string.Format(
            "Host={0};Port={1};Database={2};Username={3};Password={4};",
            configuration.GetSection("POSTGRES_HOST").Get<string>(),
            configuration.GetSection("POSTGRES_PORT").Get<string>(),
            configuration.GetSection("POSTGRES_DATABASE").Get<string>(),
            configuration.GetSection("POSTGRES_USER").Get<string>(),
            configuration.GetSection("POSTGRES_PASSWORD").Get<string>());
    }
}
