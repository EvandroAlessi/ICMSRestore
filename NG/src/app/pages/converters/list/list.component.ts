import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';
import { ConverterService } from '../../../services/converter.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
})
export class ListComponent implements OnInit {
  public route: string = '/converters';
  public showFilter: boolean = false;
  public filters: any = {
    nome: '',
    page: 1,
    take: 5
  };

  public pagination: any = {};
  public converters: any = [];
  admin: boolean = false;

  constructor(
    private routerActived: ActivatedRoute,
    private router: Router,
    private modalService: BsModalService,
    private toast: ToastrService,
    private converterService: ConverterService
  ) {}

  ngOnInit() {
    this.admin = JSON.parse(localStorage.getItem('user')) 
                    && JSON.parse(localStorage.getItem('user')).user.cargo.toUpperCase() === 'ADMIN';

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
    this.converterService.getAll(this.filters).subscribe((response) => {
      console.log(response);
      this.converters = response.converters;
      this.pagination = response.pagination;
    });
  }

  delete(company) {
    this.modalService
        .show(ConfirmDialogComponent, {
          initialState: {
            message: 'Deseja realmente excluir o conversor?',
            note: 'Esta ação não poderá ser desfeita.',
          },
        })
        .content.onClose.subscribe((result) => {
          if (result) {
            this.converterService
              .delete(company.id)
              .subscribe((response) => {
                  this.toast.success("Conversor excluido.", 'Sucesso!');

                  this.converters = this.converters.filter(obj => obj !== company);
                },
                (err) => {
                  this.toast.error(err.error, 'Erro!');
                }
              );
          }
        });
  }

  changePageSize(size) {
    this.filters.take = size;
    this.filters.page = 1;
    
    this.paginate(this.filters.page);
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
