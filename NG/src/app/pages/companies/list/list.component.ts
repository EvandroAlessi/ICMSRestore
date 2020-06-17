import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';
import { CompanyService } from '../../../services/company.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
})
export class ListComponent implements OnInit {
  public route: string = '/companies';
  public showFilter: boolean = false;
  public filters: any = {
    content: '',
    page: 1,
  };
  public pagination: any = {};
  public companies: any = [];

  constructor(
    private routerActived: ActivatedRoute,
    private router: Router,
    private modalService: BsModalService,
    private toast: ToastrService,
    private companyService: CompanyService
  ) {}

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
    this.companyService.getAll(this.filters.page, 30).subscribe((response) => {
      this.companies = response.list;
      this.pagination = response.pagination;

      const end = this.pagination.current_page + 4 > this.pagination.last_page
          ? this.pagination.last_page
          : this.pagination.current_page + 4;

      const start = end - 9 < 1 
          ? 1 
          : end - 9;

      this.pagination.pages = [];

      for (let i = start; i <= end; i++) {
        this.pagination.pages.push(i);
      }
    });
  }

  delete(id) {
    this.modalService
      .show(ConfirmDialogComponent, {
        initialState: {
          message: 'Deseja realmente excluir o registro código ' + id + '?',
          note: 'Esta ação não poderá ser desfeita.',
        },
      })
      .content.onClose.subscribe((result) => {
        // if (result) {
        //   this.departmentService
        //     .delete(id)
        //     .then((response) => {
        //       this.toast.success(response.message, 'Sucesso!');
        //       this.list();
        //     })
        //     .catch((response) => {
        //       this.toast.error(response.error.message, 'Erro!');
        //     });
        // }
      });
  }

  toggleFilter() {
    this.showFilter = !this.showFilter;
  }

  filter() {
    this.paginate(1);
  }

  unfilter() {
    this.filters.content = '';
    this.toggleFilter();
    this.paginate(1);
  }

  paginate(page) {
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
