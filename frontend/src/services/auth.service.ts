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

  registerUserAndValidate(userDto: RegisterUserDto, callback: (success: boolean) => void): void {
    if (userDto.firstName == '' || userDto.lastName == '' || userDto.email == '' || userDto.password == '' || userDto.confirmPassword == '') {
      alert("You are missing a value in a field. Please fill in all the fields in order to register your account!");
      callback(false);
    }
    if (userDto.password !== userDto.confirmPassword) {
      alert("Please enter correct password confirmation!");
      callback(false);
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
        this.showRegisterForm.next(false);
        callback(true);
      },
      error: (result) => {
        console.log(result);
        alert("Unsuccessfull register : '" + result.error + "'");
        callback(false);
      }
    })
  }

  loginUser(loginUserDto: LoginUserDto, callback: (success: boolean) => void): void {
    this.http.post("https://localhost:7243/api/Auth/login", loginUserDto, { responseType: 'text' }).subscribe({
      next: (result) => {
        localStorage.setItem("userToken", result)
        alert("Successfully logged in!");
        callback(true);
      },
      error: (result) => {
        alert("Unsuccessfull login : " + result.error);
        callback(false);
      }
    });
  }

  logoutUser(callback: (success: boolean) => void) {
    const userToken = this.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };
    this.http.post("https://localhost:7243/api/Auth/logout?userId=" + userId, null, requestOptions).subscribe({
      next: (result) => {
        localStorage.removeItem("userToken");
        this.showLoginForm.next(true);
        this.router.navigate(['']);
        callback(true);
      },
      error: (result) => {
        alert("Unsuccessfull logout : " + result.error);
        callback(false)
      }
    });
  }

  validateUserLoggedIn() {
    var token = localStorage.getItem("userToken");
    if (token != null) {
      var tokenExpired = this.isTokenExpired(token);
      if (tokenExpired == false && this.router.url == "/") {
        this.showRegisterForm.next(false);
        this.showLoginForm.next(false);
        this.router.navigate(['/home']);
      }
      else if (tokenExpired == true) {
        alert("Your token has expired please login again");
        this.logoutUser((success) => {
        });
      }
    } else {
      this.showRegisterForm.next(true);
      this.showLoginForm.next(false);
      this.router.navigate(['']);
    }
  }

  isTokenExpired(token: string): boolean {
    try {
      const decodedToken = JSON.parse(atob(token.split('.')[1]));
      if (decodedToken.exp) {
        const currentTimestamp = Math.floor(Date.now() / 1000);
        const checkIncrementDecodedToken = decodedToken.exp - 900;
        return checkIncrementDecodedToken < currentTimestamp;
      } else {
        return false;
      }
    } catch (error) {
      return false;
    }
  }

  getUserTokenAndDecode() {
    var token = localStorage.getItem("userToken");
    if (token !== null && token !== undefined) {
      return JSON.parse(atob(token.split('.')[1]));
    }
  }
}