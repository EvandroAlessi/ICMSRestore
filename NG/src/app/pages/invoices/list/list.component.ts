import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';
import { InvoiceService } from '../../../services/invoice.service';
@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
})
export class ListComponent implements OnInit {
  public route: string = '/invoices';
  public showFilter: boolean = false;
  public filters: any = {
    cnpj: '',
    nome: '',
    cidade: '',
    uf: '',
    page: 1,
    take: 8
  };
  public pagination: any = {};
  public invoices: any = [];

  constructor(
    private routerActived: ActivatedRoute,
    private router: Router,
    private modalService: BsModalService,
    private toast: ToastrService,
    private invoiceService: InvoiceService
  ) {  }

  ngOnInit() {
    this.routerActived.queryParams.subscribe((params) => {
      for (const key in params) {
        if (this.filters.hasOwnProperty(key)) {
          this.filters[key] = params[key];
        }
      }

      this.list();
    });
  }

  list() {
    this.invoiceService.getAllSimplify(this.filters).subscribe((response) => {
      this.invoices = response.invoices;
      this.pagination = response.pagination;

      console.log(response);
    });
  }

  details(invoice){
    // this.modalService.show();
  }

  toggleFilter() {
    this.showFilter = !this.showFilter;
  }

  filter() {
    this.paginate(1);
  }

  unfilter() {
    this.filters.content = [];
    this.toggleFilter();
    this.paginate(1);
  }

  paginate(page: number) {
    const params = [];
    this.filters.page = page;

    for (const key in this.filters) {
      if (this.filters[key]) {
        params[key] = this.filters[key];
      }
    }
    
    this.router.navigate([this.route], { queryParams: params });
  }
}
