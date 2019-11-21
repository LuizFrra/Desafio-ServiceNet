import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpResponse, HttpEvent, HttpResponseBase } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseURl: string;

constructor(private http: HttpClient) {
  this.baseURl = environment.hostName + ':' + environment.port;
}

  register(model: any) {
    console.log(this.baseURl + '/api/home/register');
    return this.http.post(this.baseURl + '/api/auth/register', model, { observe: 'response' }).pipe(
      map((response: HttpResponseBase) => {
        if(response.status === 201) {
          console.log('do something');
        }
      })
    );
  }

}
