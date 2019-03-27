import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { BsDropdownModule, PaginationModule, ProgressbarModule, AccordionModule, BsDatepickerModule } from 'ngx-bootstrap';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { KidListComponent } from './kids/kid-list/kid-list.component';
import { KidCardComponent } from './kids/kid-card/kid-card.component';
import { KidDetailComponent } from './kids/kid-detail/kid-detail.component';
import { UserListComponent } from './users/user-list/user-list.component';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';

import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { KidService } from './_services/kid.service';
import { MemoryService } from './_services/memory.service';
import { UserService } from './_services/user.service';

import { KidListResolver } from './_resolvers/kid-list.resolver';
import { KidDetailResolver } from './_resolvers/kid-detail.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { UserEditResolver } from './_resolvers/user-edit.resolver';

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
    KidCardComponent,
    KidDetailComponent,
    UserListComponent,
    UserEditComponent,
    AdminPanelComponent,
    HasRoleDirective,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    ProgressbarModule.forRoot(),
    AccordionModule.forRoot(),
    BsDatepickerModule.forRoot(),
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
    AlertifyService,
    KidService,
    MemoryService,
    UserService,
    KidListResolver,
    KidDetailResolver,
    UserListResolver,
    UserEditResolver,
  ],
  bootstrap: [
    AppComponent,
  ]
})
export class AppModule { }
