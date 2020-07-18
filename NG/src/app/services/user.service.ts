import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})

export class UserService {
    private api = environment.api.url + "/users";

    constructor(private http: HttpClient) {}

    getAll(filters): Observable<any> {
        let params = new HttpParams();

        params = params.append('page', filters.page);
        params = params.append('take', filters.take);

        if (filters.nome) {
            params = params.append('Nome', filters.nome);
        }

        if (filters.cargo) {
            params = params.append('Cargo', filters.cargo);
        }
        
        return this.http.get<any>(this.api, { params: params });
    }

    get(id){
        return this.http.get<any>(this.api + '/' + id);
    }

    post(user: any) {
        return this.http.post(this.api, user);
    }

    changeRole(userID, role) {
        const userChangeRole = {
            ID: userID,
            Role: role
        }

        return this.http.put(this.api, userChangeRole);
    }

    put(id, user: User) {
        return this.http.put(this.api  + '/' + id, user);
    }

    delete(id) {
        return this.http.delete(this.api  + '/' + id);
    }
}
