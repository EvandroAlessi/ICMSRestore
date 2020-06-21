import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProcessService } from '../../../../services/process.service';
import { Process } from '../../../../models/process';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html'
})
export class EditComponent implements OnInit {
  public errors: any = {};
  public process: Process = new Process();
  public id: number;
  
  constructor(
    private toast: ToastrService,
    private processService: ProcessService,
  ) {}

  ngOnInit() {}
  
  getProcess(id){
    this.id = id;

    this.processService
          .get(this.id)
          .subscribe((response) => {
              this.process = response;
            },
            (err) => {
              this.toast.error('Tente novamente.', 'Erro ao salvar dados!');
              this.getProcess(this.id);
            },
          );
  }
  
  save() {
    this.processService
        .put(this.id, this.process)
        .subscribe((response) => {
            this.toast.success('Processo editado.', 'Sucesso!');
            // this.router.navigate([this.route]);
          },
          (error) => {
            this.errors = error.error;
          },
        );
  }

  cancel() {
    this.getProcess(this.id);
  }
}
