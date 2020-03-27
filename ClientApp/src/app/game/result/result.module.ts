import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ResultComponent} from './result.component';
import {ResultRoutingModule} from "./result-routing.module";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatCardModule} from "@angular/material/card";
import {MatButtonModule} from "@angular/material/button";
import {MatInputModule} from "@angular/material/input";
import {FormsModule} from "@angular/forms";


@NgModule({
  declarations: [ResultComponent],
  imports: [
    CommonModule,
    ResultRoutingModule,
    MatFormFieldModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    FormsModule
  ]
})
export class ResultModule {
}
