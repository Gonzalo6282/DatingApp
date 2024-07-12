import { Component } from '@angular/core';
import { RegisterComponent } from '../register/register.component';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  imports: [RegisterComponent],
})
export class HomeComponent {
  registerMode = false; //to be use as conditional to show nuttons or form in home.componenets.html

  registerToggle() {
    this.registerMode = !this.registerMode; //if is false will set it to true and if is true will set it to false
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
