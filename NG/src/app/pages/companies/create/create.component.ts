import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CompanyService } from '../../../services/company.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
})
export class CreateComponent implements OnInit {
  public route: string = '/companies';
  public errors: any = {};
  public company: any = {};

  constructor(
    private router: Router,
    private toast: ToastrService,
    private companyService: CompanyService
  ) {}

  ngOnInit() {
    this.company = {
      cnpj: '',
      nome: '',
      cidade: '',
      uf: ''
    };
  }

  save() {
    this.companyService
      .post(this.company)
      .subscribe((response) => {
          this.toast.success("Empresa criada.", 'Sucesso!');
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
