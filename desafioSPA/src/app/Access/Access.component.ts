import { Component, OnInit, ViewChild, ɵConsole } from '@angular/core';
import { AuthService } from '../_services/Auth/Auth.service';
import { states } from '../Models/States';
import { NgForm } from '@angular/forms';
import { ClientService } from '../_services/Client/Client.service';
import { ClientCard } from '../Models/ClientCard';
import { DomSanitizer } from '@angular/platform-browser';

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
  googleRequest: any;
  isMapSet: boolean;

  constructor(private auth: AuthService, private client: ClientService, public sanitizer: DomSanitizer) { }

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
        this.isMapSet = false;
      } else {
        this.IsAddressPresent = true;
        const apiUrl = this.client.getAPIString(result.body.localidade, result.body.logradouro, CepId);
        this.googleRequest = this.sanitizer.bypassSecurityTrustResourceUrl(apiUrl);
        // this.client.getAPIString(result.body.localidade, result.body.logradouro, CepId);
        this.isMapSet = true;
      }
    }, error => {
      this.CEPIsValid = false;
    });
  }

  validPhone() {
    const rgx = /^[1-9]{2}9[1-9]{1}[1-9]{7}$/gm;
    const phoneNumber = this.modelClient.PhoneNumber;
    console.log(phoneNumber);
    if (rgx.test(phoneNumber)) {
      this.PhoneIsValid = true;
      // console.log('invalido');
    } else { this.PhoneIsValid = false; }
  }

  AddClient() {
    this.modelClient.PhoneNumber = this.modelClient.PhoneNumber.toString();
    this.modelClient.CepId = this.modelClient.CepId.toString().replace('-', '');
    this.modelClient.NumberAddress = this.modelClient.NumberAddress;
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
    this.PhoneIsValid = undefined;
    this.CEPIsValid = undefined;
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
      const apiUrl = this.client.getAPIString(this.modelClient.City, this.modelClient.Address, this.modelClient.CepId,
        this.modelClient.NumberAddress);
      this.googleRequest = this.sanitizer.bypassSecurityTrustResourceUrl(apiUrl);
      this.isMapSet = true;
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
        const apiUrl = this.client.getAPIString(this.modelClient.City, this.modelClient.Address, this.modelClient.CepId,
          this.modelClient.NumberAddress);
        this.googleRequest = this.sanitizer.bypassSecurityTrustResourceUrl(apiUrl);
        this.isMapSet = true;
        this.clients.push(client);
        this.PhoneIsValid = undefined;
        this.CEPIsValid = undefined;
      }
      this.IsInEditionMode = false;
    });
  }

  DisableReadAndEdition() {
    this.IsInReadMode = false;
    this.IsInEditionMode = false;
    this.isMapSet = false;
    this.googleRequest = undefined;
    this.modelClient = { };
    this.PhoneIsValid = undefined;
    this.CEPIsValid = undefined;
  }

  EnderecoOnBlur() {
    if (this.modelClient.Address !== '') {
      const apiUrl = this.client.getAPIString(this.modelClient.City, this.modelClient.Address, this.modelClient.CepId,
                                                                  this.modelClient.NumberAddress);
      this.googleRequest = this.sanitizer.bypassSecurityTrustResourceUrl(apiUrl);
      console.log(apiUrl);
      this.isMapSet = true;
    } else { this.isMapSet = false; }
  }
}
