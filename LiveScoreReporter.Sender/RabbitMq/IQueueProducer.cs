using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreReporter.Sender.RabbitMq
{
    public interface IQueueProducer
    {
        Task PublishAsync(string message);
    }
}
