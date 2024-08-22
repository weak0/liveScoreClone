using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreReporter.EFCore.Infrastructure.Entities
{
    public class Player
    {
        public int Id { get; set; }
       
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
        public ICollection<Event> AssistedEvents { get; set; }
    }
}
