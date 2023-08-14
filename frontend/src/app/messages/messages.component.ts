import { Component, OnInit } from '@angular/core';
import { Message } from 'src/models/Message.model';
import { User } from 'src/models/User.model';
import { MessageService } from 'src/services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messagesFriends!: User[]
  activeMessages!: Message[]

  constructor(private messageService: MessageService) {

  }

  ngOnInit() {
    this.messageService.getMessagesFriendsList();
    this.messageService.messagesFriends.subscribe(result => {
      this.messagesFriends = result;
    })
    this.scrollToBottom();
    this.messageService.activeMessages.subscribe(result => {
      this.activeMessages = result;
    })
  }

  scrollToBottom() {
    var scrollableContent = document.getElementById('scrollable-content');
    scrollableContent!.scrollTop = scrollableContent!.scrollHeight;
  }

  loadActiveMessages(recieverId: string) {
    this.messageService.getMessagesSingleChat(recieverId);
  }

}
