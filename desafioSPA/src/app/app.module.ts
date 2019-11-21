import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Home/Home.component';
import { AccessComponent } from './Access/Access.component';
import { FormsModule } from '@angular/forms';
import { AuthService } from './_services/Auth/Auth.service';
import { HttpClientModule } from '@angular/common/http';
import { GuardService } from './_services/Guard/Guard.service';
import { JwtHelperService, JwtModuleOptions, JWT_OPTIONS } from '@auth0/angular-jwt';

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
      FormsModule
   ],
   providers: [
      AuthService,
      GuardService,
      { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
      JwtHelperService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
