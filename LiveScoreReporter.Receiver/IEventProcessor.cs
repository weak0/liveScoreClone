using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreReporter.Receiver
{
    public interface IEventProcessor
    {
        Task ProcessEventAsync(string message);
    }
}
