import { Injectable } from '@angular/core';
import { AuthService } from '../Auth/Auth.service';
import { Router, CanActivate } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class GuardService implements CanActivate {

constructor(private auth: AuthService, private router: Router) { }

canActivate(): boolean {
  if (!this.auth.isAuthenticated()) {
    this.router.navigate(['']);
    return false;
  }
  return true;
}

}
