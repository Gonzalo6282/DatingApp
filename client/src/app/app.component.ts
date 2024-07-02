import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
//Add implements OnInit in class component and Use quick fix
export class AppComponent implements OnInit {
  http = inject(HttpClient); //property http to inject HttpClient
  title = 'DatingApp';
  users: any; //add property users

  //move function to bottom
  ngOnInit(): void {
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
