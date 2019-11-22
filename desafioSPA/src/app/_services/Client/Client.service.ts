import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpEvent, HttpResponseBase, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { map } from 'rxjs/operators';
import { JsonPipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  private baseURl: string;
  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json'), observe: 'response' as 'body'};

  constructor(private http: HttpClient) {
  this.baseURl = environment.hostName + ':' + environment.port;
 }

 ViaCEP(CEP: string) {
  return this.http.post(this.baseURl + '/api/client/ObterEndereco', JSON.stringify(CEP), this.options).pipe(
    map((response: HttpResponse<any>) => {
      return response;
    }
  ));
}

  AddClient(model: any) {
    return this.http.post(this.baseURl + '/api/client/add', model, { observe: 'response'}).pipe(
      map((response: HttpResponse<any>) => {
        // console.log(response);
        return response;
      }
      ));
  }

  GetClients(): any {
    return this.http.get(this.baseURl + '/api/client/getall').pipe(
      map((response: HttpResponse<any>) => {
        return response;
      })
    );
  }

}
