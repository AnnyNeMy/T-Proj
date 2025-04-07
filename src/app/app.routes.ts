import { Routes } from '@angular/router';
import { authGuard } from './auth.guard';
import { AuthFormComponent } from './Components/Auth-form/auth-form/auth-form.component';
import { HomeComponent } from './Components/Home/home/home.component';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent, canActivate: [authGuard] },
  { path: 'login', component: AuthFormComponent },
  { path: '**', redirectTo: '/login' }
];
