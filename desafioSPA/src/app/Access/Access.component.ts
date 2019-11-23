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
  modelClient: any = {};
  states: any = states;
  CEPIsValid: boolean;
  IsAddressPresent = true;
  PhoneIsValid: boolean;
  clients: Array<ClientCard>;
  modelRead: any = {};
  ClientIdToDelete: any;
  IsInEditionMode: boolean;
  ClientInReadMode: number;
  IsInReadMode: boolean;

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
    const CepId = this.modelClient.CepId;
    if (!rgxCep.test(CepId)) {
      this.CEPIsValid = false;
      return;
    }

    this.client.ViaCEP(CepId).subscribe(result => {
      this.CEPIsValid = true;
      // console.log(result.body);
      this.modelClient.State = result.body.uf;
      this.modelClient.City = result.body.localidade;
      this.modelClient.Address = result.body.logradouro;
      if (result.body.logradouro === '') {
        this.IsAddressPresent = false;
      } else { this.IsAddressPresent = true; }
    }, error => {
      this.CEPIsValid = false;
    });
  }

  validPhone() {
    const rgx = /^((\([1-9]{2}\))|([1-9]{2}))( ?9?[0-9]{4}-?[0-9]{4})$/gm;
    const phoneNumber = this.modelClient.PhoneNumber;
    if (!rgx.test(phoneNumber)) {
      this.PhoneIsValid = false;
      // console.log('invalido');
    } else { this.PhoneIsValid = true; }
  }

  AddClient() {
    this.modelClient.PhoneNumber = this.modelClient.PhoneNumber.toString();
    this.modelClient.CepId = parseInt(this.modelClient.CepId.toString().replace('-', ''), 10);
    this.modelClient.NumberAddress = this.modelClient.NumberAddress.toString();

    // console.log(this.modelClient);
    if (this.CEPIsValid && this.PhoneIsValid && this.modelClient.Name && this.modelClient.Country
      && this.modelClient.NumberAddress) {
      this.client.AddClient(this.modelClient).subscribe(result => {
        // console.log(result);
        if (result.status === 201) {
          const client: ClientCard = {
            address: result.body.address,
            name: result.body.name,
            clientID: result.body.clientID,
            phoneNumber: result.body.phoneNumber
          };
          this.clients.unshift(client);
          this.modelClient = {};
          this.PhoneIsValid = undefined;
          this.CEPIsValid = undefined;
        }
      });
    }
  }

  ReadClient(ClientId) {
    this.IsInReadMode = true;
    this.IsInEditionMode = false;
    this.client.GetClientById(ClientId).subscribe(result => {
      // console.log(result);
      this.modelClient.Address = result.address;
      this.modelClient.PhoneNumber = result.phoneNumber;
      this.modelClient.Name = result.name;
      this.modelClient.Country = result.country;
      this.modelClient.CepId = result.cep.cepID;
      this.modelClient.City = result.cep.city;
      this.modelClient.State = result.cep.state;
      this.modelClient.NumberAddress = result.numberAddress;
      this.modelClient.clientID = result.clientID;
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
    this.client.UpdateClient(this.modelClient).subscribe(result => {
      if (result.status === 200) {
        console.log('Update Sucesso');
        this.clients.splice(this.clients.findIndex(c => c.clientID === this.modelClient.CepId), 1);
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
  DisableReadAndEdition() {
    this.IsInReadMode = false;
    this.IsInEditionMode = false;
  }
}
