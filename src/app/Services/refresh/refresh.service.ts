import { Injectable } from '@angular/core';
import { HTTPServiceService } from '../httpservice.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RefreshService {

  constructor(private httpService: HTTPServiceService) { }

  refreshData(): Observable<any> {
    return this.httpService.refreshData();
  } 
  
}
