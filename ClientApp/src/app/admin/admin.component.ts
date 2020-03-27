import {Component, OnInit} from '@angular/core';
import {Router, ActivatedRoute} from "@angular/router";
import {AdminService} from "../shared/admin.service";
import {Category, CategoryService, Question} from "../shared/category.service";
import {LeaderboardEntry, LeaderboardService} from "../shared/leaderboard.service";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  private password: string;
  private email: string;
  private loginMessage: string;
  private loading: boolean = false;

  loggedIn: boolean;
  private categories: Category[] = [];
  private managed: Category;
  private newCategory: string;

  private readonly emptyQuestion: Question = {
    answers: null,
    correct: "",
    name: ""
  };
  private newQuestion: Question = Object.assign({}, this.emptyQuestion);
  private managedError: string = null;
  private leaderboard: LeaderboardEntry[];

  constructor(private _router: Router, private _adminService: AdminService, private _categoryService: CategoryService, private _leaderboardService: LeaderboardService) {
    this.newQuestion.answers = [null, null, null, null];
  }

  ngOnInit(): void {
    this.loggedIn = this._adminService.isAuthenticated();
    if (this.loggedIn) {
      this.loading = true;

      this._categoryService.getCategories()
        .subscribe(categories => categories
            .forEach(category => this.categories.push(category)),
          () => null,
          () => this.loading = false
        );
      this.loading = false;
    }
    this._leaderboardService.get().subscribe(leaderboard => {
      leaderboard.sort((a, b) => (b.score / b.duration) - (a.score / a.duration));
      this.leaderboard = leaderboard;
    });
  }

  login() {
    this.loading = true;

    this._adminService.authenticate(this.password, this.email).then(r => {
        if (r) {
          this.loginMessage = "";
          this.loggedIn = true;
          this._categoryService.getCategories()
            .subscribe(categories => categories
                .forEach(category => this.categories.push(category)),
              () => null,
              () => this.loading = false
            );
          this.loading = false;
        } else {
          this.loginMessage = "Login failed, please check your password and try again.";
          this.loading = false;
        }
      }
    )
  }

  deleteCategory(category: Category) {

    this._categoryService.deleteCategory(category).subscribe(() => null, () => null, () => {
      this.loading = false;
      this.categories.forEach((item, index) => {
        if (item === category) this.categories.splice(index, 1);
      });
      if (this.managed == category) {
        this.managed = null;
      }
    });
  }

  deleteQuestion(question: Question) {
    this.managed.questions.forEach((item, index) => {
      if (item === question) this.managed.questions.splice(index, 1);
    });
  }

  applyChanges() {
    this.loading = true;

    this._categoryService.updateCategory(this.managed).subscribe(() => null, () => null, () => {
      this.categories[this.categories.findIndex(category => category.id == this.managed.id)] = this.managed;
      this.loading = false;
      this.managed = null;
    });
  }

  addCategory() {
    this.loading = true;

    this._categoryService.createCategory(this.newCategory)
      .subscribe((category) => this.categories.push(category),
        () => null,
        () => this.loading = false
      );

    this.newCategory = null;
  }

  addQuestion() {
    for (let [key, value] of Object.entries(this.newQuestion)) {
      if (value == null || value === "") {
        this.managedError = "Please check all your inputs";
        return;
      }
    }

    this.newQuestion.correct = this.newQuestion.answers[Number.parseInt(this.newQuestion.correct)];

    if (this.managed.questions == null) {
      this.managed.questions = [];
    }

    this.managed.questions.push(this.newQuestion);
    this.newQuestion = Object.assign({}, this.emptyQuestion);
    this.newQuestion.answers = [null, null, null, null];
  }

  truncate(number: number): number {
    return Math.trunc(number);
  }

  deleteLeaderboardEntry(entry: LeaderboardEntry) {
    this.loading = true;

    this.leaderboard.forEach((item, index) => {
      if (item === entry) this.leaderboard.splice(index, 1);
    });

    this._leaderboardService.delete(entry).subscribe(() => null, () => null, () => {
      this.loading = false;
    });
  }
}
