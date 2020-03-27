import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AdminService} from "./admin.service";
import {Category, Question} from "./category.service";
import {ActivatedRoute, Router} from "@angular/router";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class GameService {

  private jokerUsed: boolean;
  private _gameSession: GameSession;

  constructor(private _http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private _adminService: AdminService, private _router: Router, private _route: ActivatedRoute) {
  }

  startGame(category: Category) {
    this._gameSession = null;
    this.jokerUsed = false;

    return this._http.post<GameSession>(this.baseUrl + 'api/Game/Start', category, this._adminService.getAuthenticatedHttpOptions()).subscribe(session => {
      this._gameSession = session;
      this._router.navigate(['game/question']);
    });
  }

  getQuestion(): Observable<Question> {
    if (this._gameSession === undefined) {
      this._router.navigate(['game/categories']);
    }
    return this._http.post<Question>(this.baseUrl + 'api/Game/Question', this._gameSession, this._adminService.getAuthenticatedHttpOptions());
  }

  submitAnswer(answer: string) {
    if (this._gameSession === undefined) {
      this._router.navigate(['game/categories']);
    }
    this._gameSession.request = answer;
    return this._http.post<GameSession>(this.baseUrl + 'api/Game/Answer', this._gameSession, this._adminService.getAuthenticatedHttpOptions()).subscribe(r => {
      this._gameSession = r;
      this._router.navigate(['game/result']);
    });
  }

  getPoints(): number {
    return this._gameSession.points;
  }

  getJokerUsed(): boolean {
    return this.jokerUsed;
  }

  getResult(): Observable<Result> {
    if (this._gameSession === undefined) {
      this._router.navigate(['game/categories']);
    }
    return this._http.post<Result>(this.baseUrl + 'api/Game/Result', this._gameSession, this._adminService.getAuthenticatedHttpOptions())
  }

  useJoker(): Observable<string[]> {
    if (this._gameSession === undefined) {
      this._router.navigate(['game/categories']);
      return;
    }

    this.jokerUsed = true;

    return this._http.post<string[]>(this.baseUrl + 'api/Game/Joker', this._gameSession, this._adminService.getAuthenticatedHttpOptions())
  }

  getSession() {
    return this._gameSession;
  }
}

export interface GameSession {
  id: string;
  request: string;
  points: number;
}

export interface Result {
  type: ResultType;
  correct: string;
}

export enum ResultType {
  CORRECT,
  WRONG,
  SLOW,
  WON
}
