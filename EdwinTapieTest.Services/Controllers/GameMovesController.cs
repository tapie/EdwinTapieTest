using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EdwinTapieTest.Services.Models;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace EdwinTapieTest.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class GameMovesController : Controller
    {
        private readonly Context _context;
        private readonly ILogger<GameMovesController> _logger;

        public GameMovesController(Context context, ILogger<GameMovesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/GameMoves
        [HttpGet]
        public IEnumerable<GameMove> GetGameMoves()
        {
            return _context.GameMoves;
        }

        [HttpGet("GetGameMovesByGameId/{id}")]
        public async Task<IActionResult> GetGameMovesByGameId([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var gameMoves = await _context.GameMoves.Include(g => g.WinnerPlayer).Where(g => g.GameId == id).ToListAsync();

                return Json(gameMoves, new JsonSerializerSettings()
                {
                    NullValueHandling = true ? NullValueHandling.Ignore : NullValueHandling.Include,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Have occurred an error loading game moves, please try to contact the administrator.");
            }
        }

        // POST: api/GameMoves
        [HttpPost]
        public async Task<IActionResult> PostGameMove([FromBody] GameMove gameMove)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //mapping object 
                var obj = new GameMove
                {
                    GameMoveId = 0,
                    GameId = gameMove.GameId,
                    PlayerOneId = gameMove.PlayerOneId,
                    PlayerTwoId = gameMove.PlayerTwoId,
                    PlayerOneMove = gameMove.PlayerOneMove,
                    PlayerTwoMove = gameMove.PlayerTwoMove,
                    Round = gameMove.Round,
                    Date = DateTime.Now,
                };
                //logic to define the winner
                obj = DefineWinner(obj);
                //save gamemove
                _context.GameMoves.Add(obj);
                await _context.SaveChangesAsync();
                //validate if can continue with next round or if there is a winner
                var gameMovesList = await _context.GameMoves.Where(g => g.GameId == gameMove.GameId).AsNoTracking().ToListAsync();
                var playerOneWinGames = gameMovesList.Where(g => g.Winner == gameMove.PlayerOneId).Count();
                var playerTwoWinGames = gameMovesList.Where(g => g.Winner == gameMove.PlayerTwoId).Count();
                var canContinue = playerOneWinGames == 3 || playerTwoWinGames == 3 ? false : true;
                var nextRound = gameMove.Winner != null ? gameMove.Round + 1 : gameMove.Round;
                //repsonse object
                var response = new { nextRound, winner = obj.Winner, canContinue };
                return Json(response, new JsonSerializerSettings()
                {
                    NullValueHandling = true ? NullValueHandling.Ignore : NullValueHandling.Include,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Have occurred an error in the application saving the game move, please try to contact the administrator.");
            }
        }
        /// <summary>
        /// Method to define winner depends move
        /// </summary>
        /// <param name="gameMove"></param>
        /// <returns></returns>
        private GameMove DefineWinner(GameMove gameMove)
        {
            //comparison between player one and player two moves
            switch (gameMove.PlayerOneMove)
            {
                case Constants.Rock:
                    if (gameMove.PlayerTwoMove.Equals(Constants.Paper))
                        gameMove.Winner = gameMove.PlayerTwoId;
                    else if (gameMove.PlayerTwoMove.Equals(Constants.Scissors))
                        gameMove.Winner = gameMove.PlayerOneId;
                    else
                        gameMove.Winner = null;
                    break;
                case Constants.Paper:
                    if (gameMove.PlayerTwoMove.Equals(Constants.Rock))
                        gameMove.Winner = gameMove.PlayerOneId;
                    else if (gameMove.PlayerTwoMove.Equals(Constants.Scissors))
                        gameMove.Winner = gameMove.PlayerTwoId;
                    else
                        gameMove.Winner = null;
                    break;
                case Constants.Scissors:
                    if (gameMove.PlayerTwoMove.Equals(Constants.Rock))
                        gameMove.Winner = gameMove.PlayerTwoId;
                    else if (gameMove.PlayerTwoMove.Equals(Constants.Paper))
                        gameMove.Winner = gameMove.PlayerOneId;
                    else
                        gameMove.Winner = null;
                    break;
            }
            return gameMove;
        }

    }
}