import { Player } from "./player";

export class GameMove {
    GameMoveId: number;
    GameId: number;
    PlayerOneId: number;
    PlayerOneMove: string;
    PlayerTwoId: number;
    PlayerTwoMove: string;
    Round: number;
    Winner: number;
    Date: Date;
    PlayerOne: Player;
    PlayerTwo: Player;


    constructor() {
        {
            this.GameMoveId = 0;
            this.GameId = 0;
            this.PlayerOneId = 0;
            this.PlayerOneMove="";
            this.PlayerTwoId = 0;
            this.PlayerTwoMove = "";
            this.Round = 1;
            this.Winner = 0;
            this.Date = new Date();
            this.PlayerOne = new Player();
            this.PlayerTwo = new Player();
        }
    }
}