using Infrastructure.FM;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.Data.Common;

namespace Shared.Tests.Kafka.Fixture;

public sealed class KafkaFixtureConfiguration
{
    private readonly IHostBuilder hostBuilder;
    private IServiceProvider? services;

    public KafkaFixtureConfiguration()
    {
        this.hostBuilder = CreateHostBuilder();
    }

    public void InjectBootstrapServers(string bootstrapServers)
    {
        this.hostBuilder.ConfigureAppConfiguration(
            (_, builder) =>
            {
                builder.AddInMemoryCollection(
                    new List<KeyValuePair<string, string?>>
                    {
                        new("Kafka:BootstrapServers", bootstrapServers)
                    });
            });
    }

    public void InjectTopics(IEnumerable<string> topics)
    {
        var configValues = 
            topics.Select((topic, counter) => new KeyValuePair<string, string>($"Kafka:Topics:{counter.ToString()}", topic)).ToList();
        this.hostBuilder.ConfigureAppConfiguration(
            (_, builder) =>
            {
                builder.AddInMemoryCollection(configValues!);
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
                    services.ConfigureKafka());
    }

}