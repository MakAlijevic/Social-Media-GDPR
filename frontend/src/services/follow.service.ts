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
  public onlineFollows = new BehaviorSubject<User[]>([]);
  followedSearchResults = new BehaviorSubject<User[]>([]);

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

  
  addFollow(followingId: string, callback: (success: boolean) => void): void  {
    const userToken = this.authService.getUserTokenAndDecode();
  
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    }
    var requestBody = {
      followerId: userToken.serialNumber,
      followingId: followingId
    }

    this.http.post<any>("https://localhost:7243/api/Follow", requestBody, requestOptions).subscribe({
      next: () => {
        callback(true);
      },
      error: (result) => {
        alert("Error while following users : " + result.error);
        callback(false);
      }
    })
  }

  unfollow(followingId: string, callback: (success: boolean) => void): void {
    const userToken = this.authService.getUserTokenAndDecode();

    var requestBody = {
      followerId: userToken.serialNumber,
      followingId: followingId
    }

    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!),
      responseType: 'text',
      body: requestBody
    }
    
    this.http.delete<any>("https://localhost:7243/api/Follow", requestOptions).subscribe({
      next: () => {
        callback(true);
      },
      error: (result) => {
        alert("Error while unfollowing users : " + result.error);
        callback(false);
      }
    })
  }

  getOnlineFollows() {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;

    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    }
    this.http.get<User[]>("https://localhost:7243/api/Follow/onlineFollows?userId=" + userId, requestOptions).subscribe({
      next: (result) => {
        this.onlineFollows.next(result);
      },
      error: (result) => {
        console.log("Error while getting online following users : " + result.error);
      }
    })
  }

  searchFollowedUsersByName(searchName: string) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };

    this.http.get<User[]>("https://localhost:7243/api/Follow/SearchFollowedUsers?searchName=" + searchName, requestOptions).subscribe({
      next: (result) => {
        this.followedSearchResults.next(result);
      },
      error: (result) => {
        alert("Error while loading search results : " + result.error);
      }
    })
  }

  async isUserFollowed(followingId: string): Promise<boolean> {
    const userToken = this.authService.getUserTokenAndDecode();
    const followerId = userToken.serialNumber;
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };
    var isFollowed = false;

    await this.http.get<boolean>("https://localhost:7243/api/Follow/checkIfUserFollowed?followerId=" + followerId + "&followingId=" + followingId, requestOptions).subscribe({
      next: (result) => {
        isFollowed = result;
        console.log(result);
      },
      error: (result) => {
        alert("Error while loading search results : " + result.error);
      }
    })
    setTimeout(() => {
      console.log(isFollowed);
    }, 1000)
    return isFollowed;
  }
}
