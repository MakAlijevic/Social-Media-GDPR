import { Component } from '@angular/core';
import { AppComponent } from '../app.component';
import { AuthService } from 'src/services/auth.service';
import { FormBuilder } from '@angular/forms';
import { RegisterUserDto } from 'src/models/RegisterUserDto.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  registerUserForm = this.formBuilder.group({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    confirmPassword: ''
  })

  constructor(private appComponent: AppComponent, private authService: AuthService, private formBuilder: FormBuilder) {

  }

  showLogin() {
    this.appComponent.showRegisterForm = false;
    this.appComponent.showLoginForm = true;
  }

  getActiveGdprPolicyForRegister() {
    this.authService.getActiveGdprPolicyForRegister();
  }

  registerUser() {
    var registerUserDto = new RegisterUserDto(this.registerUserForm.value.firstName!, this.registerUserForm.value.lastName!, this.registerUserForm.value.email!, this.registerUserForm.value.password!, this.registerUserForm.value.confirmPassword!);
    this.authService.registerUserAndValidate(registerUserDto, (success) => {
      if (success === true) {
        this.registerUserForm.reset();
        this.showLogin();
      }
    });
  }

}
