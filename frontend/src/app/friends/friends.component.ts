import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/models/User.model';
import { FollowService } from 'src/services/follow.service';
import { MessageService } from 'src/services/message.service';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent {

  chatUsers!: User[];
  constructor(private messageService: MessageService, private router: Router) {

  }

  @Input() userData!: User;

  openChat() {
    this.messageService.doesChatExist(this.userData.userId, (success) => {
      if(success === true) {
        this.router.navigate(['/messages']);
      }
      else {
        this.messageService.startAChat(this.userData.userId);
      }
    });
  }
}
