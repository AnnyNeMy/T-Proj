import { Component } from '@angular/core';
import { MoneyService } from '../../../Services/money/money.service';
import { PortfolioService } from '../../../Services/portfolio/portfolio.service';
import { RefreshService } from '../../../Services/refresh/refresh.service';
import { NotificationService } from '../../../Services/notification/notification.service';
import { HTTPServiceService } from '../../../Services/httpservice.service';
import { CouponeReportService } from '../../../Services/couponeReport/coupone-report.service';
import { Subscription } from 'rxjs';
import { favoriteTableChanged, FavouriteBondComponent } from '../../FavouriteBond/favourite-bond/favourite-bond.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatAccordion } from '@angular/material/expansion';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { BondQuestionnaireComponent } from '../../BondQuestionnaire/bond-questionnaire/bond-questionnaire.component';
import { PositionDealsComponent } from '../../Position-deals/position-deals.component';
import { ExpansionPanelComponent } from '../../../shared/expansion-panel/expansion-panel.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [MatToolbarModule, MatTableModule, MatCardModule, MatFormFieldModule, MatSelectModule, MatAccordion, MatProgressSpinner,
    FavouriteBondComponent, BondQuestionnaireComponent, PositionDealsComponent, ExpansionPanelComponent, MatTooltipModule,
    MatButtonModule, CommonModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    FormsModule,
    MatIconModule,
    MatDialogModule,
    ReactiveFormsModule, MatDatepickerModule, MatNativeDateModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})


export class HomeComponent {

  private tableChangedSub!: Subscription;

   constructor(private moneyServ: MoneyService, private portfolioServ: PortfolioService, private  RefreshServ:  RefreshService, 
      private  CouponReportServ: CouponeReportService, private  https: HTTPServiceService, private notificationService: NotificationService) {}
    positions: any[] = []; 
    MoneyReportData: any[] = []; 
    PortfolioReportData: any[] = []; 
    CouponReportData: any[] = []; 
    FavouriteBondsData: any[] = []; 
    BondsData: any[] = []; 
    ObservedPricesData: any[] = [];
  
    isMoneyReportLoading: boolean = false;
    isPortfolioReportLoading: boolean = false;
    isFavouriteBondsLoading: boolean = false;
    isCouponReportLoading: boolean = false;
    isUpdateLoading: boolean = false;
    isBondsLoading: boolean = false;
    isObservedPricesLoading: boolean = false;
    revreshTooltip: string = ""

    ngOnInit() {
        this.getFavouriteBonds();
        this.tableChangedSub = favoriteTableChanged.subscribe(x => {
          if(x) {
            this.getFavouriteBonds();
          }
        })
      }
    
      getMoneyReport(): void {
        this.isMoneyReportLoading = true;
        this.moneyServ.getMoneyReport().subscribe({
          next: (data) => {
            this.MoneyReportData = data;
            this.isMoneyReportLoading = false;
            this.notificationService.showNotificationDialog("Успешно", "Данные по ДВИЖЕНИЮ СРЕДСТВ средств (getMoneyReport) загружены");
          },
          error: (err) => {
            this.isMoneyReportLoading = false;
            console.error('Ошибка при получении MoneyReportData:', err)
            this.notificationService.showNotificationDialog("Error", `Ошибка при получении данных по движению средств (getMoneyReport). ${err}` );
          } 
        });
      }
    
      getCouponReport(): void {
        this.isCouponReportLoading = true;
        this.CouponReportServ.getCouponeReport().subscribe({
          next: (data) => {
            this.CouponReportData = data;
            this.isCouponReportLoading = false;
            this.notificationService.showNotificationDialog("Успешно", "Данные по КУПОНАМ (getCouponReport) загружены");
          },
          error: (err) => {
            this.isCouponReportLoading = false;
            console.error('Ошибка при получении getCouponeReport:', err)
            this.notificationService.showNotificationDialog("Error", `Ошибка при получении данных по КУПОНАМ (getCouponReport). ${err}` );
          }
        });
      }
    
      getBonds(): void {
    
        this.isBondsLoading = true;
       /* let req = new BondsReportRequest();
        this.BondServ.getBondsReport(req).subscribe({
          next: (data) => {
            this.BondsData = data;
            this.isBondsLoading = false;
          },
          error: (err) => {
            this.isBondsLoading = false;
            console.error('Ошибка при получении getCouponeReport:', err)
          }
        });*/
      }
    
      getPortfolioReport(): void {
        this.isPortfolioReportLoading = true;
        this.portfolioServ.getPortfolioReport().subscribe({
          next: (data) => {
            this.PortfolioReportData = data;
            this.isPortfolioReportLoading = false;
            console.log('getPortfolioReport Данные загружены:', data);
            this.notificationService.showNotificationDialog("Успешно", "ТЕКУЩИЕ ПОЗИЦИИ (getPortfolioReport) загружены");
          },
          error: (err) => {
            this.isPortfolioReportLoading = false;
            console.error('Ошибка при получении PortfolioReportData:', err)
            this.notificationService.showNotificationDialog("Error", `Ошибка при получении Текущих позиций (getPortfolioReport). ${err}` );
          }
        });
      }
    
      getFavouriteBonds(): void {
        this.isFavouriteBondsLoading = true;
        this.portfolioServ.getFavouriteBond().subscribe({
          next: (data) => {
            this.FavouriteBondsData = [...data];
            this.isFavouriteBondsLoading = false;
            console.log('getFavouriteBonds Данные загружены:', data);
            this.notificationService.showNotificationDialog("Успешно", "Мои бонды (getFavouriteBonds) загружены");
          },
          error: (err) => {
            this.isFavouriteBondsLoading = false;
            console.error('Ошибка при получении FavouriteBondsData:', err);
            this.notificationService.showNotificationDialog("Error", `Ошибка при получении моих бондов  (getFavouriteBonds). ${err}` );
          }
        });
      }
    
      getObservedPrices() {
        this.isObservedPricesLoading = true;
        this.https.getObservedPrices().subscribe({
          next: (data) => {
            this.ObservedPricesData = data;
            this.isObservedPricesLoading = false;
            console.log('getObservedPrices Данные загружены:', data);
            this.notificationService.showNotificationDialog("Успешно", "Данные ПОД НАБЛЮДЕНИЕМ (getObservedPrices) загружены");
          },
          error: (err) => {
            this.isObservedPricesLoading = false;
            console.error('Ошибка при получении ObservedPricesData:', err);
            this.notificationService.showNotificationDialog("Error", `Ошибка при получении данных под наблюдением (getObservedPrices). ${err}` );
          }
        });
      }
    
      updateData() {
        this.isUpdateLoading = true;
        this.RefreshServ.refreshData().subscribe({
          next: (x) => {
            this.revreshTooltip = new Date().toDateString();
            this.isUpdateLoading = false
            this.notificationService.showNotificationDialog("Успешно", "ДАННЫЕ ОБНОВЛЕНЫ (updateData)");
          },
          error: (err) => {
            this.isUpdateLoading = false;
            console.error('Ошибка при обновлении updateDate:', err)
            this.notificationService.showNotificationDialog("Error", `Ошибка при обновлении данных (updateData). ${err}` );
          }
        });
      }
    
      
    
}
