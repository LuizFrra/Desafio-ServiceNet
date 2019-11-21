import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/Auth/Auth.service';

@Component({
  selector: 'app-Access',
  templateUrl: './Access.component.html',
  styleUrls: ['./Access.component.css']
})
export class AccessComponent implements OnInit {

  constructor(private auth: AuthService) { }

  ngOnInit() {
    console.log(this.auth.getUserName());
  }

}
