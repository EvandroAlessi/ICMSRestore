import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Process } from '../models/process';

@Injectable({
  providedIn: 'root',
})

export class ProcessService {
    private api = environment.api.url + "/processes";

    constructor(private http: HttpClient) {}

    getAll(filters): Observable<any> {
        let params = new HttpParams();

        params = params.append('page', filters.page);
        params = params.append('take', filters.take);

        if (filters.nome) {
            params = params.append('Nome', filters.nome);
        }
        
        return this.http.get<any>(this.api, { params: params });
    }

    get(id){
        return this.http.get<any>(this.api + '/' + id);
    }

    post(process: Process) {
        return this.http.post<Process>(this.api, process);
    }

    put(id, process) {
        return this.http.put(this.api  + '/' + id, process);
    }

    delete(id) {
        return this.http.delete(this.api  + '/' + id);
    }
}
