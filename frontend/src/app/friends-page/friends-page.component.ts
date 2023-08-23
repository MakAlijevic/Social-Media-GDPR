import { Component, OnInit } from '@angular/core';
import { User } from 'src/models/User.model';
import { FollowService } from 'src/services/follow.service';

@Component({
  selector: 'app-friends-page',
  templateUrl: './friends-page.component.html',
  styleUrls: ['./friends-page.component.css']
})
export class FriendsPageComponent {

  allFriends!: User[];
  searchParam = '';

  constructor(private followService: FollowService) {

  }

  searchFollowedUsersByName() {
    this.followService.searchFollowedUsersByName(this.searchParam);
    this.followService.followedSearchResults.subscribe(result => {
      this.allFriends = result;
    })

  }

}
