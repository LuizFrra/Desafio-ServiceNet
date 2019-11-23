import { Component, OnInit, ViewChild, ɵConsole } from '@angular/core';
import { AuthService } from '../_services/Auth/Auth.service';
import { states } from '../Models/States';
import { NgForm } from '@angular/forms';
import { ClientService } from '../_services/Client/Client.service';
import { ClientCard } from '../Models/ClientCard';

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
  clients: Array<ClientCard>;
  modelRead: any = { };
  ClientIdToDelete: any;
  IsInEditionMode: boolean;
  ClientInReadMode: number;

  constructor(private auth: AuthService, private client: ClientService) { }

  ngOnInit() {

    this.client.GetClients().subscribe(result => {
      this.clients = result.reverse();
      // console.log(this.clients);
    });

    const cep = new RegExp('[aA-zZ]+');
    this.clientRegister.form.valueChanges.subscribe(value => {
      if (cep.test(value.CepId) && value.CepId !== undefined) {
        // console.log('cep nao ta valido');
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
      // console.log(result.body);
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
    const rgx = /^((\([1-9]{2}\))|([1-9]{2}))( ?9?[0-9]{4}-?[0-9]{4})$/gm;
    const phoneNumber = this.modelClientRegister.PhoneNumber;
    if (!rgx.test(phoneNumber)) {
      this.PhoneIsValid = false;
      // console.log('invalido');
    } else { this.PhoneIsValid = true; }
  }

  AddClient() {
    this.modelClientRegister.PhoneNumber = this.modelClientRegister.PhoneNumber.toString();
    this.modelClientRegister.CepId = parseInt(this.modelClientRegister.CepId.toString().replace('-', ''), 10);
    this.modelClientRegister.NumberAddress = this.modelClientRegister.NumberAddress.toString();

    // console.log(this.modelClientRegister);
    if (this.CEPIsValid && this.PhoneIsValid && this.modelClientRegister.Name && this.modelClientRegister.Country
        && this.modelClientRegister.NumberAddress) {
          this.client.AddClient(this.modelClientRegister).subscribe(result => {
            console.log(result);
            if (result.status === 201) {
              const client: ClientCard = {
                address: result.body.address,
                name: result.body.name,
                clientID: result.body.clientID,
                phoneNumber: result.body.phoneNumber
              };
              this.clients.unshift(client);
              this.modelClientRegister = { };
              this.PhoneIsValid = undefined;
              this.CEPIsValid = undefined;
            }
          });
    }
  }

  ReadClient(ClientId) {
    this.client.GetClientById(ClientId).subscribe(result => {
      // console.log(result);
      this.modelRead.address = result.address;
      this.modelRead.phoneNumber = result.phoneNumber;
      this.modelRead.name = result.name;
      this.modelRead.country = result.country;
      this.modelRead.cepId = result.cep.cepID;
      this.modelRead.city = result.cep.city;
      this.modelRead.state = result.cep.state;
      this.modelRead.numberAddress = result.numberAddress;
      this.modelRead.clientID = result.clientID;
      this.ClientInReadMode = ClientId;
    });
  }

  DeleteClient() {
    this.client.DeleteClient(this.ClientIdToDelete).subscribe(result => {
      // console.log(result);
      if (result.status === 200) {
        this.clients.splice(this.clients.findIndex(c => c.clientID === this.ClientIdToDelete), 1);
      }
    });
  }

  UpdateClient() {
    this.client.UpdateClient(this.modelRead).subscribe(result => {
      if (result.status === 200) {
        console.log('Update Sucesso');
        this.clients.splice(this.clients.findIndex(c => c.clientID === this.modelRead.cepId), 1);
        const client: ClientCard = {
          address: result.body.address,
          name: result.body.name,
          clientID: result.body.clientID,
          phoneNumber: result.body.phoneNumber
        };
        this.clients.push(client);
      }
      this.IsInEditionMode = false;
    });
  }
}
