import { NgModule } from '@angular/core';
import { CommonModule, NgIf } from '@angular/common';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import {  HTTP_INTERCEPTORS, provideHttpClient } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { TableComponent } from './shared/table/table.component';
import { MatFormField,  MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatAccordion } from '@angular/material/expansion';
import { ExpansionPanelComponent } from './shared/expansion-panel/expansion-panel.component';
import { MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { FavouriteBondComponent } from './Components/FavouriteBond/favourite-bond/favourite-bond.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { BondQuestionnaireComponent } from './Components/BondQuestionnaire/bond-questionnaire/bond-questionnaire.component';
import { PositionDealsComponent } from './Components/Position-deals/position-deals.component';
import { RouterModule } from '@angular/router';
import { routes } from './app.routes';
import { AuthFormComponent } from './Components/Auth-form/auth-form/auth-form.component';
import { HomeComponent } from "./Components/Home/home/home.component";
import { AuthInterceptor } from './Services/authService/auth.interceptor';



@NgModule({
  declarations: [AppComponent],
  imports: [
    CommonModule, BrowserModule,
    MatButtonModule,
    MatToolbarModule,
    MatCardModule,
    MatTableModule,
    MatPaginatorModule,
    TableComponent,
    MatFormFieldModule,
    MatSelectModule,
    BrowserAnimationsModule,
    MatAccordion,
    ExpansionPanelComponent,
    MatSortModule,
    MatInputModule,
    MatFormField,
    MatProgressSpinner,
    FavouriteBondComponent,
    FormsModule,
    MatIconModule,
    MatDialogModule,
    MatTooltipModule,
    ReactiveFormsModule, MatDatepickerModule, MatNativeDateModule, BondQuestionnaireComponent, PositionDealsComponent, RouterModule.forRoot(routes), 
    HomeComponent
],
  providers: [  provideHttpClient(), { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true } ],
  bootstrap: [AppComponent],
})
export class AppModule { }
