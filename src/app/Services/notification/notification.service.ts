import { inject, Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NotificationDialogComponent } from '../../Components/Notification-dialog/notification-dialog/notification-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private dialog = inject(MatDialog);
  
  constructor() { }

  showNotificationDialog(dialogTitle: string, dialogMsg: string ) {
    this.dialog.open(NotificationDialogComponent, {
      width: 'auto',
    height: 'auto',
    data: { dialogTitle, dialogMsg },
    })
  }
}
