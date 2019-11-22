import { Component, OnInit, ViewChild, ɵConsole } from '@angular/core';
import { AuthService } from '../_services/Auth/Auth.service';
import { states } from '../Models/States';
import { NgForm } from '@angular/forms';
import { ClientService } from '../_services/Client/Client.service';
import { strictEqual } from 'assert';

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
  constructor(private auth: AuthService, private client: ClientService) { }

  ngOnInit() {
    const cep = new RegExp('[aA-zZ]+');
    this.clientRegister.form.valueChanges.subscribe(value => {
      if (cep.test(value.CEP) && value.CEP !== undefined) {
        console.log('cep nao ta valido');
        this.CEPIsValid = false;
      } else if (value.CEP !== undefined && value.CEP !== '') {
        this.CEPIsValid = true;
      }
    });
  }

  validCEP() {
    const rgxCep = new RegExp('^[0-9]{5}-?[\\d]{3}$');
    const CEP =  this.modelClientRegister.CEP;
    if (!rgxCep.test(CEP)) {
      this.CEPIsValid = false;
      return;
    }

    this.client.ViaCEP(CEP).subscribe(result => {
      this.CEPIsValid = true;
      console.log(result.body);
      this.modelClientRegister.State = result.body.uf;
      this.modelClientRegister.City = result.body.localidade;
      this.modelClientRegister.Address = result.body.logradouro;
      if (result.body.logradouro === '') {
        this.IsAddressPresent = false;
        console.log('nao tem endereco');
      } else { this.IsAddressPresent = true; console.log('tem endereco'); }

      console.log(result.body.uf);
    }, error => {
      this.CEPIsValid = false;
    });
  }
}
