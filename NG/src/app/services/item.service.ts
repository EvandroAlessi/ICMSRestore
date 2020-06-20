import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class ItemService {
    private api = environment.api.url + "/items";

    constructor(private http: HttpClient) {}

    getAll(): Observable<any> {
        return this.http.get<any>(this.api);
    }

    getAllByProcessID(id): Observable<any> {
        return this.http.get<any>(this.api);
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
