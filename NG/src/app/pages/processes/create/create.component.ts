import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProcessService } from '../../../services/process.service';
import { Process } from '../../../models/process';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
})
export class CreateComponent implements OnInit {
  public route: string = '/processes';
  public errors: any = {};
  public process: Process;

  constructor(
    private router: Router,
    private toast: ToastrService,
    private processService: ProcessService
  ) {}

  ngOnInit() {
    this.process = new Process();
  }

  save() {
    this.processService
      .post(this.process)
      .subscribe((response) => {
          this.toast.success("Processo criada.", 'Sucesso!');
          this.router.navigate([this.route]);
        },
        (err) => {
          console.log(err);
          this.errors = err.error.errors;
        }
      );
  }

  cancel() {
    this.router.navigate([this.route]);
  }
}
