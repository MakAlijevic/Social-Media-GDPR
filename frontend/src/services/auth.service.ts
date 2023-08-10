import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Policy } from '../models/Policy.model';
import { BehaviorSubject } from 'rxjs';
import policyEnum from 'src/enums/policyEnum';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public policyGdpr = new BehaviorSubject<Policy>(new Policy("", "", "", ""));

  constructor(private http: HttpClient) { }

  getActiveGdprPolicy() {
    this.http.get<Policy>("https://localhost:7243/api/Policy?id=" + policyEnum.registerPolicyEnumId).subscribe(result => {
      this.policyGdpr.next(result);
    });
  }
}
