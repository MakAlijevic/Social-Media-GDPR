import { Component } from '@angular/core';
import { AppComponent } from '../app.component';
import { AuthService } from 'src/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  constructor(private appComponent: AppComponent, private authService: AuthService) {

  }

  showLogin() {
    this.appComponent.showRegisterForm = false;
    this.appComponent.showLoginForm = true;
  }

  getActiveGdprPolicy() {
    this.authService.getActiveGdprPolicy();
  }

}
