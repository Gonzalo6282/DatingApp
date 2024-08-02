import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms'; //add imports
import { AccountService } from '../_services/account.service'; //add imports
import { BsDropdownModule } from 'ngx-bootstrap/dropdown'; //add imports
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive], //add imports
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent {
  //inject toastr service
  private toastr = inject(ToastrService);
  //inject http login method from account.service.ts
  accountService = inject(AccountService);
  //inject router
  private router = inject(Router);
  //create a model type any "empty"
  model: any = {};
  //create method login and store what is in model
  login() {
    this.accountService.login(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl('/members'); //when login route to members page
      },
      error: (error) => this.toastr.error(error.error), //pass toastr to error
    });
  }
  //create funtion logout
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/'); //when logout route to home page
  }
}
