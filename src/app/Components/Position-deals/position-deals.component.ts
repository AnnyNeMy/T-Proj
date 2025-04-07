import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ExpansionPanelComponent } from '../../shared/expansion-panel/expansion-panel.component';
import { PositionService } from '../../Services/position/position.service';
import { NotificationService } from '../../Services/notification/notification.service';

@Component({
  selector: 'app-position-deals',
  imports: [MatButtonModule, MatTableModule, MatFormFieldModule, MatInputModule, FormsModule, MatIconModule, MatTooltipModule, MatProgressSpinnerModule,
    MatCardModule, ExpansionPanelComponent, NgIf],
  templateUrl: './position-deals.component.html',
  styleUrl: './position-deals.component.scss'
})
export class PositionDealsComponent {

  constructor(private positionServ: PositionService, private notificationService: NotificationService) {}
  newIsin: string = ""; 

  PositionDealsData: any[] = []; 
  isPositionDealsLoading: boolean = false;

  PositionReportData: any[] = []; 
  isFavoritePositionReportLoading: boolean = false;

  FavoritePositionReport: any[] = []; 
  isPositionReportLoading: boolean = false;
  
  getPositionReport() {
    this.isPositionReportLoading = true;
    this.positionServ.getPositionReport().subscribe({
      next: (data) => {
        this.PositionReportData = data;
        this.isPositionReportLoading = false;
        console.log('getPositionReport Данные загружены:', data);
        this.notificationService.showNotificationDialog("Успешно", "Агрегированный отчет (getPositionReport) загружен");
      },
      error: (err) => {
        this.isPositionReportLoading = false;
        console.error('Ошибка при получении getPositionDeals:', err);
        this.notificationService.showNotificationDialog("Error", `Ошибка при получении Агрегированного отчета (getPositionReport). ${err}` );
      } 
    });
  }

  getPositionDeals(newIsin: string) {
    this.isPositionDealsLoading = true;
    this.positionServ.getPositionDeals(newIsin).subscribe({
      next: (data) => {
        this.PositionDealsData = data;
        this.isPositionDealsLoading = false;
        console.log('getPositionDeals Данные загружены:', data);
        this.notificationService.showNotificationDialog("Успешно", "Сделки для позиции (getPositionDeals) загружены");
      },
      error: (err) => {
        this.isPositionDealsLoading = false;
        console.error('Ошибка при получении getPositionDeals:', err);
        this.notificationService.showNotificationDialog("Error", `Ошибка при получении Сделок для позиций (getPositionDeals). ${err}` );
      } 
    });
  }
}
