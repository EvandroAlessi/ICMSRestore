import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class InvoiceService {
    private api = environment.api.url + "/invoices";

    constructor(private http: HttpClient) {}

    getAll(filters): Observable<any> {
        let params = new HttpParams();

        params = params.append('page', filters.page);
        params = params.append('take', filters.take);

        if (filters.cnpj) {
            params = params.append('CNPJ', filters.cnpj);
        }
        
        return this.http.get<any>(this.api, { params: params });
    }

    get(id){
        return this.http.get<any>(this.api + '/' + id);
    }

    getAllSimplify(filters): Observable<any> {
        let params = new HttpParams();

        params = params.append('page', filters.page);
        params = params.append('take', filters.take);

        if (filters.cnpj) {
            params = params.append('CNPJ', filters.cnpj);
        }
        
        return this.http.get<any>(this.api + '/simplify', { params: params });
    }

    post(invoice: any) {
        return this.http.post(this.api, invoice);
    }

    put(id, invoice: any) {
        return this.http.put(this.api  + '/' + id, invoice);
    }

    delete(id) {
        return this.http.delete(this.api  + '/' + id);
    }
}
