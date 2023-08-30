import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/models/User.model';
import { FollowService } from 'src/services/follow.service';
import { MessageService } from 'src/services/message.service';

@Component({
  selector: 'app-friend-profile',
  templateUrl: './friend-profile.component.html',
  styleUrls: ['./friend-profile.component.css']
})
export class FriendProfileComponent {

  @Input() userData!: User;

  constructor(private messageService: MessageService, private router: Router, private followService: FollowService) 
  {

  }
  
  followUser(){
    this.followService.addFollow(this.userData.userId, (success) => {
      if(success === true) {
        this.userData.isFollowed = true;
      }
    });
  }

  unfollowUser(){
    this.followService.unfollow(this.userData.userId, (success) => {
      if(success === true) {
        this.userData.isFollowed = false;
      }
    });
  }

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
