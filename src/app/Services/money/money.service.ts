import { Injectable } from '@angular/core';
import { HTTPServiceService } from '../httpservice.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MoneyService {

  constructor(private httpService: HTTPServiceService) { }

  getMoneyReport(): Observable<any> {
    const money = this.httpService.getMoneyReport(); 
    return money;
  }
}
