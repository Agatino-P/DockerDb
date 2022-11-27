using FluentMigrator.Runner;
using Infrastructure.FM.Runners;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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


        services
            .AddFluentMigratorCore()
            .ConfigureRunner(
                builder =>
                {
                    builder
                        .AddPostgres()
                        .WithGlobalCommandTimeout(TimeSpan.FromMinutes(5))
                        .WithGlobalConnectionString(provider => {
                            var cs = provider.GetRequiredService<IOptions<DatabaseConfiguration>>().Value.ConnectionString;
                            return provider.GetRequiredService<IOptions<DatabaseConfiguration>>().Value.ConnectionString; 
                        })
                        .ScanIn(typeof(AssemblyNamePlaceHolder).Assembly)
                        .For.Migrations();
                });
    }
}