import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GameService } from '../../services/gameservice';
import { Move } from '../../models/move';
import { GameMove } from '../../models/gameMoves';

@Component({
    selector: 'move',
    templateUrl: './move.component.html'
})
export class MoveComponent implements OnInit {
    errorMessage: any;
    hasError: boolean;
    gameMove: GameMove;
    moveList: Array<Move> = [];
    gameMoveList: Array<GameMove> = [];
    playerName: string;
    moveSelected: string;
    isTimeToSave: boolean;
    actualPlayer: number;

    constructor(private _gameService: GameService,
        private _router: Router) {
        //initialize variables
        this.gameMove = <GameMove>JSON.parse(sessionStorage.GlobalGameMove);
        this.playerName = "";
        this.moveSelected = "";
        this.isTimeToSave = false;
        this.actualPlayer = this.gameMove.PlayerOneId;
        this.hasError = false;
    }
    //OnInit
    ngOnInit(): void {
        //get moves from database
        this._gameService.getMoveList().subscribe(
            data => this.moveList = data, error => this.errorMessage = error);
        this.playerName = this.gameMove.PlayerOne.Name;
    }
    //function to save a game
    saveGameMove() {
        this._gameService.saveGameMove(this.gameMove).subscribe(
            data => {
                this.hasError = false;
                var res = data;
                //get gamemoves from database
                this.getGameMoves();
                //if backend allows to continue
                if (res.canContinue) {
                    this.gameMove.PlayerOneMove = "";
                    this.gameMove.PlayerTwoMove = "";
                    this.gameMove.Round = data.nextRound;
                    //change to Player 1
                    this.playerName = this.gameMove.PlayerOne.Name;
                    this.moveSelected = "";
                    this.actualPlayer = this.gameMove.PlayerOneId;
                }
                //backend define winner
                else {
                    //Save the winner in sessionStorage
                    var winner = res.winner == this.gameMove.PlayerOneId ? this.gameMove.PlayerOne.Name : this.gameMove.PlayerTwo.Name;
                    sessionStorage.setItem("gameWinner", winner);
                    //Redirection to winner view
                    this._router.navigate(['/winner']);
                }
            }, error => {
                this.errorMessage = error._body;
                this.hasError = true;
            });
    }
    //function to get gamemoves
    getGameMoves() {
        this._gameService.getGameMoveList(this.gameMove.GameId).subscribe(
            data => this.gameMoveList = data, error => this.errorMessage = error);
    }
    //Event Next button
    next() {
        if (this.moveSelected != "") {
            //if is player 1's move
            if (this.actualPlayer == this.gameMove.PlayerOneId) {
                //map object gameMove
                this.gameMove.PlayerOneMove = this.moveSelected;
                //change the player 1 to player 2
                this.playerName = this.gameMove.PlayerTwo.Name;
                this.moveSelected = "";
                this.actualPlayer = this.gameMove.PlayerTwoId;
            }
            //if is player 2's move
            else {
                this.gameMove.PlayerTwoMove = this.moveSelected;
                this.saveGameMove();
            }
        }
    }
}

