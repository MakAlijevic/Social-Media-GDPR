import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { User } from 'src/models/User.model';
import { AuthService } from 'src/services/auth.service';
import { FollowService } from 'src/services/follow.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  showRegisterForm = true;
  showLoginForm = false;

  onlineFollows!: User[];

  constructor(public router: Router, private authService: AuthService, private followService: FollowService) {

  }
  ngOnInit(): void {
    this.followService.getOnlineFollows();
    this.authService.validateUserLoggedIn();
    this.authService.showLoginForm.subscribe(result => {
      this.showLoginForm = result;
    });
    this.authService.showRegisterForm.subscribe(result => {
      this.showRegisterForm = result;
    });
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.authService.validateUserLoggedIn();
      }
    });
    this.followService.onlineFollows.subscribe(result => {
      this.onlineFollows = result;
    });
  }

  logoutUser() {
    this.authService.logoutUser();
  }

}
