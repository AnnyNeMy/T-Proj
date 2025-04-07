import { ChangeDetectorRef, Component, inject, Input, signal, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { PortfolioService } from '../../../Services/portfolio/portfolio.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { DialogComponent } from '../../../shared/dialog/dialog.component';
import { FavouriteBond } from '../../../Models/FavouriteBond';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Subject } from 'rxjs';
import { MatProgressSpinner, MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgIf } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { NotificationService } from '../../../Services/notification/notification.service';
import { MatExpansionModule } from '@angular/material/expansion';
import { ECommonStatus } from '../../../Models/ECommonStatus';

export const favoriteTableChanged: Subject<boolean> = new Subject<boolean>();

@Component({
  selector: 'app-favourite-bond',
  imports: [MatButtonModule, MatTableModule, MatFormFieldModule, MatInputModule, FormsModule, MatIconModule, MatDialogModule, MatTooltipModule, MatProgressSpinnerModule, NgIf,
     MatProgressSpinner, MatCardModule, MatExpansionModule], 
  templateUrl: './favourite-bond.component.html',
  styleUrl: './favourite-bond.component.scss'
})

export class FavouriteBondComponent {

 constructor(private portfolioServ: PortfolioService, private cdr: ChangeDetectorRef, private notificationService: NotificationService) {}
 readonly panelOpenState = signal(false);
  @Input() inputFavouriteBondData: FavouriteBond[] = [];
  displayedColumns: string[] = ['Figi', 'Isin', 'Name', 'Actions'];
  dataToDisplay = this.inputFavouriteBondData;
  newIsin: string = "";
  readonly dialog = inject(MatDialog);
  isAddBondLoading: boolean = false;
  isDeleteAllLoading: boolean = false;
  deleteLoadingMap: Map<string, boolean> = new Map();

  ngOnChanges() {
   this.dataToDisplay = this.inputFavouriteBondData;
   this.isAddBondLoading = false;
   this.isDeleteAllLoading = false;
  }

  ngOnDestroy() {
  }

  addBond() {
    this.isAddBondLoading = true;
    this.portfolioServ.addFavouriteBond(this.newIsin).subscribe(res => {
      console.log("результат addFavouriteBond", res);
      if(res.Status == ECommonStatus.Ok) {
        this.newIsin = "";
        favoriteTableChanged.next(true);
        this.notificationService.showNotificationDialog("Успешно", res.Message);
      } else {
        this.isAddBondLoading = false;
        this.newIsin = "";
        this.notificationService.showNotificationDialog("Error", res.Message);
      }
    });
  }

  removeDataOpenDialog(enterAnimationDuration: string, exitAnimationDuration: string) {
    this.isDeleteAllLoading = true;
    const dialogRef = this.dialog.open(DialogComponent, {
      width: '250px',
      enterAnimationDuration,
      exitAnimationDuration,
      data: {DialogTitle: "Удалить данные таблицы", 
             DialogText: "Вы уверены, что хотите удалить все данные? Они будут безвозвратно утеряны."}
    });
    
    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.isDeleteAllLoading = true;
          this.portfolioServ.delAllFavouriteBonds().subscribe({
              next: (res) => {
              if (res != -1 && res != 0) {
                this.dataToDisplay = []
              }
              this.isDeleteAllLoading = false;
              this.cdr.markForCheck();
              this.notificationService.showNotificationDialog("Успешно", "Все данные успешно удалены из таблицы favirite");
              }, 
              error: (err) => {
                console.error('Ошибка при удалении:', err);
                this.isDeleteAllLoading = false;
                this.cdr.markForCheck();
                this.notificationService.showNotificationDialog("Error", `Ошибка при удалении всег данных removeDataOpenDialog. ${err}` );
               }
            }); 
      } else {
        this.isDeleteAllLoading = false;
        this.cdr.markForCheck();
      }
    });
  }  

  removeBond(delietedBond: FavouriteBond) {
    const key = delietedBond.Isin;
    this.deleteLoadingMap.set(key, true); // Устанавливаем флаг загрузки

    this.portfolioServ.delFavouriteBond(delietedBond.Isin).subscribe(res => {
      if(!!res && res != -1 && res != 0) {
      var newData = this.dataToDisplay.filter(b => b.Isin != delietedBond.Isin);
      this.dataToDisplay = [...newData];
      this.deleteLoadingMap.set(key, false);
      this.notificationService.showNotificationDialog("Успешно", "isin elietedBond.Isin данные успешно удален из таблицы favirite");
    }else {
      this.deleteLoadingMap.set(key, false);
    }
    })
   }   

   get isRemoveDataDisable(){
    return !this.dataToDisplay?.length;
   }
}
