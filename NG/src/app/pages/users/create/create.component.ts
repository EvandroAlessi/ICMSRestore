import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
})
export class CreateComponent implements OnInit {
  public route: string = '/users';
  public errors: any = {};
  public user: any = {};
  public roles = ['ADMIN', 'FUNCIONARIO']

  constructor(
    private router: Router,
    private toast: ToastrService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.user = {
      nome: '',
      senha: '',
      cargo: this.roles[1],
    };
  }

  save() {
    this.userService
      .post(this.user)
      .subscribe((response) => {
          this.toast.success("Usuário criado.", 'Sucesso!');
          this.router.navigate([this.route]);
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
