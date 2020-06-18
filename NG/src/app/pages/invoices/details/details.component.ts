import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { InvoiceService } from '../../../services/invoice.service';

@Component({
  selector: 'app-edit',
  styleUrls: ['./details.component.scss'],
  templateUrl: './details.component.html',
})
export class DetailsComponent implements OnInit {
  public route: string = '/invoices';
  public errors: any = {};
  public invoice: any = {};
  public id: number;

  constructor(
    private routerActived: ActivatedRoute,
    private router: Router,
    private toast: ToastrService,
    private invoiceService: InvoiceService
  ) {}

  ngOnInit() {
    this.routerActived.params.subscribe((params) => {
      this.id = params.id;
      this.invoiceService
          .get(this.id)
          .subscribe((response) => {
              this.invoice = response;
            },
            (err) => {
              this.router.navigate([this.route]);
            },
          );
    });
  }
}
