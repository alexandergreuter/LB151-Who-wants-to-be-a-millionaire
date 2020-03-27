import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {AdminService} from "./admin.service";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private _http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private _adminService: AdminService) {
  }

  public createCategory(name: string): Observable<Category> {
    console.log(this._adminService.getAuthenticatedHttpOptions());
    return this._http.post<Category>(this.baseUrl + 'api/Category/', {"name": name}, this._adminService.getAuthenticatedHttpOptions());
  }

  getCategories(): Observable<Category[]> {
    console.log(this._adminService.getAuthenticatedHttpOptions());
    return this._http.get<Category[]>(this.baseUrl + 'api/Category/', this._adminService.getAuthenticatedHttpOptions());
  }

  deleteCategory(category: Category) {
    console.log(this.baseUrl + 'api/Category/' + category.id);
    return this._http.delete(this.baseUrl + 'api/Category/' + category.id, this._adminService.getAuthenticatedHttpOptions());
  }

  updateCategory(category: Category): Observable<Category> {
    return this._http.put<Category>(this.baseUrl + 'api/Category/' + category.id, category, this._adminService.getAuthenticatedHttpOptions());
  }
}

export interface Question {
  name: string;
  answers: string[];
  correct?: string;
  asked?: string;
  right?: number;
  wrong?: number;
}

export interface Category {
  id: string;
  name: string;
  questions?: Question[];
}
