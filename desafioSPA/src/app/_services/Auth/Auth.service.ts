import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.prod';
import { HttpClient, HttpResponse, HttpResponseBase } from '@angular/common/http';
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
        return response;
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

  public emailExist(email: string) {
    console.log(this.baseURl);
    return this.http.get(this.baseURl + '/api/auth/emailexist/' + email, {observe: 'response'}).pipe(
      map((response: HttpResponse<any>) => {
        return response;
      })
    );
  }

  public logout() {
    localStorage.removeItem('token');
  }
}
