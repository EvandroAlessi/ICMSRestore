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

    getAllByProcessID(id): Observable<any> {
        return this.http.get<any>(environment.api.url + '/processes/'+ id +'/filtered-items');
    }

    get(id){
        return this.http.get<any>(this.api + '/' + id);
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
