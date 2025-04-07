import { Injectable } from '@angular/core';
import { HTTPServiceService } from '../httpservice.service';
import { catchError, map, Observable, of } from 'rxjs';
import { CommonResponse } from '../../Models/CommonResponse';
import { IAuthResponse } from '../../Models/IAuthResponse';
import { ECommonStatus } from '../../Models/ECommonStatus';
import { UserNameChanged } from '../../Models/CommonVariables';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  constructor(private httpServ: HTTPServiceService) { 
  }

  login(login: string, password: string): Observable<CommonResponse<IAuthResponse>> {
    return this.httpServ.login(login, password).pipe(map(x => {
      if ( x.Status == ECommonStatus.Ok) {
        UserNameChanged.next(login);
        this.saveToken(x.Data.accessToken);
        this.saveUserNameInLocalSorage(login)
      }
      return {
        Status: x.Status, 
        Message: x.Message,
        Data: x.Data as IAuthResponse,
      } as CommonResponse<IAuthResponse>; 
    }))
  }

  register(login: string, password: string): Observable<CommonResponse<IAuthResponse>> {
    return this.httpServ.register(login, password).pipe(map (x => {
      if ( x.Status == ECommonStatus.Ok) {
        UserNameChanged.next(login);
        this.saveToken(x.Data.accessToken);
        this.saveUserNameInLocalSorage(login);
      }
      return {
        Status: x.Status, 
        Message: x.Message,
        Data: x.Data as IAuthResponse,
      } as CommonResponse<IAuthResponse>; 
    }))
  }
  logout() {
    sessionStorage.removeItem('accessToken');
    localStorage.removeItem('userName');
    this.deleteCookie('refreshToken');
  //  window.location.href = '/login';
  }

  saveToken(token: string): void {
    sessionStorage.setItem('accessToken', token);
  }

  saveUserNameInLocalSorage(userName: string): void {
    localStorage.setItem('userName', userName);
  }

  getToken(): string | null {
    return sessionStorage.getItem('accessToken');
  }

  getCookie(name: string): string | null {
    const nameEq = name + "=";
    const ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
      let c = ca[i].trim();
      if (c.indexOf(nameEq) === 0) return c.substring(nameEq.length, c.length);
    }
    return null;
  }
  deleteCookie(name: string) {
    document.cookie = `${name}=;expires=Thu, 01 Jan 1970 00:00:00 GMT;path=/`;
  }

  refreshToken(): Observable<string | null> {
    return this.httpServ.refreshToken().pipe(map(resp => {
      if (resp.Status == ECommonStatus.Ok) {
          sessionStorage.setItem('accessToken', resp.Data?.accessToken);
      return resp.Data?.accessToken ?? null; 
      } else {
        return null
      }
      }),
      catchError(error => {
        this.logout(); 
        return of(null)
      })
    );
  }

}
