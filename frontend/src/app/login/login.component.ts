import { Component } from '@angular/core';
import { AppComponent } from '../app.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private appComponent: AppComponent, private router: Router) {

  }

  showRegister() {
    this.appComponent.showLoginForm = false;
    this.appComponent.showRegisterForm = true;
  }

  login() {
    this.appComponent.showLoginForm = false;
    this.appComponent.showRegisterForm = false;
    this.router.navigate(['/home']);
  }

}
