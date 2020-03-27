import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {GameService, Result, ResultType} from "../../shared/game.service";
import {LeaderboardService} from "../../shared/leaderboard.service";

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent implements OnInit {

  result: Result;
  resultType: number;
  user: string;

  constructor(private _router: Router, private _route: ActivatedRoute, private _gameService: GameService, private _leaderboardService: LeaderboardService) {
    this._gameService.getResult().subscribe(r => {
      this.result = r;
      this.resultType = r.type;
    });
  }

  ngOnInit() {

  }

  next() {
    this._router.navigate(['game/question']);
  }

  startOver() {
    this._router.navigate(['']);
  }

  enterLeaderboard() {
    var session = this._gameService.getSession();

    session.request = this.user;

    this._leaderboardService.addEntry(session);
  }
}
