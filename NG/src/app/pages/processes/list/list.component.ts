import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';
import { ProcessService } from '../../../services/process.service';
@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
})
export class ListComponent implements OnInit {
  public route: string = '/processes';
  public showFilter: boolean = false;
  public filters: any = {
    nome: '',
    page: 1,
    take: 5
  };
  public pagination: any = {};
  public processes: any = [];
  admin: boolean = false;

  constructor(
    private routerActived: ActivatedRoute,
    private router: Router,
    private modalService: BsModalService,
    private toast: ToastrService,
    private processService: ProcessService
  ) {  }

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
    this.processService.getAll(this.filters).subscribe((response) => {
      this.processes = response.processes;
      this.pagination = response.pagination;
    });
  }

  delete(process) {
    this.modalService
        .show(ConfirmDialogComponent, {
          initialState: {
            message: 'Deseja realmente excluir o processo "' + process.nome + '"?',
            note: 'Esta ação não poderá ser desfeita.',
          },
        })
        .content.onClose.subscribe((result) => {
          if (result) {
            this.processService
              .delete(process.id)
              .subscribe((response) => {
                  this.toast.success("Processo excluida.", 'Sucesso!');

                  this.processes = this.processes.filter(obj => obj !== process);
                },
                (err) => {
                  this.toast.error(err.error, 'Erro!');
                }
              );
          }
        });
  }

  details(invoice){
    // this.modalService.show();
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
