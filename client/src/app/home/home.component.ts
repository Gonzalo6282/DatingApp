import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from '../register/register.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  imports: [RegisterComponent],
})
export class HomeComponent implements OnInit {
  http = inject(HttpClient); //property http to inject HttpClient
  registerMode = false; //to be use as conditional to show nuttons or form in home.componenets.html
  users: any; //add property users

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode; //if is false will set it to true and if is true will set it to false
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  getUsers() {
    //make http get request
    this.http.get('https:/localhost:5001/api/users').subscribe({
      //add boiler plates of callback functions
      //next: () => {},
      next: (response) => (this.users = response), //response equals to  users response from API server
      //error: () => {},
      error: (error) => console.log(error),
      //complete: () => {},
      complete: () => console.log('Request has completed'),
    });
  }
}
