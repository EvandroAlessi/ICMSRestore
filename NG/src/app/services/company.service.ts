import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class CompanyService {
  private api = environment.api.url + "/companies";

  constructor(private router: Router
      , private http: HttpClient) {}

    get(id: number){
        return this.http.get<any>(this.api + id);
    }

    getAll(): Observable<any> {
        return this.http.get<any>(this.api);
    }

    post(company: any) {
        return this.http.post(this.api, company);
    }

    put(company: any, id: number) {
        return this.http.put(this.api + id, company);
    }

    delete(id: number) {
        return this.http.delete(this.api + id);
    }
}
