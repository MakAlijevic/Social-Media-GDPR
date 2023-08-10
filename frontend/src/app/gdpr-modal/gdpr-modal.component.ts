import { Component, OnInit } from '@angular/core';
import { Policy } from 'src/models/Policy.model';
import { AuthService } from 'src/services/auth.service';

@Component({
  selector: 'app-gdpr-modal',
  templateUrl: './gdpr-modal.component.html',
  styleUrls: ['./gdpr-modal.component.css']
})
export class GdprModalComponent implements OnInit {

  activeRegisterPolicyGdpr!: Policy;

  constructor(private authService: AuthService) {

  }

  ngOnInit(): void {
    this.authService.activeRegisterPolicyGdpr.subscribe(result => {
      this.activeRegisterPolicyGdpr = result;
    })
  }

}
