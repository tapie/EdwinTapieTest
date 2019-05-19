using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdwinTapieTest.Services.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public DateTime Date { get; set; }
        public virtual List<GameMove> GameMoves { get; set; }
    }
}
