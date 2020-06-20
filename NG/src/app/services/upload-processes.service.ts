import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class UploadProcessService {
    private api = environment.api.url + "/upload-processes";

    constructor(private http: HttpClient) {}

    getAll(filters): Observable<any> {
        
        return this.http.get<any>(this.api);
    }

    getAllByProcessID(id){
        return this.http.get<any>(environment.api.url + '/processes/'+ id +'/upload-processes');
    }

    get(id){
        return this.http.get<any>(this.api + '/' + id);
    }

    getState(id){
        return this.http.get<any>(this.api + '/state/' + id);
    }

    post(uploadProcess) {
        return this.http.post(this.api, uploadProcess);
    }

    put(id, uploadProcess) {
        return this.http.put(this.api  + '/' + id, uploadProcess);
    }

    delete(id) {
        return this.http.delete(this.api  + '/' + id);
    }
}
