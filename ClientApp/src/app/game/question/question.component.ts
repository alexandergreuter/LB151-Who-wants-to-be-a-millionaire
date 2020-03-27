import {Component, OnInit} from '@angular/core';
import {GameService, ResultType} from "../../shared/game.service";
import {Question} from "../../shared/category.service";
import {ActivatedRoute, Router} from "@angular/router";

const timePerQuestion: number = 30;

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {
  question: Question;
  timeLeft: number = 100;
  answered: boolean = false;
  disableAllButtons: boolean = false;
  disabledButtons: string[] = [];
  jokerUsed: boolean = false;
  timePerQuestion: number = timePerQuestion;

  constructor(private _router: Router, private _route: ActivatedRoute, private _gameService: GameService) {
  }

  ngOnInit() {
    this._gameService.getQuestion().subscribe(question => {
      this.question = question;
      console.log(this.question);
      this.countdown();
    });
    this.jokerUsed = this._gameService.getJokerUsed();
  }

  next(answer: string) {
    this.answered = true;
    this._gameService.submitAnswer(answer);
  }

  countdown() {
    // Accounts for delays from the server and relativates to 100 which is the loading bars value.
    console.log((new Date().getTime() - new Date(this.question.asked).getTime()).toString());
    var tickInMilliSeconds = (timePerQuestion - (new Date().getTime() - new Date(this.question.asked).getTime())) / 100 * 1000;

    var handle = setInterval(() => {
      this.timeLeft--;
      if (this.timeLeft <= 0 && this.answered == false) {
        this.disableAllButtons = true;

        setTimeout(()=> this._gameService.submitAnswer(""), 1000);

        clearInterval(handle);
      }
    }, tickInMilliSeconds)
  }

  useJoker() {
    this._gameService.useJoker().subscribe(toDisable => {
      this.disabledButtons = toDisable;
      this.jokerUsed = true;
    });
  }

  truncate(number: number): number {
    return Math.trunc(number);
  }
}
