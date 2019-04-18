import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { BsDropdownModule, PaginationModule, ProgressbarModule, AccordionModule, BsDatepickerModule, ModalModule } from 'ngx-bootstrap';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { KidListComponent } from './kids/kid-list/kid-list.component';
import { KidCardComponent } from './kids/kid-card/kid-card.component';
import { KidDetailComponent } from './kids/kid-detail/kid-detail.component';
import { KidEditComponent } from './kids/kid-edit/kid-edit.component';
import { KidSearchComponent } from './kids/kid-search/kid-search.component';
import { UserListComponent } from './users/user-list/user-list.component';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { UserKidListComponent } from './users/user-kid-list/user-kid-list.component';
import { UserPasswordResetComponent } from './users/user-password-reset/user-password-reset.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';

import { HasRoleDirective } from './_directives/has-role.directive';

import { ErrorInterceptorProvider } from './_services/error.interceptor';

import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { KidService } from './_services/kid.service';
import { MemoryService } from './_services/memory.service';
import { UserService } from './_services/user.service';
import { OrganizationService } from './_services/organization.service';

import { KidListResolver } from './_resolvers/kid-list.resolver';
import { KidDetailResolver } from './_resolvers/kid-detail.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { UserProfileResolver } from './_resolvers/user-profile.resolver';
import { OrganizationListResolver } from './_resolvers/organization-list.resolver';

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
    KidEditComponent,
    KidSearchComponent,
    UserListComponent,
    UserEditComponent,
    UserKidListComponent,
    UserPasswordResetComponent,
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
    ModalModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    }),
  ],
  providers: [
    ErrorInterceptorProvider,
    AuthService,
    AlertifyService,
    KidService,
    MemoryService,
    UserService,
    OrganizationService,
    KidListResolver,
    KidDetailResolver,
    UserListResolver,
    UserEditResolver,
    UserProfileResolver,
    OrganizationListResolver,
  ],
  entryComponents: [
    KidEditComponent,
    KidSearchComponent,
    UserPasswordResetComponent,
  ],
  bootstrap: [
    AppComponent,
  ]
})
export class AppModule { }
