import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { MessageService } from './message.service';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  constructor(private messageService: MessageService, private authService: AuthService) { }

  private hubConnection!: signalR.HubConnection

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7243/message')
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }
  public addMessageTransferListener = () => {
    this.hubConnection.on('MessageAdded', () => {

    })
  }
}
