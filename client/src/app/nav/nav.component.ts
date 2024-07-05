import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms'; //add imports
import { AccountService } from '../_services/account.service'; //add imports
import { BsDropdownModule } from 'ngx-bootstrap/dropdown'; //add imports

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule], //add imports
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent {
  //inject http login method from account.service.ts
  accountService = inject(AccountService);
  //create a model type any "empty"
  model: any = {};
  //create method login and store what is in model
  login() {
    this.accountService.login(this.model).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error) => console.log(error),
    });
  }
  //create funtion logout
  logout() {
    this.accountService.logout()
  }
}
