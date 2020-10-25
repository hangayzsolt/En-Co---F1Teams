using F1Teams.Models.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace F1Teams.DAL.Interfaces
{
    public interface ITeamRepository
    {
        Task<Team> GetTeamById(int id, CancellationToken token);
        Task<List<Team>> ListTeams(CancellationToken token);
        Task CreateTeam(Team newTeam, CancellationToken token);
        Task UpdateTeam(Team teamToBeUpdated, CancellationToken token);
        Task<bool> DeleteTeam(int id, CancellationToken token);
    }
}
