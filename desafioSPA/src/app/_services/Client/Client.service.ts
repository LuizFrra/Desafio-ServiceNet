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
  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json'), observe: 'response' as 'body' };

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
    return this.http.post(this.baseURl + '/api/client/add', model, { observe: 'response' }).pipe(
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

  GetClientById(ClientId: number): any {
    return this.http.get(this.baseURl + '/api/client/getbyid/' + ClientId).pipe(
      map((response: HttpResponse<any>) => {
        return response;
      })
    );
  }

  DeleteClient(ClientId: number): any {
    return this.http.delete(this.baseURl + '/api/client/delete/' + ClientId, { observe: 'response' }).pipe(
      map((response: HttpResponse<any>) => {
        return response;
      })
    );
  }

  UpdateClient(Client: any): any {
    return this.http.put(this.baseURl + '/api/client/update', Client, { observe: 'response' }).pipe(
      map((response: HttpResponse<any>) => {
        return response;
      })
    );
  }

  getAPIString(cidade: string, rua: string, cep: number, numero = -1): string {
    let parameter = cidade.replace(/ /g, '+') + '+' + rua.replace(/ /g, '+');
    if (numero !== -1) { parameter += '+' + numero.toString(); }
    parameter += '+' + cep.toString();
    return environment.googleAPIUrl + parameter + environment.googleAPIKey;
  }
}
