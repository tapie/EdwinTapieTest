using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EdwinTapieTest.Services.Models;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace EdwinTapieTest.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class GamesController : Controller
    {
        private readonly Context _context;
        private readonly ILogger<GamesController> _logger;

        public GamesController(Context context, ILogger<GamesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Games
        [HttpGet]
        public IEnumerable<Game> GetGames()
        {
            return _context.Games;
        }

        // POST: api/Games
        [HttpPost]
        public async Task<IActionResult> PostGame([FromBody] Game game)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                game.Date = DateTime.Now;
                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                return Json(game, new JsonSerializerSettings()
                {
                    NullValueHandling = true ? NullValueHandling.Ignore : NullValueHandling.Include,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Have occurred an error in the application saving the game, please try to contact the administrator.");
            }
        }
    }
}