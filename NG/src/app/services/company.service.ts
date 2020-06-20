import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class CompanyService {
    private api = environment.api.url + "/companies";

    constructor(private http: HttpClient) {}

    getAll(filters): Observable<any> {
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
        
        return this.http.get<any>(this.api, { params: params });
    }

    get(id){
        return this.http.get<any>(this.api + '/' + id);
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
