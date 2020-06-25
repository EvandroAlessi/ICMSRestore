import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';
import { InvoiceService } from '../../../services/invoice.service';
import { DetailsComponent } from '../details/details.component';
@Component({
  selector: 'app-list',
  styleUrls: ['./list.component.scss'],
  templateUrl: './list.component.html',
})
export class ListComponent implements OnInit {
  public route: string = '/invoices';
  public showFilter: boolean = false;
  public filters: any = {
    cnpj: '',
    page: 1,
    take: 5
  };
  public pagination: any = {};
  public invoices: any = [];

  selectedInvoice: any = {};
  bsModalRef: BsModalRef;

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
    });
  }

  details(id){
    const initialState = {
      id: id
    };

    this.bsModalRef = this.modalService.show(DetailsComponent, {initialState});
  }

  toggleFilter() {
    this.showFilter = !this.showFilter;
  }

  changePageSize(size) {
    this.filters.take = size;
    this.filters.page = 1;
    
    this.paginate(this.filters.page);
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
