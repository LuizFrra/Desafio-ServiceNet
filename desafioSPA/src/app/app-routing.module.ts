import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './Home/Home.component';
import { AccessComponent } from './Access/Access.component';


const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'acesso', component: AccessComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
