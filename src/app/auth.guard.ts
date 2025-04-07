import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  console.log("AuthGuard вызывается..."); 
  const token = sessionStorage.getItem('accessToken'); 

  const router = inject(Router); 
  
  if (token) {
    return true; 
  } else {
    sessionStorage.setItem('redirectUrl', state.url);
    router.navigate(['/login']); 
    return false;
  }
};
