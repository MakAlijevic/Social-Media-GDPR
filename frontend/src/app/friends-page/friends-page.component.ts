import { Component, OnInit } from '@angular/core';
import { User } from 'src/models/User.model';
import { FollowService } from 'src/services/follow.service';

@Component({
  selector: 'app-friends-page',
  templateUrl: './friends-page.component.html',
  styleUrls: ['./friends-page.component.css']
})
export class FriendsPageComponent implements OnInit {

  public allFriends!: User[];


  constructor(private followService: FollowService) {

  }

  ngOnInit(): void {
    this.followService.getAllFollows();
    this.followService.allFollows.subscribe(result => {
      this.allFriends = result;
    })
  }

}
