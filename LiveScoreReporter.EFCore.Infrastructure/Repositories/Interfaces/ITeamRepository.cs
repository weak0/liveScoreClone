﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveScoreReporter.EFCore.Infrastructure.Entities;

namespace LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        Task<Team?> GetTeamAsync(int teamId);
        Task<List<Team>> GetTeamsAsync();
    }
}
