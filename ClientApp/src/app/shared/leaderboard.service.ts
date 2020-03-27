import {Inject, Injectable} from '@angular/core';
import {GameSession} from "./game.service";
import {HttpClient} from "@angular/common/http";
import {AdminService} from "./admin.service";
import {ActivatedRoute, Router} from "@angular/router";
import {Category} from "./category.service";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class LeaderboardService {

  constructor(private _http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private _adminService: AdminService, private _router: Router, private _route: ActivatedRoute) {
  }

  addEntry(session: GameSession) {
    return this._http.post<LeaderboardEntry>(this.baseUrl + 'api/Leaderboard', session, this._adminService.getAuthenticatedHttpOptions()).subscribe(session => {
      this._router.navigate(['leaderboard']);
    }, () => this._router.navigate(['leaderboard']));
  }

  get(): Observable<LeaderboardEntry[]> {
    return this._http.get<LeaderboardEntry[]>(this.baseUrl + "api/Leaderboard");
  }

  delete(entry: LeaderboardEntry) {
    console.log(entry.id);
    return this._http.delete<Category>(this.baseUrl + 'api/Leaderboard/' + entry.id, this._adminService.getAuthenticatedHttpOptions());
  }
}

export interface LeaderboardEntry {
  id: string;
  user: string;
  score: number;
  timestamp: Date;
  category: Category;
  duration: number;
}
