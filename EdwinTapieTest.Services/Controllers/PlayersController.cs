using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EdwinTapieTest.Services.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EdwinTapieTest.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class PlayersController : Controller
    {
        private readonly Context _context;
        private readonly ILogger<PlayersController> _logger;

        public PlayersController(Context context, ILogger<PlayersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Players
        [HttpGet]
        public IEnumerable<Player> GetPlayers()
        {
            return _context.Players;
        }
        // POST: api/Players
        [HttpPost]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> PostPlayer([FromBody] List<Player> players)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (players.Count == 2)
                {
                    await _context.Players.AddRangeAsync(players);
                    await _context.SaveChangesAsync();

                    var res = new { playerOneId = players[0].PlayerId, playerTwoId = players[1].PlayerId };
                    return Json(res, new JsonSerializerSettings()
                    {
                        NullValueHandling = true ? NullValueHandling.Ignore : NullValueHandling.Include,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }
                else
                    return BadRequest("Needs to 2 players names");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Have occurred an error in the application, please try to contact the administrator.");
            }
        }
    }
}