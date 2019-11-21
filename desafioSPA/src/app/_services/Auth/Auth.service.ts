import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpResponse, HttpEvent, HttpResponseBase } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseURl: string;

constructor(private http: HttpClient, private jwtHelper: JwtHelperService) {
  this.baseURl = environment.hostName + ':' + environment.port;
}

  register(model: any) {
    console.log(this.baseURl + '/api/auth/register');
    return this.http.post(this.baseURl + '/api/auth/register', model, { observe: 'response' }).pipe(
      map((response: HttpResponseBase) => {
        if (response.status === 201) {
          console.log('do something');
        }
      })
    );
  }

  login(model: any) {
    console.log(this.baseURl + '/api/auth/login');
    return this.http.post(this.baseURl + '/api/auth/login', model, { observe: 'response'}).pipe(
      map((response: HttpResponse<any>) => {
        if (response.status === 200) {
          const token = response.body.token;
          if (token) {
            localStorage.setItem('token', token);
          }
          return response.status;
        }
      })
    );
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    const isExpired = this.jwtHelper.isTokenExpired(token);
    if (isExpired) {
      localStorage.removeItem('token');
    }
    return !isExpired;
  }

  public getUserName(): string {
    const token = localStorage.getItem('token');
    if (token) {
      const tokenDecode = this.jwtHelper.decodeToken(token);
      if (tokenDecode.user_name) {
        return tokenDecode.user_name;
      }
    }
    return '';
  }

}
