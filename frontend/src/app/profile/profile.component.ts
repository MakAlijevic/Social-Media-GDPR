import { Component, OnInit } from '@angular/core';
import { Post } from 'src/models/Post.model';
import { User } from 'src/models/User.model';
import { PostService } from 'src/services/post.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  userProfileData!: User;
  userProfilePosts!: Post[];

  constructor(private userService: UserService, private postService: PostService) {

  }
  ngOnInit(): void {
    this.userService.getUserProfileData();
    this.userService.userProfileData.subscribe(result => {
      this.userProfileData = result;
    });
    this.postService.userProfilePosts.subscribe(result => {
      this.userProfilePosts = result;
    })
  }

}
