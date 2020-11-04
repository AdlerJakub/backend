using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class SavingObjectivesController : BaseApiController
    {
        private readonly DataContext _context;
        public SavingObjectivesController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("store")]
        public async Task<ActionResult<SavingObjective[]>> Objectives(SavingObjectiveDto savingObjectiveDto)
        {
            var objective = new SavingObjectives
            {
                SOUserName = savingObjectiveDto.username,
                SOObjectives = JsonSerializer.Serialize(savingObjectiveDto.savingObjectives)
            };
            var objectives = await _context.SavingObjectives.SingleOrDefaultAsync(x => x.SOUserName == savingObjectiveDto.username);
            objectives.SOObjectives = objective.SOObjectives;
            await _context.SaveChangesAsync();

            return savingObjectiveDto.savingObjectives;
        }

        [Authorize]
        [HttpGet("get/{username}")]
        public async Task<ActionResult<string>> GetObjectives(string username)
        {
            var objectives = await _context.SavingObjectives.SingleOrDefaultAsync(x => x.SOUserName == username);

            return objectives == null ? "" : objectives.SOObjectives;
        }
    }
}