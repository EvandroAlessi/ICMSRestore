import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProcessService } from '../../../services/process.service';
import { CompanyService } from '../../../services/company.service';
import { Process } from '../../../models/process';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
})
export class CreateComponent implements OnInit {
  public route: string = '/processes';
  public errors: any = {};
  public process: Process;
  private companies: any = [];
  private selectedCompany: any = { };

  constructor(
    private router: Router,
    private toast: ToastrService,
    private processService: ProcessService,
    private companyService: CompanyService
  ) {}

  ngOnInit() {
    this.process = new Process();

    this.companyService.getAllWithoutFilters()
                       .subscribe((response) => {
                          this.companies = response.companies;
                        
                          if (this.companies) {
                            this.selectedCompany = this.companies[0];
                            this.process.EmpresaID = this.selectedCompany.id;
                          }
                          else {
                            this.toast.warning("Primeiramente você deve cadastrar uma empresa.", 'Cuidado!', { timeOut: 5000 });
                            this.router.navigate(['/companies/create']);
                            this.toast.info("Redirecionamos para facilitar, agora basta cadastrar a empresa desejada.", 'Aviso!', { timeOut: 5000 });
                          }
                        },
                        (err) => {
                          this.toast.error("Erro ao buscar empresas cadastradas.", 'Erro!');
                       });
  }

  changeCompany(company){
    this.selectedCompany = company; 
    this.process.EmpresaID = this.selectedCompany.id;
  }

  save() {
    this.processService
      .post(this.process)
      .subscribe((response) => {
          this.toast.success("Processo criada.", 'Sucesso!');
          this.router.navigate([this.route]);
        },
        (err) => {
          this.errors = err.error.errors;
          this.toast.error("Algo deu errado, tente novamente.", 'Erro :(');
        }
      );
  }

  cancel() {
    this.router.navigate([this.route]);
  }
}
