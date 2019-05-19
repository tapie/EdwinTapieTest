import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class GameService {
    servicesUrl: string = "";

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.servicesUrl = baseUrl;
    }
    /**function to get all moves */
    getMoveList() {
        return this._http.get(this.servicesUrl + 'api/Moves')
            .map(res => res.json())
            .catch(this.errorHandler);
    }
    /**
     * Function to get gamemoves while player are playing
     * @param id
     */
    getGameMoveList(id) {
        return this._http.get(this.servicesUrl + 'api/GameMoves/GetGameMovesByGameId/' + id)
            .map(res => res.json())
            .catch(this.errorHandler);
    }
    /**
     * function to save a game
     * @param game object game
     */
    saveGame(game) {
        return this._http.post(this.servicesUrl + 'api/Games', game)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }
    /**
     * function to save players
     * @param players list of players names
     */
    savePlayers(players) {
        return this._http.post(this.servicesUrl + 'api/Players', players)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }
    /**
     * function to save a move
     * @param move
     */
    saveGameMove(gameMove) {
        return this._http.post(this.servicesUrl + 'api/GameMoves', gameMove)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }
    /**
     * function to manage the errors
     * @param error
     */
    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }
}  