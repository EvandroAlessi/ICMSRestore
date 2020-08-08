import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConverterService } from '../../../services/converter.service';
import { CompanyService } from '../../../services/company.service';
import { Converter } from '../../../models/converter';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
})
export class CreateComponent implements OnInit, AfterViewInit {
  public route: string = '/converters';
  public errors: any = {};
  public converter: Converter = new Converter();;
  public showCompanies = true;
  private companies: any = [];
  private selectedCompany: any = { };

  constructor(
    private router: Router,
    private toast: ToastrService,
    private converterService: ConverterService,
    private companyService: CompanyService
  ) {}
  ngAfterViewInit(): void {
    this.companyService.getAllWithoutFilters()
                       .subscribe((response) => {
                          this.companies = response.companies;
                        
                          if (this.companies) {
                            this.selectedCompany = this.companies[0];
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

  ngOnInit() {
    
  }

  changeCompany(company){
    this.selectedCompany = company; 
  }

  save() {
    if (this.showCompanies) {
      this.converter.EmpresaID = this.selectedCompany.id;
    }

    this.converterService
      .post(this.converter)
      .subscribe((response) => {
          this.toast.success("Conversor criado.", 'Sucesso!');
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
