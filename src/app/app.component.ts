import {Component } from '@angular/core';
import { UserNameChanged } from './Models/CommonVariables';
import { AuthServiceService } from './Services/authService/auth-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-component',
  standalone: false,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})

export class AppComponent  {
  
  UserName: string = "Войти";

  constructor(private authServ: AuthServiceService, private router: Router) {
    
  }

  ngOnInit() {
    UserNameChanged.subscribe(name => {
      this.UserName = name;
    });
    this.checkSession();
  }

  logout() {
    this.authServ.logout();
  }

  checkSession() {
    this.authServ.refreshToken()
      .subscribe({
        next: (response) => {
          if (response != null) {
          sessionStorage.setItem('accessToken', response);
          this.router.navigate(['/home']);
          } else {
            this.router.navigate(['/login']);
          }
        },
        error: (err) => {
          sessionStorage.removeItem('accessToken');
          this.router.navigate(['/login']);
        }
      });
  }

}



