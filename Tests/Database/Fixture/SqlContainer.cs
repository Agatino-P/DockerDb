using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Database.Fixture;
public sealed class SqlContainer : IAsyncLifetime
{
    private readonly PostgreSqlTestcontainer testContainerDatabase;

    public SqlContainer()
    {
        this.testContainerDatabase = BuildTestContainerDatabase();
    }

    public string ConnectionString => this.testContainerDatabase.ConnectionString;

    public async Task InitializeAsync()
    {
#if !DEBUG
            System.Threading.Thread.Sleep(5000);
#endif
        await this.testContainerDatabase.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await this.testContainerDatabase.DisposeAsync();
    }

    private static PostgreSqlTestcontainer BuildTestContainerDatabase()
    {
        ITestcontainersBuilder<PostgreSqlTestcontainer> builder = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(
                new PostgreSqlTestcontainerConfiguration("postgres:13.3")
                {
                    Database = "postgres",
                    Username = "postgres",
                    Password = "postgres"
                })
            .WithCleanUp(true);

        string? dockerHost = Environment.GetEnvironmentVariable("DOCKER_HOST");
        if (dockerHost is null)
        {
            return builder.Build();
        }

        builder = builder.WithDockerEndpoint(dockerHost);

        return builder.Build();
    }
}
