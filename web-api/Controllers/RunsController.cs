using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using web_api.Data;
using web_api.DTOS;
using web_api.Models;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RunsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet] //get all runs
        public async Task<ActionResult<IEnumerable<RunDto>>> GetRuns()
        {
            var runs = await _context.Runs 
                .Include(r => r.RunWeapons)
                .Include(r => r.RunTools)
                .Select(r => new RunDto{
                    Id = r.Id,
                    MapId = r.MapId,
                    CharacterId = r.CharacterId,
                    GoldEarned = r.GoldEarned,
                    EntryDate = r.EntryDate,
                    Weapons = r.RunWeapons.Select(rw => new WeaponsDto{
                        WeaponId = rw.WeaponId,
                        Level = rw.Level,
                        IsEvolved = rw.IsEvolved
                    }).ToList(),
                    Tools = r.RunTools.Select(rt => new ToolsDto{
                        ToolId = rt.ToolId,
                        Level = rt.Level
                    }).ToList()
                }).ToListAsync();

                return Ok(runs);
        }

        [HttpGet("{id}")] //get singualr run
        public async Task<ActionResult<RunDto>> GetRun(int id)
        {
            var run = await _context.Runs
                .Include(r => r.RunWeapons)
                .Include(r => r.RunTools)
                .Where(r => r.Id == id) //filter by run id
                .Select(r => new RunDto{ 
                    Id = r.Id,
                    MapId = r.MapId,
                    CharacterId = r.CharacterId,
                    GoldEarned = r.GoldEarned,
                    EntryDate = r.EntryDate,
                    Weapons = r.RunWeapons.Select(rw => new WeaponsDto{
                        WeaponId = rw.WeaponId,
                        Level = rw.Level,
                        IsEvolved = rw.IsEvolved
                    }).ToList(),
                    Tools = r.RunTools.Select(rt => new ToolsDto{
                        ToolId = rt.ToolId,
                        Level = rt.Level
                    }).ToList()
                }).FirstOrDefaultAsync(); //Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements.

                if(run == null){return NotFound();} //if run cannot be found return not found
                return Ok(run);
        }
        
        [HttpPost]
        public async Task<ActionResult<RunDto>> CreateRun(RunDto runDto)
        {
            var run = new Run
            {
                MapId = runDto.MapId,
                CharacterId = runDto.CharacterId,
                GoldEarned = runDto.GoldEarned,
                EntryDate = runDto.EntryDate,
                RunWeapons = runDto.Weapons.Select(w => new RunWeapon
                {
                    WeaponId = w.WeaponId,
                    Level = w.Level,
                    IsEvolved = w.IsEvolved
                }).ToList(),
                RunTools = runDto.Tools.Select(t => new RunTool
                {
                    ToolId = t.ToolId,
                    Level = t.Level
                }).ToList()
            };

            _context.Runs.Add(run);
            await _context.SaveChangesAsync();

            runDto.Id = run.Id;

            return CreatedAtAction(nameof(GetRun), new { id = run.Id }, runDto);
        }
    }
}