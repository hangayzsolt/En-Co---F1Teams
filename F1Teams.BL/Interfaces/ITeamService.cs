using F1Teams.Models.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace F1Teams.BL.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDto> GetTeamById(int id, CancellationToken token);
        Task<List<TeamDto>> ListTeams(CancellationToken token);
        Task CreateTeam(TeamDto newTeam, CancellationToken token);
        Task UpdateTeam(TeamDto teamToBeUpdated, CancellationToken token);
        Task DeleteTeam(int id, CancellationToken token);
    }
}
