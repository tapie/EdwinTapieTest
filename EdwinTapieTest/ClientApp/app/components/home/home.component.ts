import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { GameService } from '../../services/gameservice';
import { Game } from '../../models/game';
import { GameMove } from '../../models/gameMoves';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    homeForm: FormGroup;
    hasError: boolean;
    errorMessage: any;
    players: any;
    game: Game;
    gameMove: GameMove;

    constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
        private _gameService: GameService,
        private _router: Router) {
        //initialize variables
        this.game = new Game();
        this.hasError = false;
        this.players = [];
        this.gameMove = new GameMove();
        this.homeForm = this._fb.group({
            homeId: 0,
            playerOneName: ['', [Validators.required]],
            playerTwoName: ['', [Validators.required]]
        })
    }
    //function to save a game
    saveGame() {
        this._gameService.saveGame(this.game).subscribe(
            data => {
                this.gameMove.GameId = data.GameId;
                this.savePlayers();
            }, error => {
                this.errorMessage = error;
                this.hasError = true;
            });
    }
    //function to save players list
    savePlayers() {
        this._gameService.savePlayers(this.players).subscribe(
            data => {
                this.gameMove.PlayerOneId = data.playerOneId;
                this.gameMove.PlayerTwoId = data.playerTwoId;
                //Save in sessionStorage to use in all forms
                sessionStorage.setItem("GlobalGameMove", JSON.stringify(this.gameMove));
                //Redirection to get first move
                this._router.navigate(['/move']);
            }
            , error => {
                this.errorMessage = error;
                this.hasError = true;
            });
    }
    //Event Next button
    next() {
        if (!this.homeForm.valid) {
            alert('Please enter players names')
            return;
        } else {
            //Lógic to send request to save Game and Players
            if (this.playerOneName != null && this.playerTwoName != null) {
                this.players.push({ PlayerId: 0, PlayerName: this.playerOneName.value }, { PlayerId: 0, PlayerName: this.playerTwoName.value });
                this.gameMove.PlayerOne.Name = this.playerOneName.value;
                this.gameMove.PlayerTwo.Name = this.playerTwoName.value;
                this.game.Date = new Date();
                this.saveGame();
            }
        }
    }
    get playerOneName() { return this.homeForm.get('playerOneName'); }
    get playerTwoName() { return this.homeForm.get('playerTwoName'); }
}

