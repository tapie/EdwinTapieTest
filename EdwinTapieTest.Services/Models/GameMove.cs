using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EdwinTapieTest.Services.Models
{
    public class GameMove
    {
        public int GameMoveId { get; set; }
        public int GameId { get; set; }
        [ForeignKey("PlayerOne")]
        public int? PlayerOneId { get; set; }
        [ForeignKey("MoveOne")]
        public string PlayerOneMove { get; set; }
        [ForeignKey("PlayerTwo")]
        public int? PlayerTwoId { get; set; }
        [ForeignKey("MoveTwo")]
        public string PlayerTwoMove { get; set; }
        public int Round { get; set; }
        [ForeignKey("WinnerPlayer")]
        public int? Winner { get; set; }
        public DateTime Date { get; set; }
        public virtual Game Game { get; set; }
        public virtual Player PlayerOne { get; set; }
        public virtual Player PlayerTwo { get; set; }
        public virtual Player WinnerPlayer { get; set; }
        public virtual Move MoveOne { get; set; }
        public virtual Move MoveTwo { get; set; }
    }
}
