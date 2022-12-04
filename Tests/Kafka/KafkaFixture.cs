using Infrastructure.Kafka.Runners;

namespace Shared.Tests.Kafka.Fixture;

public class KafkaFixture : IAsyncLifetime
{
    public KafkaFixtureConfiguration Configuration { get; }
    private KafkaContainer _kafkaContainer { get; }
    private List<string> _topics = new ();

    public KafkaFixture()
    {
        this.Configuration = new KafkaFixtureConfiguration();
        this._kafkaContainer = new KafkaContainer();
    }

    public async Task CreateTopicsAsync(List<string> topics)
    {
        _topics = topics!;
        this.Configuration.InjectTopics(this._topics);
        await this.Configuration.GetRequiredService<CreateAndUpdateKafkaRunner>().CreateTopics();
    }

    public async Task InitializeAsync()
    {
        await this._kafkaContainer.InitializeAsync();
        string bootstrapServer = _kafkaContainer.BootstrapServers;
        this.Configuration.InjectBootstrapServers(bootstrapServer);
        //this.Configuration.InjectTopics(new List<string>() { "tpc1","tpc2"});
        await this.Configuration.GetRequiredService<CreateAndUpdateKafkaRunner>().ExecuteAsync();
    }

    public async Task DisposeAsync()
    {
        await this._kafkaContainer.DisposeAsync();
    }
}
