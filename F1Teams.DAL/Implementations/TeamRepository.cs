using F1Teams.DAL.Interfaces;
using F1Teams.Models;
using F1Teams.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace F1Teams.DAL.Implementations
{
    //This class is not wired up
    public class TeamRepository : ITeamRepository
    {
        private readonly TeamsDbContext _teamsContext;

        public TeamRepository(TeamsDbContext ctx)
        {
            _teamsContext = ctx;
        }

        public async Task<Team> GetTeamById(int id, CancellationToken token)
        {
            return await _teamsContext.Teams.FirstOrDefaultAsync(team => team.Id == id, token);
        }

        public async Task<List<Team>> ListTeams(CancellationToken token)
        {
            return await _teamsContext.Teams.ToListAsync(token);
        }

        public async Task CreateTeam(Team newTeam, CancellationToken token)
        {
            await _teamsContext.Teams.AddAsync(newTeam, token);
            await _teamsContext.SaveChangesAsync(token);
        }

        public async Task UpdateTeam(Team teamToBeUpdated, CancellationToken token)
        {
            var originalTeam = await _teamsContext.Teams.FirstOrDefaultAsync(team => team.Id == teamToBeUpdated.Id, token);
            if (originalTeam != null)
            {
                originalTeam.Name = teamToBeUpdated.Name;
                originalTeam.FoundationYear = teamToBeUpdated.FoundationYear;
                originalTeam.WonChampionsTitle = teamToBeUpdated.WonChampionsTitle;
                originalTeam.IsEntryFeePayed = teamToBeUpdated.IsEntryFeePayed;

                await _teamsContext.SaveChangesAsync(token); 
            }
        }

        public async Task<bool> DeleteTeam(int id, CancellationToken token)
        {
            var team = await _teamsContext.Teams.FirstOrDefaultAsync(t => t.Id == id, token);
            if (team == null) return false;
            _teamsContext.Teams.Remove(team);

            await _teamsContext.SaveChangesAsync(token);

            return true;

        }
    }
}
