import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private authKey: string;

  getAuthenticatedHttpOptions() {
    if (!this.isAuthenticated()) return;
    return {
      headers: new HttpHeaders({
        'Authorization': this.authKey
      })
    };
  }

  constructor(private _http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  public async authenticate(password: string, email: string): Promise<boolean> {
    await this._http.post(this.baseUrl + 'api/Authentication/', {
      "password": password,
      "email": email
    }, {responseType: "text"})
      .toPromise()
      .then(s => {
        this.authKey = s;
      });

    return this.isAuthenticated();
  }

  public isAuthenticated(): boolean {
    return this.authKey !== undefined && this.authKey !== null;
  }
}
