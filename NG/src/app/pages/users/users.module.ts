import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UsersRouting } from './users.routing';

@NgModule({
  imports: [CommonModule, FormsModule, RouterModule, UsersRouting],
})
export class UsersModule {}
