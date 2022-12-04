using Infrastructure.Kafka;
using Infrastructure.Kafka.Runners;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace Infrastructure.FM;
public static class ServiceCollectionExtension
{
    public static void ConfigureKafka(this IServiceCollection services)
    {
        services.AddTransient<CreateAndUpdateKafkaRunner>();

        services
            .AddOptions<KafkaConfiguration>()
            .BindConfiguration("Kafka")
            .ValidateDataAnnotations();
    }

}