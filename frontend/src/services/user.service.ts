import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from 'src/models/User.model';
import { BehaviorSubject } from 'rxjs';
import { PostService } from './post.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  userProfileData = new BehaviorSubject<User>(new User("", "", "", "", false, "",false));
  searchResults = new BehaviorSubject<User[]>([]);

  constructor(private http: HttpClient, private authService: AuthService, private postService: PostService, private router: Router) { }

  getUserProfileData() {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };

    this.http.get<User>("https://localhost:7243/api/User/GetLoggedInUser?userId=" + userId, requestOptions).subscribe({
      next: (result) => {
        this.userProfileData.next(result);
        this.postService.getUserProfilePosts();
      },
      error: (result) => {
        alert("Error while loading your profile : " + result.error);
      }
    })
  }

  searchUsersByName(searchName: string) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };

    this.http.get<User[]>("https://localhost:7243/api/User/SearchAllUsers?searchName=" + searchName, requestOptions).subscribe({
      next: (result) => {
        this.searchResults.next(result);
        this.router.navigate(['/search']);
      },
      error: (result) => {
        alert("Error while loading search results : " + result.error);
      }
    })
  }
}
