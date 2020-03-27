import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestionComponent } from './question.component';
import {QuestionRoutingModule} from "./question-routing.module";
import {MatCardModule} from "@angular/material/card";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {MatButtonModule} from "@angular/material/button";
import {MatGridListModule} from "@angular/material/grid-list";
import {MatProgressBarModule} from "@angular/material/progress-bar";




@NgModule({
  declarations: [QuestionComponent],
    imports: [
        CommonModule,
        QuestionRoutingModule,
        MatCardModule,
        MatProgressSpinnerModule,
        MatButtonModule,
        MatGridListModule,
        MatProgressBarModule
    ]
})
export class QuestionModule { }
