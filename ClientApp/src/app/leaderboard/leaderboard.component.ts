import {Component, OnInit} from '@angular/core';
import {LeaderboardEntry, LeaderboardService} from "../shared/leaderboard.service";

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.css']
})
export class LeaderboardComponent implements OnInit {
  leaderboard: LeaderboardEntry[];

  constructor(private _leaderboardService: LeaderboardService) {
  }

  ngOnInit() {
    this._leaderboardService.get().subscribe(leaderboard => {
      leaderboard.sort((a, b) => (b.score / b.duration) - (a.score / a.duration));
      this.leaderboard = leaderboard;
    });
  }

  truncate(number: number): number {
    return Math.trunc(number);
  }
}
