import { Component } from '@angular/core';
import { PostService } from 'src/services/post.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(private postService: PostService) {

  }

  addNewPost() {
    const postContent = document.getElementById("createPostContent") as HTMLInputElement;
    this.postService.addNewPost(postContent.value);
  }

}
