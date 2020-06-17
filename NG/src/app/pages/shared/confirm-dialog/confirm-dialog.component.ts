import { Component, OnInit, Input } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
})
export class ConfirmDialogComponent implements OnInit {
  public onClose: Subject<boolean>;
  @Input() message: string = 'Deseja realmente confirmar essa ação?';
  @Input() title: string = 'Confirmar';
  @Input() confirm: string = 'danger';
  @Input() cancel: string = 'primary';

  constructor(private modal: BsModalRef) {}

  public ngOnInit() {
    this.onClose = new Subject();
  }

  public onConfirm() {
    this.onClose.next(true);
    this.modal.hide();
  }

  public onCancel() {
    this.onClose.next(false);
    this.modal.hide();
  }
}
