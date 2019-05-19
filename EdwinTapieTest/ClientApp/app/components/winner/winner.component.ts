import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'winner',
    templateUrl: './winner.component.html'
})
export class WinnerComponent {
    gameWinner: string;

    constructor(private _router: Router) {
        //initialize variables
        this.gameWinner = sessionStorage.gameWinner;
    }
    //Event Next button
    playAgain() {
        //Redirection to winner view
        this._router.navigate(['/home']);
    }
}

