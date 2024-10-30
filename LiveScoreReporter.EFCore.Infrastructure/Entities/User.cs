using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreReporter.EFCore.Infrastructure.Entities
{
    public class User 
    {
        [Key]
        public int Id { get; init; }

        public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
        public string Username { get; init; }
        public string PasswordHash { get; init; }
    }
}
