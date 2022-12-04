using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Kafka.Runners;
public class CreateAndUpdateKafkaRunner : BaseKafkaRunner
{
    public CreateAndUpdateKafkaRunner(IOptions<KafkaConfiguration> kafkaConfiguration,
                                      ILogger<BaseKafkaRunner> logger) : base(kafkaConfiguration.Value, logger)
    {
    }

    public async Task CreateTopics()
    {
        //await Task.Delay(1);
    }

    public async Task ExecuteAsync()
    {

    }

    private Task CreateTopicsIfNeeded()
    {
        throw new NotImplementedException();
    }
}
