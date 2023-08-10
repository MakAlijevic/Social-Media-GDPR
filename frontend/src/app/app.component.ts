import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AuthService } from 'src/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  showRegisterForm = true;
  showLoginForm = false;

  constructor(public router: Router, private authService: AuthService) {

  }
  ngOnInit(): void {
    this.authService.validateUserLoggedIn();
    this.authService.showLoginForm.subscribe(result => {
      this.showLoginForm = result;
    })
    this.authService.showRegisterForm.subscribe(result => {
      this.showRegisterForm = result;
    })
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.authService.validateUserLoggedIn();
      }
    });
  }

  logoutUser() {
    this.authService.logoutUser();
  }

}
