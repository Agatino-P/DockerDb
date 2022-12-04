using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Kafka;
public class KafkaConfiguration
{
    [Required] public string BootstrapServers { get; set; } = default!;
}
