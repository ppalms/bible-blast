import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { KidListComponent } from './kids/kid-list/kid-list.component';
import { AuthGuard } from './_guards/auth-guard';
import { KidListResolver } from './_resolvers/kid-list-resolver';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { KidDetailComponent } from './kids/kid-detail/kid-detail.component';
import { KidDetailResolver } from './_resolvers/kid-detail-resolver';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'kids', component: KidListComponent, resolve: { kids: KidListResolver } },
      { path: 'kids/:id', component: KidDetailComponent, resolve: { kid: KidDetailResolver } },
      { path: 'admin', component: AdminPanelComponent, data: { roles: ['Admin', 'Coach'] } },
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
