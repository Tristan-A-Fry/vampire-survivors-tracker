using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Data;
using web_api.Models;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
       private readonly AppDbContext _context; 

       public MapController(AppDbContext context)
       {
            _context = context;
       }

       [HttpGet]
       public async Task<ActionResult<IEnumerable<Maps>>> GetMaps()
       {
            var maps = await _context.Map.ToListAsync();

            return Ok(maps);
       }
    }
}