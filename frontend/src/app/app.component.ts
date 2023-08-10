import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'frontend';
  showRegisterForm = true;
  showLoginForm = false;

  constructor(public router: Router, private authService: AuthService) {

  }
  ngOnInit(): void {
    this.authService.showLoginForm.subscribe(result => {
      this.showLoginForm = result;
    })
    this.authService.showRegisterForm.subscribe(result => {
      this.showRegisterForm = result;
    })
  }

}
