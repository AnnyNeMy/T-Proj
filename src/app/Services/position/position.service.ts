import { Injectable } from '@angular/core';
import { HTTPServiceService } from '../httpservice.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PositionService {

  constructor(private httpService:  HTTPServiceService) { }
  
    getPositionDeals(isin: string): Observable<any> {
      return this.httpService.getPositionDeals(isin);
    }
  
    getFavoritePositionReport(): Observable<any> {
      return this.httpService.getFavoritePositionReport();
    }

    getPositionReport(): Observable<any> {
      return this.httpService.getPositionReport();
    }
}
