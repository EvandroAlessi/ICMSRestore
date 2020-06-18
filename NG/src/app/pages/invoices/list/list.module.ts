import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { FormsModule } from '@angular/forms';
import { ListComponent } from './list.component';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';
import { SharedModule } from '../../shared/shared.module';
import { ListRouting } from './list.routing';

@NgModule({
  declarations: [ListComponent],
  imports: [
    CommonModule,
    RouterModule,
    ListRouting,
    SharedModule,
    FormsModule,
    CollapseModule.forRoot(),
  ],
  entryComponents: [ConfirmDialogComponent],
})
export class ListModule {}
