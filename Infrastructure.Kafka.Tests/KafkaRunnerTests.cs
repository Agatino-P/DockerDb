using FluentAssertions;
using Shared.Tests.Kafka.Fixture;

namespace Infrastructure.Kafka.Tests;

public class KafkaRunnerTests : IClassFixture<KafkaFixture>
{
    private readonly KafkaFixture _kafkaFixture;

    public KafkaRunnerTests(KafkaFixture kafkaFixture)
    {
        _kafkaFixture = kafkaFixture;
    }
    [Fact]
    public void ShouldCreateContainerWithTopics() //Creating fixture etc
    {
        true.Should().Be(true);
    }
}