import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Policy } from '../models/Policy.model';
import { BehaviorSubject } from 'rxjs';
import policyEnum from 'src/enums/policyEnum';
import { RegisterUserDto } from 'src/models/RegisterUserDto.model';
import { LoginUserDto } from 'src/models/LoginUserDto.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public showRegisterForm = new BehaviorSubject<boolean>(true);
  public showLoginForm = new BehaviorSubject<boolean>(false);

  public activeRegisterPolicyGdpr = new BehaviorSubject<Policy>(new Policy("", "", "", ""));

  constructor(private http: HttpClient, private router: Router) { }

  getActiveGdprPolicyForRegister() {
    this.http.get<Policy>("https://localhost:7243/api/Policy?id=" + policyEnum.registerPolicyEnumId).subscribe(result => {
      this.activeRegisterPolicyGdpr.next(result);
    });
  }

  registerUserAndValidate(userDto: RegisterUserDto): boolean {
    if (userDto.firstName == '' || userDto.lastName == '' || userDto.email == '' || userDto.password == '' || userDto.confirmPassword == '') {
      alert("You are missing a value in a field. Please fill in all the fields in order to register your account!");
      return false;
    }
    if (userDto.password !== userDto.confirmPassword) {
      alert("Please enter correct password confirmation!");
      return false;
    }

    var registerUserDto = {
      firstName: userDto.firstName,
      lastName: userDto.lastName,
      email: userDto.email,
      password: userDto.password,
      policyId: policyEnum.registerPolicyEnumId
    };

    this.http.post("https://localhost:7243/api/Auth/register", registerUserDto).subscribe({
      next: () => {
        alert("Successfully registered");
        this.showLoginForm.next(true);
        this.showRegisterForm.next(false);
        return true;
      },
      error: (result) => {
        console.log(result);
        alert("Unsuccessfull register : '" + result.error + "'");
        return false;
      }
    })
    return false;
  }

  loginUser(loginUserDto: LoginUserDto): boolean {
    this.http.post("https://localhost:7243/api/Auth/login", loginUserDto, { responseType: 'text' }).subscribe({
      next: (result) => {
        localStorage.setItem("token", result)
        alert("Successfully logged in!");
        this.router.navigate(['/home']);
        this.showLoginForm.next(false);
        this.showRegisterForm.next(false);
        return true;
      },
      error: (result) => {
        alert("Unsuccessfull login : " + result.error);
        return false;
      }
    });
    return false;
  }
}
