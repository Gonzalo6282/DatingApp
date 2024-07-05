import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule], //add import
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  //inject toastr service
  private toastr = inject(ToastrService);
  //inject accountService
  private accountService = inject(AccountService);
  //add signal cancel register in parent component
  cancelRegister = output<boolean>();
  //create model set to any
  model: any = {};
  //create function register
  register() {
    this.accountService.register(this.model).subscribe({
      next: (response) => {
        console.log(response);
        this.cancel(); //call cancel to close register form
      },
      error: (error) => this.toastr.error(error.error), //pass toastr to error
    });
  }
  //create function cancel
  cancel() {
    this.cancelRegister.emit(false);
  }
}
