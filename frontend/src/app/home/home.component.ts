import { Component, OnInit } from '@angular/core';
import { Post } from 'src/models/Post.model';
import { PostService } from 'src/services/post.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  dashboardPosts!: Post[];

  constructor(private postService: PostService) {

  }
  ngOnInit(): void {
    this.postService.getDashboardPosts();
    this.postService.dashboardPosts.subscribe(result => {
      this.dashboardPosts = result;
    })
  }

  addNewPost() {
    const postContent = document.getElementById("createPostContent") as HTMLInputElement;
    if (postContent.value !== null && postContent.value !== "") {
      this.postService.addNewPost(postContent.value);
    }
  }

}
