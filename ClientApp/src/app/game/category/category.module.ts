import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {CategoryComponent} from "./category.component";
import {CategoryRoutingModule} from "./category-routing.module";
import {MatListModule} from "@angular/material/list";
import {MatCardModule} from "@angular/material/card";
import {MatButtonModule} from "@angular/material/button";



@NgModule({
  declarations: [CategoryComponent],
    imports: [
        CommonModule,
        CategoryRoutingModule,
        MatListModule,
        MatCardModule,
        MatButtonModule
    ]
})
export class CategoryModule { }
