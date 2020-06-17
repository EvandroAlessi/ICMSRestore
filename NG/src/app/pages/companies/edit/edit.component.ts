import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CompanyService } from '../../../services/company.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
})
export class EditComponent implements OnInit {
  public route: string = '/companies';
  public errors: any = {};
  public company: any = {};
  public id: number;

  constructor(
    private routerActived: ActivatedRoute,
    private router: Router,
    private toast: ToastrService,
    private companyService: CompanyService
  ) {}

  ngOnInit() {
    this.routerActived.params.subscribe((params) => {
      this.id = params.id;
      this.companyService
          .get(this.id)
          .subscribe((response) => {
              this.company = response;
            },
            (err) => {
              this.router.navigate([this.route]);
            },
          );
    });
  }

  save() {
    this.companyService
        .put(this.id, this.company)
        .subscribe((response) => {
            this.toast.success('Empresa editada.', 'Sucesso!');
            this.router.navigate([this.route]);
          },
          (err) => {
            this.errors = err.error;
          }
        );
  }

  cancel() {
    this.router.navigate([this.route]);
  }
}
