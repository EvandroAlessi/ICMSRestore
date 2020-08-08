import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Converter } from '../models/converter';

@Injectable({
  providedIn: 'root',
})

export class ConverterService {
    private api = environment.api.url + "/converters";

    constructor(private http: HttpClient) {}

    getUnitDiferences(processID): Observable<Converter[]> {
        let params = new HttpParams();

        params = params.append('processID', processID);
        
        return this.http.get<Converter[]>(this.api + '/unit-differences', { params: params });
    }

    getAll(filters): Observable<any> {
        let params = new HttpParams();

        params = params.append('page', filters.page);
        params = params.append('take', filters.take);

        if (filters.nome) {
            params = params.append('Nome', filters.nome);
        }
        
        return this.http.get<any>(this.api, { params: params });
    }

    getAllWithoutFilters() {
        return this.http.get<any>(this.api);
    }

    get(id: number){
        return this.http.get<Converter>(this.api + '/' + id);
    }

    post(converter: Converter) {
        return this.http.post<Converter>(this.api, converter);
    }

    put(id, converter: any) {
        return this.http.put<Converter>(this.api  + '/' + id, converter);
    }

    delete(id) {
        return this.http.delete(this.api  + '/' + id);
    }
}
