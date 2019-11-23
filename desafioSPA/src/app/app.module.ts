import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Home/Home.component';
import { AccessComponent } from './Access/Access.component';
import { FormsModule } from '@angular/forms';
import { AuthService } from './_services/Auth/Auth.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { GuardService } from './_services/Guard/Guard.service';
import { JwtHelperService, JwtModuleOptions, JWT_OPTIONS } from '@auth0/angular-jwt';
import { ClientService } from './_services/Client/Client.service';
import { HttpInterceptorService } from './_services/HttpInterceptor/HttpInterceptor.service';
import { PathLocationStrategy, LocationStrategy } from '@angular/common';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { SocialLoginModule, AuthServiceConfig } from "angularx-social-login";
import { GoogleLoginProvider, FacebookLoginProvider } from "angularx-social-login";
import { FBService } from './_services/FB/FB.service';

const config = new AuthServiceConfig([
   {
      id: FacebookLoginProvider.PROVIDER_ID,
      provider: new FacebookLoginProvider('2411188055863087')
   }
]);

export function provideConfig() {
   return config;
}

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      AccessComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      NgxMaskModule.forRoot({
         validation: true,
      }),
      SocialLoginModule
   ],
   providers: [
      AuthService,
      GuardService,
      { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
      JwtHelperService,
      ClientService,
      Location,
      { provide: LocationStrategy, useClass: PathLocationStrategy },
      { provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true },
      {
         provide: AuthServiceConfig,
         useFactory: provideConfig
      },
      FBService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
