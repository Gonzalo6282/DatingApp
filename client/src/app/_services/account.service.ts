import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';
//this is to inject into components
@Injectable({
  providedIn: 'root',
})
export class AccountService {
  //inject http
  private http = inject(HttpClient);
  //create property for baseUrl
  baseUrl = environment.apiUrl;
  //angular signal to store current user object
  currentUser = signal<User | null>(null); //user or null, initial value is null

  //create method to login
  login(
    model: any //model:any = username ans password
  ) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      //pass type User in post
      map((user) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user)); //if user store in local storage passing the key 'uer' and string function JSON
          this.currentUser.set(user);
        }
      })
    );
  }

  //create method to register
  register(
    model: any //model:any = username ans password
  ) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      //pass type User in post
      map((user) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user)); //login the user as sonn as the register
          this.currentUser.set(user); //set user
        }
        return user;
      })
    );
  }

  logout() {
    localStorage.removeItem('user'); //remove user when logout
    this.currentUser.set(null); //set to null when logout
  }
}
