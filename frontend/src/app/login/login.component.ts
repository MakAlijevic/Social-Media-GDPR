import { Component } from '@angular/core';
import { AppComponent } from '../app.component';
import { FormBuilder } from '@angular/forms';
import { LoginUserDto } from 'src/models/LoginUserDto.model';
import { AuthService } from 'src/services/auth.service';
import { FollowService } from 'src/services/follow.service';
import { Router } from '@angular/router';

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

  constructor(private appComponent: AppComponent, private formBuilder: FormBuilder, private authService: AuthService, private followService: FollowService, private router: Router) {

  }

  showRegister() {
    this.appComponent.showLoginForm = false;
    this.appComponent.showRegisterForm = true;
  }

  loginUser() {
    var loginUserDto = new LoginUserDto(this.loginUserForm.value.email!, this.loginUserForm.value.password!);
    this.authService.loginUser(loginUserDto, (success) => {
      if (success === true) {
        this.loginUserForm.reset();
        this.authService.showLoginForm.next(false);
        this.authService.showRegisterForm.next(false);
        this.appComponent.showLoginForm = false;
        this.appComponent.showRegisterForm = false;
        this.router.navigate(['/home']);
        this.followService.getOnlineFollows();
      }
    });
  }
}
