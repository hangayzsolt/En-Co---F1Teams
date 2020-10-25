using AutoMapper;
using F1Teams.BL.Interfaces;
using F1Teams.DAL.Interfaces;
using F1Teams.Models.DTOs;
using F1Teams.Models.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace F1Teams.BL.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<TeamDto> GetTeamById(int id, CancellationToken token)
        {
            var team = await _teamRepository.GetTeamById(id, token);
            return _mapper.Map<TeamDto>(team);
        }

        public async Task<List<TeamDto>> ListTeams(CancellationToken token)
        {
            var teams = await _teamRepository.ListTeams(token);
            return _mapper.Map<List<TeamDto>>(teams);
        }

        public async Task CreateTeam(TeamDto newTeam, CancellationToken token)
        {
            await _teamRepository.CreateTeam(_mapper.Map<Team>(newTeam), token);
        }

        public async Task<bool> DeleteTeam(int id, CancellationToken token)
        {
            return await _teamRepository.DeleteTeam(id, token);
        }

        public async Task UpdateTeam(TeamDto teamToBeUpdated, CancellationToken token)
        {
            await _teamRepository.UpdateTeam(_mapper.Map<Team>(teamToBeUpdated), token);
        }
    }
}
