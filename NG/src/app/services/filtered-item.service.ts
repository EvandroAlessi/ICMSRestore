import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class FilteredItemService {
    private api = environment.api.url + "/filtered-items";

    constructor(private http: HttpClient) {}

    getAll(): Observable<any> {
        return this.http.get<any>(this.api);
    }

    getAllByProcessID(id, filters): Observable<any> {
        let params = new HttpParams();

        params = params.append('page', filters.page);
        params = params.append('take', filters.take);

        return this.http.get<any>(environment.api.url + '/processes/'+ id +'/filtered-items', { params: params});
    }

    get(id){
        return this.http.get<any>(this.api + '/' + id);
    }

    getSumarry(processID){
        let params = new HttpParams();

        params = params.append('processID', processID);

        return this.http.get<any>(this.api + '/summary-result', { params: params});
    }

    download(processID, ncm) {
        let params = new HttpParams();

        params = params.append('processID', processID);
        if(ncm)
            params = params.append('ncm', ncm);

        return this.http.get(this.api + '/download-results', { params: params, responseType: "arraybuffer"});
    }

    post(item: any) {
        return this.http.post(this.api, item);
    }

    put(id, item: any) {
        return this.http.put(this.api  + '/' + id, item);
    }

    delete(id) {
        return this.http.delete(this.api  + '/' + id);
    }
}
