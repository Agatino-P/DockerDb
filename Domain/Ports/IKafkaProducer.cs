using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports;
public interface IKafkaPublisher
{
    public void Publish(string topic, string message);
}
