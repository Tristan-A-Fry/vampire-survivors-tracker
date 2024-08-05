using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            // Debugging statements to check if User claims are being accessed correctly
            if (User == null)
            {
                return Unauthorized("User context is not available.");
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID claim is not available.");
            }

            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized("User ID claim is invalid.");
            } 
            var runs = await _context.Runs
                .Where(r => r.UserId == userId)
                .Include(r => r.RunWeapons)
                .Include(r => r.RunTools)
                .Select(r => new RunDto
                {
                    Id = r.Id,
                    MapId = r.MapId,
                    CharacterId = r.CharacterId,
                    GoldEarned = r.GoldEarned,
                    EntryDate = r.EntryDate,
                    Weapons = r.RunWeapons.Select(rw => new WeaponsDto
                    {
                        WeaponId = rw.WeaponId,
                        Level = rw.Level,
                        IsEvolved = rw.IsEvolved
                    }).ToList(),
                    Tools = r.RunTools.Select(rt => new ToolsDto
                    {
                        ToolId = rt.ToolId,
                        Level = rt.Level
                    }).ToList()
                }).ToListAsync();

            return Ok(runs);
        }

        [HttpGet("{id}")] //get singualr run
        public async Task<ActionResult<RunDto>> GetRun(int id)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var run = await _context.Runs
                .Include(r => r.RunWeapons)
                .Include(r => r.RunTools)
                .Where(r => r.Id == id && r.UserId == userId) //filter by run ida and userId
                .Select(r => new RunDto
                {
                    Id = r.Id,
                    MapId = r.MapId,
                    CharacterId = r.CharacterId,
                    GoldEarned = r.GoldEarned,
                    EntryDate = r.EntryDate,
                    Weapons = r.RunWeapons.Select(rw => new WeaponsDto
                    {
                        WeaponId = rw.WeaponId,
                        Level = rw.Level,
                        IsEvolved = rw.IsEvolved
                    }).ToList(),
                    Tools = r.RunTools.Select(rt => new ToolsDto
                    {
                        ToolId = rt.ToolId,
                        Level = rt.Level
                    }).ToList()
                }).FirstOrDefaultAsync(); //Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements.

            if (run == null) { return NotFound(); } //if run cannot be found return not found
            return Ok(run);
        }

        [HttpGet("bymap/{mapId}")]
        public async Task<ActionResult<IEnumerable<RunDto>>> GetRunsByMap(int mapId)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var runs = await _context.Runs
                .Where(r => r.MapId == mapId)
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
                    }).ToList(),
                }).ToListAsync();
                return Ok(runs);
        }

        [HttpPost]
        public async Task<ActionResult<RunDto>> CreateRun(RunDto runDto)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID claim is not available.");
            }

            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized("User ID claim is invalid.");
            }

            // Check if related entities exist
            var mapExists = await _context.Map.AnyAsync(m => m.Id == runDto.MapId);
            if (!mapExists)
            {
                return BadRequest("Map does not exist.");
            }

            var characterExists = await _context.Characters.AnyAsync(c => c.Id == runDto.CharacterId);
            if (!characterExists)
            {
                return BadRequest("Character does not exist.");
            }

            var run = new Run
            {
                MapId = runDto.MapId,
                CharacterId = runDto.CharacterId,
                GoldEarned = runDto.GoldEarned,
                EntryDate = runDto.EntryDate,
                UserId = userId,
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRun(int id, RunDto runDto)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //Check to see if the run exists
            if (id != runDto.Id)
            {
                return BadRequest();
            }

            var run = await _context.Runs
                .Include(r => r.RunWeapons)
                .Include(r => r.RunTools)
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

            if (run == null)
            {
                return NotFound();
            }

            run.MapId = runDto.MapId;
            run.CharacterId = runDto.CharacterId;
            run.GoldEarned = runDto.GoldEarned;
            run.EntryDate = runDto.EntryDate;

            //Update RunWeapons
            _context.RunWeapons.RemoveRange(run.RunWeapons);
            run.RunWeapons = runDto.Weapons.Select(w => new RunWeapon
            {
                RunId = run.Id,
                WeaponId = w.WeaponId,
                Level = w.Level,
                IsEvolved = w.IsEvolved
            }).ToList();

            //Update RunTools
            _context.RunTools.RemoveRange(run.RunTools);
            run.RunTools = runDto.Tools.Select(t => new RunTool
            {
                RunId = run.Id,
                ToolId = t.ToolId,
                Level = t.Level
            }).ToList();

            await _context.SaveChangesAsync();
            return NoContent(); //return no content becuase we are just updating a run
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRun(int id)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var run = await _context.Runs
                .Include(r => r.RunWeapons)
                .Include(r => r.RunTools)
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

            if (run == null)
            {
                return NotFound();
            }

            _context.Runs.Remove(run);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}