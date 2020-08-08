import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConverterService } from '../../../services/converter.service';
import { CompanyService } from '../../../services/company.service';
import { Converter } from '../../../models/converter';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
})
export class EditComponent implements OnInit {
  public route: string = '/converters';
  public errors: any = {};
  public converter: Converter = new Converter();
  private companies: any = [];
  public id: number;
  private selectedCompany: any = { };

  constructor(
    private routerActived: ActivatedRoute,
    private router: Router,
    private toast: ToastrService,
    private converterService: ConverterService,
    private companyService: CompanyService
  ) {}

  ngOnInit() {
    this.companyService.getAllWithoutFilters()
                       .subscribe((response) => {
                          this.companies = response.companies;
                        
                          if (!this.companies) {
                            this.toast.warning("Primeiramente você deve cadastrar uma empresa.", 'Cuidado!', { timeOut: 5000 });
                            this.router.navigate(['/companies/create']);
                            this.toast.info("Redirecionamos para facilitar, agora basta cadastrar a empresa desejada.", 'Aviso!', { timeOut: 5000 });
                          }

                          console.log(this.companies);

                          this.routerActived.params.subscribe((params) => {
                            this.id = params.id;
                            this.converterService
                                .get(this.id)
                                .subscribe((response) => {
                                    this.converter = response;
                                    let aux = this.converter as any;

                                    this.companies.forEach(company => {
                                      if (company.id == aux.empresaID) {
                                        this.changeCompany(company);
                                      }
                                    });
                                  },
                                  (err) => {
                                    this.router.navigate([this.route]);
                                  },
                                );
                          });
                        },
                        (err) => {
                          this.toast.error("Erro ao buscar empresas cadastradas.", 'Erro!');
                       });
  }

  changeCompany(company){
    this.selectedCompany = company; 
    this.converter.EmpresaID = this.selectedCompany.id;
  }

  save() {
    this.converterService
        .put(this.id, this.converter)
        .subscribe((response) => {
            this.toast.success('Conversor editado.', 'Sucesso!');
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
