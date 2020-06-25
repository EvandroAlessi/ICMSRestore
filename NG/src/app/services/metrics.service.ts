import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class MetricsService {
    private api = environment.api.url + "/metrics";

    constructor(private http: HttpClient) {}

    getCounts(): Observable<any> {
        return this.http.get<any>(this.api + "/counts");
    }
}
