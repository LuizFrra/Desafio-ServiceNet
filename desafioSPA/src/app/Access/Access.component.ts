import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/Auth/Auth.service';
import { states } from '../Models/States';

@Component({
  selector: 'app-Access',
  templateUrl: './Access.component.html',
  styleUrls: ['./Access.component.css']
})
export class AccessComponent implements OnInit {

  states: any = states;
  constructor(private auth: AuthService) { }

  ngOnInit() {
    console.log(this.auth.getUserName());
    console.log(this.states[0]);
  }

}
