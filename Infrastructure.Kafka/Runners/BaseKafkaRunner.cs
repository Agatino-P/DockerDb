using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Kafka.Runners;
public class BaseKafkaRunner
{

    private readonly KafkaConfiguration _kafkaConfiguration;
    protected readonly ILogger<BaseKafkaRunner> logger;

    public object AdminClient { get; private set; }

    public BaseKafkaRunner(KafkaConfiguration kafkaConfiguration, ILogger<BaseKafkaRunner> logger)
    {
        _kafkaConfiguration = kafkaConfiguration;
        this.logger = logger;
    }

    //protected async Task CreateTopicsIfNeed()
    //{


    //    using IAdminClient adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = _kafkaConfiguration.BootstrapServers }).Build();
    //    try
    //    {
    //        await adminClient.CreateTopicsAsync(
    //            _kafkaConfiguration.TopicNames.Select(topicName => new TopicSpecification { Name = topicName, ReplicationFactor = 1, NumPartitions = 1 })
    //            );
    //    }
    //    catch (CreateTopicsException e)
    //    {
    //        Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
    //    }

    //}



}
