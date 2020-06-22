import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { FormGroup, FormBuilder } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})

export class UploadService {
    private api = environment.api.url + "/upload";
    private formGroup: FormGroup;

    constructor(public formBuilder: FormBuilder, private http: HttpClient) {
        this.formGroup = this.formBuilder.group({
            files: [null],
          })
    }

    post(file, processID, entrada) {
        const formData = new FormData(); 
        let params = new HttpParams();


        this.formGroup.patchValue({
            files: file
        });

        this.formGroup.get('files').updateValueAndValidity()

        formData.append('files', this.formGroup.get('files').value);

        params = params.append('Entrada', entrada);
        
        return this.http.post(this.api + "/" + processID, formData, { params: params, reportProgress: true, observe: 'events'  });
    }

    postAll(processID, files) {
        const formData = new FormData();

        files.forEach(element => {
            this.formGroup.patchValue({
                files: element
            });
    
            this.formGroup.get('files').updateValueAndValidity()
    
            formData.append('files', this.formGroup.get('files').value);
        });
        
        return this.http.post(this.api + "/" + processID, formData);
    }
}
