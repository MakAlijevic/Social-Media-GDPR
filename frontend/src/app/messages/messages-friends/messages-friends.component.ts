import { Component, Input, OnInit } from '@angular/core';
import { User } from 'src/models/User.model';
import { MessageService } from 'src/services/message.service';

@Component({
  selector: 'app-messages-friends',
  templateUrl: './messages-friends.component.html',
  styleUrls: ['./messages-friends.component.css']
})
export class MessagesFriendsComponent implements OnInit {

  @Input() friend!: User
  public isSelected = false;

  constructor(private messageService: MessageService) {

  }

  ngOnInit(): void {
    this.messageService.activeUserForMessagesId.subscribe(result => {
      if (this.friend.userId === result) {
        this.isSelected = true;
      } else {
        this.isSelected = false;
      }
    })
  }

}
