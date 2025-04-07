import { Injectable } from '@angular/core';
import { HTTPServiceService } from '../httpservice.service';
import { Observable } from 'rxjs';
import { BondsReportRequest } from '../../Models/BondsReportRequest';

@Injectable({
  providedIn: 'root'
})
export class BondService {

  constructor(private httpService: HTTPServiceService) { }
  
  getBondsReport(req: BondsReportRequest): Observable<any> {
      return this.httpService.getBonds(req);
  } 

}
