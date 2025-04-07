import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Routes } from './Routes';
import { FavouriteBond } from '../Models/FavouriteBond';
import { BondsReportRequest } from '../Models/BondsReportRequest';
import { CommonResponse } from '../Models/CommonResponse';


export interface User {
  id: number;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class HTTPServiceService {

  constructor(private http: HttpClient) { }
  

 getPositions(): Observable<any> {
  return this.http.get<any[]>(Routes.getFullUrl(Routes.POSITIONS), { withCredentials: true });
}

getPosition(id: number): Observable<any> {
  return this.http.get<any>(`${Routes.getFullUrl(Routes.POSITIONS)}/${id}`, { withCredentials: true });
}

addPosition(user: any): Observable<any> {
  return this.http.post<any>(Routes.getFullUrl(Routes.POSITIONS), user, { withCredentials: true });
}

updatePosition(id: number, position: any): Observable<void> {
  return this.http.put<void>(`${Routes.getFullUrl(Routes.POSITIONS)}/${id}`, position, { withCredentials: true });
}

deletePosition(id: number): Observable<void> {
  return this.http.delete<void>(`${Routes.getFullUrl(Routes.POSITIONS)}/${id}`, { withCredentials: true });
}

getMoneyReport(): Observable<any> {
  return this.http.get<any[]>(Routes.getFullUrl(Routes.MONEYREPORT), { withCredentials: true });
}

getPortfolioReport(): Observable<any> {
  return this.http.get<any[]>(Routes.getFullUrl(Routes.PORTFOLIOREPORT), { withCredentials: true });
}

getFavouriteBonds(): Observable<any> {
  return this.http.get<FavouriteBond[]>(Routes.getFullUrl(Routes.FAVOURITEBOND), { withCredentials: true });
}

addFavouriteBond(newBondIsin: string): Observable<CommonResponse<any>> {
  return this.http.post<CommonResponse<any>>(Routes.getFullUrl(Routes.FAVOURITEBOND), {Id: newBondIsin}, { withCredentials: true });
}

delFavouriteBond(bondIsin: string): Observable<number> {
  bondIsin.trim();
  return this.http.delete<number>(`${Routes.getFullUrl(Routes.FAVOURITEBOND)}/${bondIsin}`, { withCredentials: true });
}

delFavouriteBonds(): Observable<number> {
  return this.http.delete<number>(Routes.getFullUrl(Routes.FAVOURITEBOND), { withCredentials: true });
}

refreshData(): Observable<any> {
  return this.http.patch<any>(Routes.getFullUrl(Routes.REFRESHDATA), {}, { withCredentials: true });
}

getCouponeReport(): Observable<any> {
  return this.http.get<any>(Routes.getFullUrl(Routes.COUPONEREPORT), { withCredentials: true });
}

getBonds(req: BondsReportRequest): Observable<any> {
  return this.http.post<FavouriteBond[]>(Routes.getFullUrl(Routes.BONDS), req, { withCredentials: true });
}

getFavoritePositionReport(): Observable<any> {
  return this.http.get<any[]>(Routes.getFullUrl(Routes.POSITION_FAVORITEREPORT), { withCredentials: true });
}

getPositionReport(): Observable<any> {
  return this.http.get<any[]>(Routes.getFullUrl(Routes.POSITION_REPORT), { withCredentials: true });
}

getPositionDeals(isin: string): Observable<any> {
  return this.http.get<any[]>(`${Routes.getFullUrl(Routes.POSITION)}/${isin}`, { withCredentials: true });
}

getObservedPrices(): Observable<any> {
  return this.http.get<any[]>(Routes.getFullUrl(Routes.OBSERVEDPRICES), { withCredentials: true });
}

register(login: string, password: string): Observable<any> {
  return this.http.post(`${Routes.getFullUrl(Routes.AUTH_REGISTER)}`, { login, password }, { withCredentials: true });
}

login(login: string, password: string): Observable<any> {
  return this.http.post(`${Routes.getFullUrl(Routes.AUTH_LOGIN)}`, { login, password }, { withCredentials: true });
}

refreshToken(): Observable<CommonResponse<any>> {
  return this.http.post<CommonResponse<any>>(`${Routes.getFullUrl(Routes.AUTH_REVRESH_TOKEN)}`, {}, { withCredentials: true });
}


}
