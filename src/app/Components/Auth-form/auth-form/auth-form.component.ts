import { ChangeDetectionStrategy, ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AuthServiceService } from '../../../Services/authService/auth-service.service';
import { CommonModule, NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { NotificationService } from '../../../Services/notification/notification.service';
import { UserNameChanged } from '../../../Models/CommonVariables';
import { ECommonStatus } from '../../../Models/ECommonStatus';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-form',
  imports: [ FormsModule, CommonModule,
    ReactiveFormsModule, MatButtonModule,  
    MatFormFieldModule,  
    MatInputModule, MatProgressSpinnerModule,
    MatTooltipModule],
  standalone: true,
  templateUrl: './auth-form.component.html',
  styleUrl: './auth-form.component.scss',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class AuthFormComponent {
  AuthForm!: FormGroup; 
  isLoading: boolean = false;
  LoginDescription = "Если вы уже зарегистрированы - вам нужно ";
  RegisterDescription = "Если у Вас нет учетной записи - надо ";
  LoginText = "ВОЙТИ";
  RegisterText = "ЗАРЕГИСТРИРОВАТЬСЯ"
  LoginRegisterText = this.RegisterText;

  constructor(private fb: FormBuilder, private cdRef: ChangeDetectorRef,  private authServ: AuthServiceService, private notificServ: NotificationService, private router: Router ) {
  }

   ngOnInit(): void {
      this.AuthForm  = this.fb.group({
        Login: [' ',  Validators.required],
        Pass: [' ', [Validators.required]],
      });

      this.AuthForm.valueChanges.subscribe(() => {
        this.cdRef.markForCheck(); // Обновляем шаблон вручную
      });
    }
  LoginRegisterClick() {
    this.LoginRegisterText = this.LoginRegisterText == this.LoginText ? this.LoginRegisterText = this.RegisterText : this.LoginText;

  }

  onSubmit() {
    this.isLoading = true;
    let login = this.AuthForm.value.Login.trim();
    let password = this.AuthForm.value.Pass.trim();
    if(this.LoginRegisterText != this.LoginText) {
      this.authServ.login(login, password).subscribe({
        next: (data) => {
          this.isLoading = false;
          if(data.Status == ECommonStatus.Ok) {
            this.notificServ.showNotificationDialog("Успешно", `${data.Message}`);
            const redirectUrl = sessionStorage.getItem('redirectUrl') || '/home';
            this.router.navigate([redirectUrl]);
          } else {
            this.notificServ.showNotificationDialog("Error", `${data.Message}`);
          }
         // data.Status == ECommonStatus.Ok ? this.notificServ.showNotificationDialog("Успешно", `${data.Message}`) : this.notificServ.showNotificationDialog("Error", `${data.Message}`);
        },
        error: (err) => {
          this.isLoading = false;
          console.error('Ошибка при получении authServ.login:', err)
          let error = !!err?.error?.Message ? err.error.Message : err.message;
          this.notificServ.showNotificationDialog("Error", `Ошибка при входе пользователя ${login}. ${error}` );
        }
        });
    } else {
      this.authServ.register(login, password).subscribe({
        next: (data) => { 
          this.isLoading = false;
          if(data.Status == ECommonStatus.Ok) {
            const redirectUrl = sessionStorage.getItem('redirectUrl') || '/home';
            this.router.navigate([redirectUrl]);
            this.notificServ.showNotificationDialog("Успешно", `${data.Message}`);
          } else {
            this.notificServ.showNotificationDialog("Error", `${data.Message}`);
          }
         // data.Status == ECommonStatus.Ok ? this.notificServ.showNotificationDialog("Успешно", `${data.Message}`) : this.notificServ.showNotificationDialog("Error", `${data.Message}`);
        },
        error: (err) => {
          this.isLoading = false;
          console.error('Ошибка при получении authServ.register:', err)
          let error = !!err?.error?.Message ? err.error.Message : err.message;
          this.notificServ.showNotificationDialog("Error", `Ошибка регистрации ${login}. ${error}` );
        }
    })
  }
  }
}
