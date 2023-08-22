import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { User } from 'src/models/User.model';
import { BehaviorSubject } from 'rxjs';
import { Message } from 'src/models/Message.model';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  public messagesFriends = new BehaviorSubject<User[]>([]);
  public activeMessages = new BehaviorSubject<Message[]>([]);

  constructor(private http: HttpClient, private authService: AuthService) { }

  getMessagesFriendsList() {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };

    this.http.get<User[]>("https://localhost:7243/api/Message/getAllFriendsForMessages?userId=" + userId, requestOptions).subscribe({
      next: (result) => {
        this.messagesFriends.next(result);
      },
      error: (result) => {
        alert("Error while loading your friends list : " + result.error);
      }
    })
  }

  getMessagesSingleChat(recieverId: string) {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };

    this.http.get<Message[]>("https://localhost:7243/api/Message/GetAllMessagesBetweenFriends?FollowerId=" + userId + "&FollowingId=" + recieverId, requestOptions).subscribe({
      next: (result) => {
        this.activeMessages.next(result);
      },
      error: (result) => {
        alert("Error while loading your messages : " + result.error);
      }
    });
  }

  addNewMessageToActiveChat(recieverId: string, content: string): any {
    if (content === "") {
      return;
    }
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };
    const requestBody = {
      senderId: userId,
      recieverId: recieverId,
      content: content
    }

    this.http.post<Message>("https://localhost:7243/api/Message", requestBody, requestOptions).subscribe({
      next: (result) => {
        var currentActiveMessages = this.activeMessages.getValue();
        currentActiveMessages.push(result);
        this.activeMessages.next(currentActiveMessages);
        this.resetSendMessageForm();
      },
      error: (result) => {
        alert("Error while sending your message : " + result.error);
      }
    });
  }

  resetSendMessageForm() {
    const messageContent = document.getElementById("sendMessageForm") as HTMLInputElement;
    messageContent.value = "";
  }
}
