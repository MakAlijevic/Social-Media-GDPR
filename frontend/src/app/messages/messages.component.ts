import { Component, OnInit } from '@angular/core';
import { User } from 'src/models/User.model';
import { MessageService } from 'src/services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messagesFriends!: User[]

  constructor(private messageService: MessageService) {

  }

  ngOnInit() {
    this.messageService.getMessagesFriendsList();
    this.messageService.messagesFriends.subscribe(result => {
      this.messagesFriends = result;
    })
    this.scrollToBottom();
  }

  scrollToBottom() {
    var scrollableContent = document.getElementById('scrollable-content');
    scrollableContent!.scrollTop = scrollableContent!.scrollHeight;
  }

}
