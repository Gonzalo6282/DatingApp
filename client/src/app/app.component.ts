import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from './nav/nav.component';
import { AccountService } from './_services/account.service';
import { HomeComponent } from './home/home.component';
import { NgxSpinnerComponent } from 'ngx-spinner';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [RouterOutlet, NavComponent, HomeComponent, NgxSpinnerComponent],
})
//Add implements OnInit in class component and Use quick fix
export class AppComponent implements OnInit {
  private accountService = inject(AccountService); //property accountService to inject AccountService

  //move function to bottom
  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user'); //check what is in local storage
    if (!userString) return; //if not userString breakout out of function
    const user = JSON.parse(userString); //parse userString
    this.accountService.currentUser.set(user); //pass currentUsr signal
  }
}
