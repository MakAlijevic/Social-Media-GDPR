import { Component, OnInit } from '@angular/core';
import { User } from 'src/models/User.model';
import { FollowService } from 'src/services/follow.service';

@Component({
  selector: 'app-friends-page',
  templateUrl: './friends-page.component.html',
  styleUrls: ['./friends-page.component.css']
})
export class FriendsPageComponent implements OnInit {

  allFriends!: User[];
  searchParam = '';

  constructor(private followService: FollowService) {

  }

  ngOnInit(): void {
    this.followService.getAllFollows()
    this.followService.allFollows.subscribe(result => {
      this.allFriends = result;
    })
  }

  searchFollowedUsersByName() {
    if(this.searchParam == null || this.searchParam == '') {
      this.followService.getAllFollows()
      this.followService.allFollows.subscribe(result => {
        this.allFriends = result;
      })
    }
    else {
    this.followService.searchFollowedUsersByName(this.searchParam);
    this.followService.followedSearchResults.subscribe(result => {
      this.allFriends = result;
    })
  }
  }
}
