import { Component, Input, OnInit } from '@angular/core';
import { User } from 'src/models/User.model';
import { FollowService } from 'src/services/follow.service';

@Component({
  selector: 'app-search-profile',
  templateUrl: './search-profile.component.html',
  styleUrls: ['./search-profile.component.css']
})
export class SearchProfileComponent {
  @Input() userData!: User;

  constructor(private followService: FollowService) {
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
}
