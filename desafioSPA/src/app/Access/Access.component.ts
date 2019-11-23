import { Component, OnInit, ViewChild, ɵConsole } from '@angular/core';
import { AuthService } from '../_services/Auth/Auth.service';
import { states } from '../Models/States';
import { NgForm } from '@angular/forms';
import { ClientService } from '../_services/Client/Client.service';

@Component({
  selector: 'app-Access',
  templateUrl: './Access.component.html',
  styleUrls: ['./Access.component.css']
})
export class AccessComponent implements OnInit {

  @ViewChild('clientRegister', { static: true }) clientRegister: NgForm;
  modelClientRegister: any = { };
  states: any = states;
  CEPIsValid: boolean;
  IsAddressPresent = true;
  PhoneIsValid: boolean;
  clients: any;
  modelRead: any = { };
  constructor(private auth: AuthService, private client: ClientService) { }

  ngOnInit() {

    this.client.GetClients().subscribe(result => {
      console.log(result);
      this.clients = result;
    });

    const cep = new RegExp('[aA-zZ]+');
    this.clientRegister.form.valueChanges.subscribe(value => {
      if (cep.test(value.CepId) && value.CepId !== undefined) {
        console.log('cep nao ta valido');
        this.CEPIsValid = false;
      } else if (value.CepId !== undefined && value.CepId !== '') {
        this.CEPIsValid = true;
      }
    });
  }

  validCEP() {
    const rgxCep = new RegExp('^[0-9]{5}-?[\\d]{3}$');
    const CepId =  this.modelClientRegister.CepId;
    if (!rgxCep.test(CepId)) {
      this.CEPIsValid = false;
      return;
    }

    this.client.ViaCEP(CepId).subscribe(result => {
      this.CEPIsValid = true;
      console.log(result.body);
      this.modelClientRegister.State = result.body.uf;
      this.modelClientRegister.City = result.body.localidade;
      this.modelClientRegister.Address = result.body.logradouro;
      if (result.body.logradouro === '') {
        this.IsAddressPresent = false;
      } else { this.IsAddressPresent = true;}
    }, error => {
      this.CEPIsValid = false;
    });
  }

  validPhone() {
    // (\+[0-9]{2,3})?
    const rgx = /^((\([1-9]{2}\))|([1-9]{2}))( ?9?[0-9]{4}-?[0-9]{4})$/gm;
    const phoneNumber = this.modelClientRegister.PhoneNumber;
    if (!rgx.test(phoneNumber)) {
      this.PhoneIsValid = false;
      console.log('invalido');
    } else { this.PhoneIsValid = true; }
  }

  AddClient() {
    this.modelClientRegister.PhoneNumber = this.modelClientRegister.PhoneNumber.toString();
    this.modelClientRegister.CepId = parseInt(this.modelClientRegister.CepId, 10);
    this.modelClientRegister.NumberAddress = this.modelClientRegister.NumberAddress.toString();
    console.log(this.modelClientRegister);
    if (this.CEPIsValid && this.PhoneIsValid && this.modelClientRegister.Name && this.modelClientRegister.Country
        && this.modelClientRegister.NumberAddress) {
          this.client.AddClient(this.modelClientRegister).subscribe(result => {
            // console.log(result);
          });
    }
  }

  ReadClient(ClientId) {
    console.log(ClientId);
  }

  teste(value) {
    console.log(value);
  }

}
