import { CommonModule } from '@angular/common';
import { Component, Input, SimpleChanges, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { provideAnimations } from '@angular/platform-browser/animations';
import { Subscription } from 'rxjs';
import { favoriteTableChanged } from '../../Components/FavouriteBond/favourite-bond/favourite-bond.component';

@Component({
  selector: 'app-table',
  standalone: true,
  providers: [provideAnimations()],
  imports: [CommonModule, MatTableModule, MatPaginatorModule, MatButtonModule, MatSelectModule, MatFormFieldModule, MatInputModule, MatSortModule ],
  templateUrl: './table.component.html',
  styleUrl: './table.component.scss'
})
export class TableComponent  {
 // @Input() dataSource: any[] = []; 
  @Input() inputDataSource: any[] = [];
  dataSource = new MatTableDataSource<any>();
  displayedColumns: string[] = [];
  pageSizeOptions: number[] = this.getPageSizeOptions();
  // Объект для хранения суммы по столбцам (ключ - имя столбца)
  columnSums: { [key: string]: number } = {};

  @ViewChild(MatPaginator) paginator!: MatPaginator; 
  @ViewChild(MatSort) sort!: MatSort;

  constructor() {}

 ngOnChanges(changes: SimpleChanges) {
    if (changes['inputDataSource'] && this.inputDataSource.length > 0) {
      const preparedData = this.prepareData(this.inputDataSource);
      this.dataSource.data = [...preparedData]; 
      this.displayedColumns = Object.keys(preparedData[0]);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort; 
      // Расчет сумм для столбцов, где данные можно привести к числу
      this.calculateSums(preparedData);
    }
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.pageSizeOptions = this.getPageSizeOptions();
  }

  calculateSums(data: any[]): void {
    this.columnSums = {};
    // Идем по всем столбцам
    this.displayedColumns.forEach(column => {
      let sum = 0;
      let hasNumeric = false;
      data.forEach(row => {

        const regex = /^-?\d+(\.\d+)?$/;
        if (regex.test(row[column])) {
          const parsedValue = parseFloat(row[column]);
          if (!isNaN(parsedValue)) {
            sum += parsedValue;
            hasNumeric = true;
          }
        }
      });
      if (hasNumeric) {
        this.columnSums[column] = sum;
      }
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    this.calculateSums(this.dataSource.filteredData);
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  prepareData(data: any[]): any[] {
    return data.map(row => {
      const newRow = { ...row };
      this.displayedColumns.forEach(column => {
        const regex = /^-?\d+(\.\d+)?$/;
        if (regex.test(row[column])) {
          const parsedValue = parseFloat(row[column]);
          if (!isNaN(parsedValue)) {
            newRow[column] = parsedValue;
          }
        }
      });
      return newRow;
    });
  }

  getPageSizeOptions(): number[] {
    var lenght = this.dataSource?.data?.length;
    if (lenght > 0) {
      if (lenght < 20) {
        return [lenght];
      } else if (lenght < 50) {
        return [20, lenght];
      } else if (lenght < 75) {
        return [20, 50, lenght];
      } else if (lenght < 100){
        return [20, 50, 75, lenght];
      } else {
        return [20, 50, 75, 100, lenght];
      }
    } else {
      return [20, 50, 100];
    }
  }
}
