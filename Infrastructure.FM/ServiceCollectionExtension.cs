using FluentMigrator.Runner;
using Infrastructure.FM.Runners;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data.Common;

namespace Infrastructure.FM;
public static class ServiceCollectionExtension
{
    public static void ConfigureDatabaseMigration(this IServiceCollection services)
    {
        services.AddTransient<CreateAndUpdateDatabaseRunner>();
        services.AddTransient<RollbackDatabaseRunner>();

        services
            .AddOptions<DatabaseConfiguration>()
            .BindConfiguration("TestDb")
            .ValidateDataAnnotations();

        services.AddDbConnection();

        services
            .AddFluentMigratorCore()
            .ConfigureRunner(
                builder =>
                {
                    builder
                        .AddPostgres()
                        .WithGlobalCommandTimeout(TimeSpan.FromMinutes(5))
                        .WithGlobalConnectionString(provider => provider.GetRequiredService<IOptions<DatabaseConfiguration>>().Value.ConnectionString
                            )
                            .ScanIn(typeof(AssemblyNamePlaceHolder).Assembly)
                            .For.Migrations();
                });


    }

    private static IServiceCollection AddDbConnection(this IServiceCollection services) =>
        services.AddScoped<DbConnection>(provider => new NpgsqlConnection(provider.GetRequiredService<IOptions<DatabaseConfiguration>>().Value.ConnectionString));
}