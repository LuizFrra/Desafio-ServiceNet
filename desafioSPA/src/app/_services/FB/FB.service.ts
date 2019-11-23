import { Injectable } from '@angular/core';
import { AuthService, SocialUser } from 'angularx-social-login';
import { FacebookLoginProvider, GoogleLoginProvider } from 'angularx-social-login';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FBService {

constructor(private auth: AuthService) { }


private user: SocialUser;
private loggedIn: boolean;

signInWithFB(): void {
  // this.auth.signIn(FacebookLoginProvider.PROVIDER_ID);
}

signOut(): void {
  // this.auth.signOut();
}

getFbStatus() {
  // return this.auth.authState;
}

}
