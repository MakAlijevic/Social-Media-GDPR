import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { BehaviorSubject } from 'rxjs';
import { User } from 'src/models/User.model';

@Injectable({
  providedIn: 'root'
})
export class FollowService {

  public allFollows = new BehaviorSubject<User[]>([]);

  constructor(private http: HttpClient, private authService: AuthService) { }

  getAllFollows() {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;

    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    }
    this.http.get<User[]>("https://localhost:7243/api/Follow/allFollows?userId=" + userId, requestOptions).subscribe({
      next: (result) => {
        this.allFollows.next(result);
      },
      error: (result) => {
        alert("Error while getting all following users : " + result.error);
      }
    })
  }
}
