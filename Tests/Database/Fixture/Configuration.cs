using Infrastructure.FM;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.Data.Common;

namespace Tests.Database.Fixture;

public sealed class Configuration
{
    private readonly IHostBuilder hostBuilder;
    private IServiceProvider? services;

    public Configuration()
    {
        this.hostBuilder = CreateHostBuilder();
    }

    public void InjectConnectionString(string connectionString)
    {
        this.hostBuilder.ConfigureAppConfiguration(
            (_, builder) =>
            {
                builder.AddInMemoryCollection(
                    new List<KeyValuePair<string, string?>>
                    {
                        new("TestDb:ConnectionString", connectionString),
                        new("CollectionContext:Read:Database:ConnectionString", connectionString)
                    });
            });
    }

    public T GetRequiredService<T>() where T : notnull
    {
        this.services ??= this.hostBuilder.Build().Services;
        return this.services!.GetRequiredService<T>();
    }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(
                (_, builder) => builder.Sources.Clear()
            )
            .ConfigureServices(
                (_, services) =>
                    services
                        //.AddDataSources()
                        //.AddCollectionContextWriteDbDependencies()
                        //.AddCollectionContextReadDbDependencies("CollectionContext:Read")
                        //.AddPayablesContextWriteDbDependencies()
                        .ConfigureDatabaseMigration());
    }

    public NpgsqlConnection GetNpgsqlConnection() => (this.GetRequiredService<DbConnection>() as NpgsqlConnection)!;
}