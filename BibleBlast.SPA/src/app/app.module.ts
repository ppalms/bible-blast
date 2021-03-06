import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CollapseModule } from 'ngx-bootstrap/collapse';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { KidListComponent } from './kids/kid-list/kid-list.component';
import { KidCardComponent } from './kids/kid-card/kid-card.component';
import { KidDetailComponent } from './kids/kid-detail/kid-detail.component';
import { KidMemoryCategoryComponent } from './kids/kid-detail/kid-memory-category/kid-memory-category.component';
import { KidMemoryItemComponent } from './kids/kid-detail/kid-memory-category/kid-memory-item/kid-memory-item.component';
import { KidEditComponent } from './kids/kid-edit/kid-edit.component';
import { KidSearchComponent } from './kids/kid-search/kid-search.component';
import { UserListComponent } from './users/user-list/user-list.component';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { UserKidListComponent } from './users/user-kid-list/user-kid-list.component';
import { UserPasswordResetComponent } from './users/user-password-reset/user-password-reset.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CompletedMemoriesComponent } from './dashboard/completed-memories/completed-memories.component';
import { AwardsComponent } from './awards/awards.component';
import { AwardListComponent } from './awards/award-list/award-list.component';
import { AwardEditComponent } from './awards/award-edit/award-edit.component';

import { HasRoleDirective } from './_directives/has-role.directive';

import { ErrorInterceptorProvider } from './_services/error.interceptor';

import { KidListResolver } from './_resolvers/kid-list.resolver';
import { KidDetailResolver } from './_resolvers/kid-detail.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { UserProfileResolver } from './_resolvers/user-profile.resolver';
import { OrganizationListResolver } from './_resolvers/organization-list.resolver';
import { MemoryCategoryResolver } from './_resolvers/memory-category.resolver';
import { AwardListResolver } from './_resolvers/award-list.resolver';
import { AwardEditResolver } from './_resolvers/award-edit.resolver';
import { AwardListEarnedComponent } from './awards/award-list-earned/award-list-earned.component';

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
    KidMemoryCategoryComponent,
    KidMemoryItemComponent,
    KidEditComponent,
    KidSearchComponent,
    UserListComponent,
    UserEditComponent,
    UserKidListComponent,
    UserPasswordResetComponent,
    AdminPanelComponent,
    HasRoleDirective,
    DashboardComponent,
    CompletedMemoriesComponent,
    AwardsComponent,
    AwardListComponent,
    AwardEditComponent,
    AwardListEarnedComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
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
    CollapseModule.forRoot(),
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
    KidListResolver,
    KidDetailResolver,
    UserListResolver,
    UserEditResolver,
    UserProfileResolver,
    OrganizationListResolver,
    MemoryCategoryResolver,
    AwardListResolver,
    AwardEditResolver,
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
