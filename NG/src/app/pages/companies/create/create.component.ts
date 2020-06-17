import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
})
export class CreateComponent implements OnInit {
  public route: string = '/companies';
  public errors: any = {};
  public department: any = {};

  constructor(
    private router: Router,
    private toast: ToastrService,
  ) {}

  ngOnInit() {
    this.department = {
      name: '',
      description: '',
    };
  }

  save() {
    // this.departmentService
    //   .insert(this.department)
    //   .then((response) => {
    //     this.toast.success(response.message, 'Sucesso!');
    //     this.router.navigate([this.route]);
    //   })
    //   .catch((response) => {
    //     this.errors = response.error.errors;
    //   });
  }

  cancel() {
    this.router.navigate([this.route]);
  }
}
