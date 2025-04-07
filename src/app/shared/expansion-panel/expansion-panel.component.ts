import { ChangeDetectionStrategy, Component, Input, signal } from '@angular/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { TableComponent } from '../table/table.component';

@Component({
  selector: 'app-expansion-panel',
  imports: [MatExpansionModule, TableComponent],
  templateUrl: './expansion-panel.component.html',
  styleUrl: './expansion-panel.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ExpansionPanelComponent {
  readonly panelOpenState = signal(false);
   @Input() inputDataTabel: any[] = [];
   @Input() title: string = "";

   ngOnInit() {

  }
}
