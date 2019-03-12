import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { BsDropdownModule, PaginationModule } from 'ngx-bootstrap';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { KidListComponent } from './kids/kid-list/kid-list.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';

import { AuthService } from './_services/auth.service';
import { KidService } from './_services/kid.service';
import { KidListResolver } from './_resolvers/kid-list-resolver';

import { HasRoleDirective } from './_directives/has-role.directive';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    KidListComponent,
    AdminPanelComponent,
    HasRoleDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    }),
  ],
  providers: [
    AuthService,
    KidService,
    KidListResolver,
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
