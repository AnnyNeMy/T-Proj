import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, catchError, Observable, switchMap, throwError } from "rxjs";
import { AuthServiceService } from "./auth-service.service";


@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);
  
  constructor(private authService: AuthServiceService) {} 

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>  {
    const accessToken = sessionStorage.getItem('accessToken'); 

    if (accessToken ) {
      req = this.addToken(req, accessToken);
    }
    return next.handle(req).pipe(
      catchError(error => {
        // Если ошибка 401 - значит токен недействителен
        if (error instanceof HttpErrorResponse && error.status === 401) {
          return this.handle401Error(req, next);
        }
        return throwError(() => error);
      })
    );
  }

  private handle401Error(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      return this.authService.refreshToken().pipe(
        switchMap((newToken) => {
          this.isRefreshing = false;

          if(newToken != null) {
            sessionStorage.setItem('accessToken', newToken);
            this.refreshTokenSubject.next(newToken);
            return next.handle(req.clone({
              setHeaders: {
                Authorization: `Bearer ${newToken}`
              }
            }));
          }
          return throwError(() => new Error('Refresh token is missing or invalid'));
        }),
        catchError(err => {
          this.isRefreshing = false;
          this.authService.logout();
          return throwError(() => err);
        })
      );
      
    }
    return throwError(() => new Error('Unauthorized'));
  }

  private addToken(req: HttpRequest<any>, token: string): HttpRequest<any> {
    return req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
  }
}