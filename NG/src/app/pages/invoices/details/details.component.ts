import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { InvoiceService } from '../../../services/invoice.service';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-edit',
  styleUrls: ['./details.component.scss'],
  templateUrl: './details.component.html',
})
export class DetailsComponent implements OnInit {
  public route: string = '/invoices';
  public errors: any = {};
  invoice: any = {};
  public id: number;

  constructor(
    private routerActived: ActivatedRoute,
    private router: Router,
    private toast: ToastrService,
    private invoiceService: InvoiceService,
    public bsModalRef: BsModalRef
  ) {}

  ngOnInit() {
    this.invoiceService
        .get(this.id)
        .subscribe((response) => {
            this.invoice = response;
          },
          (err) => {
            this.router.navigate([this.route]);
          },
        );
  }
}
