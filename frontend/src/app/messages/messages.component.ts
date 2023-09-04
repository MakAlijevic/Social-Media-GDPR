import { Component, OnInit } from '@angular/core';
import { Message } from 'src/models/Message.model';
import { User } from 'src/models/User.model';
import { AuthService } from 'src/services/auth.service';
import { MessageService } from 'src/services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messagesFriends!: User[]
  activeMessages!: Message[]

  constructor(private messageService: MessageService, private authService: AuthService) {

  }

  ngOnInit() {
    this.messageService.getMessagesFriendsList();
    this.messageService.messagesFriends.subscribe(result => {
      this.messagesFriends = result;
    })
    this.scrollToBottom();
    this.messageService.activeMessages.subscribe(result => {
      this.activeMessages = result;
      this.scrollToBottom();
      this.messageService.getMessagesFriendsList();
    })
  }

  scrollToBottom() {
    var scrollableContent = document.getElementById('scrollable-content');
    scrollableContent!.scrollTop = scrollableContent!.scrollHeight;
  }

  loadActiveMessages(recieverId: string) {
    this.messageService.getMessagesSingleChat(recieverId);
  }

  addMessageToActiveChat() {
    var message = document.getElementById("sendMessageForm") as HTMLInputElement;
    const content = message.value;
    if (this.activeMessages !== null && this.activeMessages.length > 0) {
      const activeChatUserId = this.getActiveChatUserId();

      this.messageService.addNewMessageToActiveChat(activeChatUserId, content);
    }
  }

  getActiveChatUserId() {
    var message = this.activeMessages[0];

    const userToken = this.authService.getUserTokenAndDecode();
    const loggedInUser = userToken.serialNumber;

    if (message.senderId === loggedInUser) {
      return message.recieverId;
    }
    return message.senderId;
  }

}
