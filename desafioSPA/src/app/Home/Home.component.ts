import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { AuthService } from '../_services/Auth/Auth.service';

@Component({
  selector: 'app-Home',
  templateUrl: './Home.component.html',
  styleUrls: ['./Home.component.css']
})
export class HomeComponent implements OnInit {

  @ViewChild('registerForm', { static: true}) ngForm: NgForm;

  EmailIsValid: boolean;
  PasswordIsValid: boolean;
  NameIsValid: boolean;
  modelRegister: any = { };
  modelLogin: any = { };

  constructor(private authService: AuthService) { }

  ngOnInit() {
    let Name;
    let Email;
    let Password;
    const emailRegex = new RegExp('^[^\s@]+@[^\s@]+\.[^\s@]+$');
    this.ngForm.valueChanges.subscribe(change => {
      if (change.Name !== undefined && change.Name !== Name) {
        Name = change.Name;
        this.NameIsValid = Name.length === 0 ? false : true;
      }
      if (change.Email !== undefined && change.Email !== Email) {
        Email = change.Email;
        this.EmailIsValid = emailRegex.test(Email);
      }
      if (change.Password !== undefined && change.Password !== Password) {
        Password = change.Password;
        this.PasswordIsValid = Password.length > 7  ? true : false;
      }
    });
  }

  register() {
    if (this.EmailIsValid && this.PasswordIsValid && this.NameIsValid) {
      this.authService.register(this.modelRegister).subscribe(response => {
        console.log('Registrado Com Sucesso');
      }, error =>{
        console.log('Erro ao Registrar:\n');
        console.log(error);
      });
      this.zerarInputs();
    }
  }

  private zerarInputs() {
    this.modelRegister.Name = undefined;
    this.modelRegister.Email = undefined;
    this.modelRegister.Password = undefined;
    this.EmailIsValid = undefined;
    this.PasswordIsValid = undefined;
    this.NameIsValid = undefined;
  }
}
