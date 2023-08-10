import { Component } from '@angular/core';
import { AppComponent } from '../app.component';
import { FormBuilder } from '@angular/forms';
import { LoginUserDto } from 'src/models/LoginUserDto.model';
import { AuthService } from 'src/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginUserForm = this.formBuilder.group({
    email: '',
    password: ''
  })

  constructor(private appComponent: AppComponent, private formBuilder: FormBuilder, private authService: AuthService) {

  }

  showRegister() {
    this.appComponent.showLoginForm = false;
    this.appComponent.showRegisterForm = true;
  }

  loginUser() {
    var loginUserDto = new LoginUserDto(this.loginUserForm.value.email!, this.loginUserForm.value.password!);
    var status = this.authService.loginUser(loginUserDto);
    if (status === true) {
      this.loginUserForm.reset();
    }
  }

}
