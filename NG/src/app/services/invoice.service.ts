import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class InvoiceService {
  private api = environment.api.url + "/invoices";

  constructor(private router: Router
      , private http: HttpClient) {}

    get(id){
        return this.http.get<any>(this.api + '/' + id);
    }

    getAll(filters): Observable<any> {
        console.log(filters);
        let params = new HttpParams();

        params = params.append('page', filters.page);
        params = params.append('take', filters.take);

        if (filters.cnpj) {
            params = params.append('CNPJ', filters.cnpj);
        }

        if (filters.nome) {
            params = params.append('Nome', filters.nome);
        }

        if (filters.cidade) {
            params = params.append('Cidade', filters.cidade);
        }

        if (filters.uf) {
            params = params.append('UF', filters.uf);
        }
        
        console.log(params);
        return this.http.get<any>(this.api, { params: params });
    }

    getAllSimplify(filters): Observable<any> {
        console.log(filters);
        let params = new HttpParams();

        params = params.append('page', filters.page);
        params = params.append('take', filters.take);

        if (filters.cnpj) {
            params = params.append('CNPJ', filters.cnpj);
        }

        if (filters.nome) {
            params = params.append('Nome', filters.nome);
        }

        if (filters.cidade) {
            params = params.append('Cidade', filters.cidade);
        }

        if (filters.uf) {
            params = params.append('UF', filters.uf);
        }
        
        console.log(params);
        return this.http.get<any>(this.api + '/simplify', { params: params });
    }

    post(company: any) {
        return this.http.post(this.api, company);
    }

    put(id, company: any) {
        return this.http.put(this.api  + '/' + id, company);
    }

    delete(id) {
        return this.http.delete(this.api  + '/' + id);
    }
}
