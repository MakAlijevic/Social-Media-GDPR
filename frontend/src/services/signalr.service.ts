import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { MessageService } from './message.service';
import { AuthService } from './auth.service';
import { FollowService } from './follow.service';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  constructor(private messageService: MessageService, private authService: AuthService, private followService: FollowService) { }

  private messageHubConnection!: signalR.HubConnection
  private onlineFollowingHubConnection!: signalR.HubConnection

  public startConnection = () => {
    this.messageHubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7243/message')
      .build();
    this.messageHubConnection
      .start()
      .then(() => console.log('Connection 1 started'))
      .catch(err => console.log('Error while starting connection 1: ' + err))

    this.onlineFollowingHubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7243/onlinefollowing')
      .build();
    this.onlineFollowingHubConnection
      .start()
      .then(() => console.log('Connection 2 started'))
      .catch(err => console.log('Error while starting connection 2: ' + err))
  }

  public addMessageTransferListener = () => {
    this.messageHubConnection.on('MessageAdded', () => {
      const recieverMessage = this.messageService.activeMessages.getValue()[0];
      const userToken = this.authService.getUserTokenAndDecode();
      const userId = userToken.serialNumber;
      var reciever = "";
      if (recieverMessage.senderId === userId) {
        reciever = recieverMessage.recieverId;
      } else {
        reciever = recieverMessage.senderId;
      }
      setTimeout(()=> {
        this.messageService.getMessagesSingleChat(reciever);
      }, 750);
    })

  }

  public onlineFollowingUpdateListener = () => {
    this.onlineFollowingHubConnection.on('OnlineFollowingUpdate', (onlineFollowingUpdateList: any) => {
      const userToken = this.authService.getUserTokenAndDecode();
      const userId = userToken.serialNumber;
      for (const followingElement of onlineFollowingUpdateList) {
        if (followingElement.id === userId) {
          this.followService.getOnlineFollows();
        }
      }

    })
  }
}
