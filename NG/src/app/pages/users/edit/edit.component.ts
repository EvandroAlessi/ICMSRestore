import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../services/user.service';
import { User } from '../../../models/user';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
})
export class EditComponent implements OnInit {
  public route: string = '/users';
  public errors: any = {};
  public user: User = new User();
  id: number;
  public roles = ['ADMIN', 'FUNCIONARIO']

  constructor(
    private routerActived: ActivatedRoute,
    private router: Router,
    private toast: ToastrService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.routerActived.params.subscribe((params) => {
      this.id = params.id;
      this.userService
          .get(this.id)
          .subscribe((response) => {
              this.user = response;
            },
            (err) => {
              this.router.navigate([this.route]);
            },
          );
    });
  }

  save() {
    this.userService
        .put(this.id, this.user)
        .subscribe((response) => {
            this.toast.success('Usuário editado.', 'Sucesso!');
            localStorage.removeItem('user');
            window.dispatchEvent(new CustomEvent('user:logout'));

            this.router.navigate(['/login']);
          },
          (err) => {
            this.errors = err.error;
          }
        );
  }

  cancel() {
    this.router.navigate([this.route]);
  }
}
