import { NgIf } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ExpansionPanelComponent } from '../../../shared/expansion-panel/expansion-panel.component';
import { BondService } from '../../../Services/bond/bond.service';
import { BondsReportRequest } from '../../../Models/BondsReportRequest';
import { MatCardModule } from '@angular/material/card';
import { NotificationService } from '../../../Services/notification/notification.service';

@Component({
  selector: 'app-bond-questionnaire',
  imports: [MatButtonModule, MatTableModule, MatFormFieldModule, MatInputModule, FormsModule, MatIconModule, MatDialogModule, MatTooltipModule, 
    MatProgressSpinnerModule, NgIf, ReactiveFormsModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, ExpansionPanelComponent, MatCardModule ],
  templateUrl: './bond-questionnaire.component.html',
  styleUrl: './bond-questionnaire.component.scss',
  changeDetection: ChangeDetectionStrategy.Default,
})

export class BondQuestionnaireComponent {

  BondQuestionnaireForm!: FormGroup; 
  BondData: any[] = []; 
  isGetBondsLoading: boolean = false;
  errorText: string = "";
  minDate:Date = new Date();

  constructor(private fb: FormBuilder, private cdRef: ChangeDetectorRef, private bondService: BondService, private notificationService: NotificationService) {
  }

  ngOnInit(): void {
      this.BondQuestionnaireForm = this.fb.group({
      CloseDate: [new Date(), Validators.required],
      Profit: ['', [Validators.required, Validators.pattern(/^-?\d+$/)]], 
      Currency: ['RUB', Validators.required],
      LagDay: ['', [Validators.required, Validators.pattern(/^-?\d+$/)]],
    });
    this.BondQuestionnaireForm.valueChanges.subscribe(() => {
      this.cdRef.markForCheck(); // Обновляем шаблон вручную
    });
    console.log(this.BondQuestionnaireForm)
  }
  
  onSubmit() {
    this.isGetBondsLoading = true;
    if (this.BondQuestionnaireForm.valid) {
      let t = this.BondQuestionnaireForm.value.Currency;
      let bindReq = new BondsReportRequest(this.BondQuestionnaireForm.value.Currency, this.BondQuestionnaireForm.value.CloseDate, this.BondQuestionnaireForm.value.Profit,
      this.BondQuestionnaireForm.value.LagDay);
      this.bondService.getBondsReport(bindReq).subscribe({
        next: (data) => {
          this.BondData = data;
          this.isGetBondsLoading = false;
          this.notificationService.showNotificationDialog("Успешно", "Сводка бондов (getBondsReport) загружены");
        },
        error: (err) => {
          this.isGetBondsLoading = false;
          console.error('Ошибка при получении getBondsReport:', err)
          this.notificationService.showNotificationDialog("Error", `Ошибка при получении Сводки бондов (getBondsReport). ${err}` );
        } 
      });
    } else {
      this.isGetBondsLoading = false;
      console.log('Форма не полностью заполнена');
    }
  }

  validateNumber(event: KeyboardEvent) {
    const allowedKeys = ['Backspace', 'ArrowLeft', 'ArrowRight', 'Tab'];
    const regex = /^-?\d+$/;
    if (!regex.test(event.key) && !allowedKeys.includes(event.key)) {
      event.preventDefault();
    }
  }

  get isBtnDisabled() {
    return  !this.BondQuestionnaireForm || this.BondQuestionnaireForm?.invalid;
  }

  onFormChange() {
    this.BondQuestionnaireForm.updateValueAndValidity();
  }
}
