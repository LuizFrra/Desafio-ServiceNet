import { AuthService } from '../_services/Auth/Auth.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { delay } from 'q';

@Component({
  selector: 'app-Home',
  templateUrl: './Home.component.html',
  styleUrls: ['./Home.component.css']
})
export class HomeComponent implements OnInit {

  @ViewChild('registerForm', { static: true }) registerForm: NgForm;
  @ViewChild('loginForm', { static: true }) loginForm: NgForm;

  EmailIsValid: boolean;
  PasswordIsValid: boolean;
  NameIsValid: boolean;
  modelRegister: any = {};
  modelLogin: any = {};
  FailInLogin: boolean;
  SucefullRegister: boolean;
  EmailIsInvalidEmailExist: boolean;

  constructor(private authService: AuthService, private route: Router) { }

  ngOnInit() {
    if (this.authService.isAuthenticated) {
      this.route.navigate(['/acesso']);
    }
    let Name;
    let Email;
    let Password;
    const emailRegex = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    this.registerForm.valueChanges.subscribe(change => {
      if (change.Name !== undefined && change.Name !== Name) {
        Name = change.Name;
        this.NameIsValid = Name.length === 0 ? false : true;
      }
      if (change.Email !== undefined && change.Email !== Email) {
        Email = change.Email;
        this.EmailIsValid = emailRegex.test(Email);
        this.EmailIsInvalidEmailExist = false;
      }
      if (change.Password !== undefined && change.Password !== Password) {
        Password = change.Password;
        this.PasswordIsValid = Password.length > 7 ? true : false;
      }
    });
  }

  register() {
    if (this.EmailIsValid && this.PasswordIsValid && this.NameIsValid) {
      this.authService.register(this.modelRegister).subscribe(async response => {
        console.log('Registrado Com Sucesso');
        if (response.status === 201) { this.SucefullRegister = true; await delay(5000); this.SucefullRegister = false; }
      }, error => {
        console.log('Erro ao Registrar:\n');
        console.log(error);
      });
      // this.zerarInputs();
    }
  }

  login() {
    // console.log(this.modelLogin);
    this.authService.login(this.modelLogin).subscribe(response => {
      if (response === 200) {
        console.log('logged In');
        this.route.navigate(['/acesso']);
      }
    }, error => {
      console.log(error.status);
      this.FailInLogin = true;
    });
  }

  private zerarInputs() {
    this.modelRegister.Name = undefined;
    this.modelRegister.Email = undefined;
    this.modelRegister.Password = undefined;
    this.EmailIsValid = undefined;
    this.PasswordIsValid = undefined;
    this.NameIsValid = undefined;
  }

  EmailExist() {
    if (this.EmailIsValid === true) {
      this.authService.emailExist(this.modelRegister.Email).subscribe(result => {
        console.log(result);
        if (result.body === true) {
          this.EmailIsValid = false;
          this.EmailIsInvalidEmailExist = true;
        }
      });
    }
  }
}
