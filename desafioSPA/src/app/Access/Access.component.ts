import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../_services/Auth/Auth.service';
import { states } from '../Models/States';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-Access',
  templateUrl: './Access.component.html',
  styleUrls: ['./Access.component.css']
})
export class AccessComponent implements OnInit {

  @ViewChild('clientRegister', { static: true }) clientRegister: NgForm;
  modelClientRegister: any;
  states: any = states;
  constructor(private auth: AuthService) { }

  ngOnInit() {
    console.log(this.auth.getUserName());
    console.log(this.states[0]);
  }

}
