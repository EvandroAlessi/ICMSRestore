import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
})
export class EditComponent implements OnInit {
  public route: string = '/departments';
  public errors: any = {};
  public department: any = {};

  constructor(
    private routerActived: ActivatedRoute,
    private router: Router,
    private toast: ToastrService
  ) {}

  ngOnInit() {
    this.routerActived.params.subscribe((params) => {
      const id = params.id;
      // this.departmentService
      //   .getById(id)
      //   .then((response) => {
      //     this.department = response.department;
      //   })
      //   .catch(() => {
      //     this.router.navigate([this.route]);
      //   });
    });
  }

  save() {
    // this.departmentService
    //   .update(this.department)
    //   .then((result) => {
    //     this.toast.success(result.message, 'Sucesso!');
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
