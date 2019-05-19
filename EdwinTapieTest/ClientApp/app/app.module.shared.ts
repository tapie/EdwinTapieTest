import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { BrowserModule } from '@angular/platform-browser';
import { GameService } from './services/gameservice';
import { MoveComponent } from './components/move/move.component';
import { WinnerComponent } from './components/winner/winner.component';

@NgModule({
    declarations: [
        AppComponent,
        MoveComponent,
        HomeComponent,
        WinnerComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'move', component: MoveComponent },
            { path: 'winner', component: WinnerComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [GameService]
})
export class AppModuleShared {
}
