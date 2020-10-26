using System.Threading;
using System.Threading.Tasks;
using F1Teams.BL.Interfaces;
using F1Teams.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F1Teams.Web.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var teams = await _teamService.ListTeams(new CancellationToken());
            
            return View(teams);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamDto team)
        {
            if (!ModelState.IsValid)
            {
                return View(team);
            }

            await _teamService.CreateTeam(team, new CancellationToken());
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var team = await _teamService.GetTeamById(id, new CancellationToken());
            return View(team);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TeamDto editedTeam)
        {
            if (!ModelState.IsValid)
            {
                return View(editedTeam);
            }

            await _teamService.UpdateTeam(editedTeam, new CancellationToken());
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            await _teamService.DeleteTeam(id, new CancellationToken());
            return RedirectToAction(nameof(Index));
        }
    }
}
