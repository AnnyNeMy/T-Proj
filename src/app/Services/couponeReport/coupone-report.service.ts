import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HTTPServiceService } from '../httpservice.service';

@Injectable({
  providedIn: 'root'
})
export class CouponeReportService {

  constructor(private httpService: HTTPServiceService) { }

  getCouponeReport(): Observable<any> {
      return this.httpService.getCouponeReport();
  } 
}
