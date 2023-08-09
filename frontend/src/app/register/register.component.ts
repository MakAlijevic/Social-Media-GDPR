import { Component } from '@angular/core';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  constructor(private appComponent: AppComponent) {

  }

  showLogin() {
    this.appComponent.showRegisterForm = false;
    this.appComponent.showLoginForm = true;
  }

}
