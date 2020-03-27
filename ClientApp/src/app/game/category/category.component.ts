import {Component, OnInit} from '@angular/core';
import {Router, ActivatedRoute} from "@angular/router";
import {Category, CategoryService} from "../../shared/category.service";
import {GameService} from "../../shared/game.service";

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categories: Category[] = [];

  constructor(private _categoryService: CategoryService, private _router: Router, private _route: ActivatedRoute, private _gameService: GameService) {

  }

  ngOnInit() {
    this._categoryService.getCategories().subscribe(categories => categories.forEach(category => this.categories.push(category)));
  }

  next(category: Category) {
    this._gameService.startGame(category);
  }
}
