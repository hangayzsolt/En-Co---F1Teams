using AutoMapper;
using F1Teams.BL.Interfaces;
using F1Teams.DAL.Interfaces;
using F1Teams.Models.DTOs;
using F1Teams.Models.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using F1Teams.Models;

namespace F1Teams.BL.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly IRepository<TeamsDbContext, Team> _teamRepository;
        private readonly IMapper _mapper;

        public TeamService(IRepository<TeamsDbContext, Team> teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<TeamDto> GetTeamById(int id, CancellationToken token)
        {
            var team = await _teamRepository.GetById(id, token);
            return _mapper.Map<TeamDto>(team);
        }

        public async Task<List<TeamDto>> ListTeams(CancellationToken token)
        {
            var teams = await _teamRepository.GetAll(token);
            return _mapper.Map<List<TeamDto>>(teams);
        }

        public async Task CreateTeam(TeamDto newTeam, CancellationToken token)
        {
            await _teamRepository.Create(_mapper.Map<Team>(newTeam), token);
        }

        public async Task DeleteTeam(int id, CancellationToken token)
        {
            await _teamRepository.Delete(id, token);
        }

        public async Task UpdateTeam(TeamDto teamToBeUpdated, CancellationToken token)
        {
            await _teamRepository.Update(_mapper.Map<Team>(teamToBeUpdated), token);
        }
    }
}
