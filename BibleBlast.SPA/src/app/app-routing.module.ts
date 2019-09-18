import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { KidListComponent } from './kids/kid-list/kid-list.component';
import { AuthGuard } from './_guards/auth-guard';
import { KidListResolver } from './_resolvers/kid-list.resolver';
import { KidDetailComponent } from './kids/kid-detail/kid-detail.component';
import { KidDetailResolver } from './_resolvers/kid-detail.resolver';
import { UserListComponent } from './users/user-list/user-list.component';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { AwardsComponent } from './awards/awards.component';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { OrganizationListResolver } from './_resolvers/organization-list.resolver';
import { UserProfileResolver } from './_resolvers/user-profile.resolver';
import { MemoryCategoryResolver } from './_resolvers/memory-category.resolver';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AwardListResolver } from './_resolvers/award-list.resolver';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'dashboard', component: DashboardComponent
      },
      {
        path: 'kids', component: KidListComponent,
        resolve: { kids: KidListResolver, memoryCategories: MemoryCategoryResolver }
      },
      {
        path: 'kids/:id', component: KidDetailComponent,
        resolve: { kid: KidDetailResolver, memoryCategories: MemoryCategoryResolver }
      },
      {
        path: 'awards', component: AwardsComponent,
        resolve: { awards: AwardListResolver }
      },
      {
        path: 'users', component: UserListComponent,
        resolve: { users: UserListResolver },
        data: { roles: ['Admin', 'Coach'] }
      },
      {
        path: 'users/new', component: UserEditComponent,
        resolve: { organizations: OrganizationListResolver },
        data: { roles: ['Admin', 'Coach'] }
      },
      {
        path: 'users/:id', component: UserEditComponent,
        resolve: { user: UserEditResolver, organizations: OrganizationListResolver },
        data: { roles: ['Admin', 'Coach'] }
      },
      {
        path: 'profile', component: UserEditComponent,
        resolve: { user: UserProfileResolver }
      },
      {
        path: 'admin', component: AdminPanelComponent,
        data: { roles: ['Admin'] }
      },
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
