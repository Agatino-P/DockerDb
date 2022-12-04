using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace Shared.Tests.Kafka.Fixture;
public sealed class KafkaContainer : IAsyncLifetime
{
    private readonly KafkaTestcontainer _kafkaTestcontainer;

    public KafkaContainer()
    {
        this._kafkaTestcontainer= BuildKafkaTestContainer();
    }

    public string BootstrapServers=> this._kafkaTestcontainer.BootstrapServers;

    public async Task InitializeAsync()
    {
#if !DEBUG
            System.Threading.Thread.Sleep(5000);
#endif
        await this._kafkaTestcontainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await this._kafkaTestcontainer.DisposeAsync();
    }

    private static KafkaTestcontainer BuildKafkaTestContainer()
    {
        ITestcontainersBuilder<KafkaTestcontainer> builder = new TestcontainersBuilder<KafkaTestcontainer>()
            .WithKafka(new KafkaTestcontainerConfiguration());
            

        string? dockerHost = Environment.GetEnvironmentVariable("DOCKER_HOST");
        if (dockerHost is null)
        {
            return builder.Build();
        }

        builder = builder.WithDockerEndpoint(dockerHost);

        return builder.Build();
    }
}
