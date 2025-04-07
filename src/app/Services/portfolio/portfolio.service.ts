import { Injectable } from '@angular/core';
import { HTTPServiceService } from '../httpservice.service';
import { filter, map, Observable } from 'rxjs';
import { FavouriteBond } from '../../Models/FavouriteBond';
import { CommonResponse } from '../../Models/CommonResponse';

@Injectable({
  providedIn: 'root'
})
export class PortfolioService {

  constructor(private httpService:  HTTPServiceService) { }

  getPortfolioReport(): Observable<any> {
    return this.httpService.getPortfolioReport();
  } 

  getFavouriteBond(): Observable<FavouriteBond[]> {
    return this.httpService.getFavouriteBonds().pipe(
      map((data: FavouriteBond[]) => {
      return data.map(item => {
        console.log(item);
        let bond = new  FavouriteBond();
        bond.Figi = item.Figi;
        bond.Isin = item.Isin;
        bond.Name = item.Name;
        console.log(bond);
        return bond;
      })
      })
    )
  }

  addFavouriteBond(newBondIsin: string): Observable<CommonResponse<any>> {
    return this.httpService.addFavouriteBond(newBondIsin).pipe(map((data) => {
      return {
        Status: data.Status, 
        Message: data.Message,
        Data: data.Data,
      } as CommonResponse<any>;
    }));
  }

  delFavouriteBond(bondIsin: string): Observable<number> {
    return this.httpService.delFavouriteBond(bondIsin);
  }

  delAllFavouriteBonds(): Observable<number> {
    return this.httpService.delFavouriteBonds();
  }

  
}
