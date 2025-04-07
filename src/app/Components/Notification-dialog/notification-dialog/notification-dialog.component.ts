import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';

@Component({
  selector: 'app-notification-dialog',
  imports: [MatDialogTitle,
    MatDialogContent,],
  templateUrl: './notification-dialog.component.html',
  styleUrl: './notification-dialog.component.scss'
})
export class NotificationDialogComponent {

  constructor(public dialogRef: MatDialogRef<NotificationDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: { dialogTitle: string; dialogMsg: string }) {
    setTimeout(() => {
      this.dialogRef.close();
    }, 3000);
  }

  closeDialog() {
    this.dialogRef.close();
  }
}
