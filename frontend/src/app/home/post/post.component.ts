import { Component, Input, OnInit } from '@angular/core';
import { Post } from 'src/models/Post.model';
import { AuthService } from 'src/services/auth.service';
import { PostService } from 'src/services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  @Input() post!: Post
  comments = false;
  authUser = "";

  constructor(private postService: PostService, private authService : AuthService) {
  }

  ngOnInit(): void {
    const userToken = this.authService.getUserTokenAndDecode();
    this.authUser = userToken.serialNumber;
  }

  showComments() {
    if (this.comments == false) {
      this.comments = true;
    } else {
      this.comments = false;
    }
  }

  addCommentToPost() {
    var comment = document.getElementById("createCommentForm-" + this.post.id) as HTMLInputElement;
    if (comment.value !== null && comment.value !== "") {
      this.postService.addCommentToPost(this.post.id, comment.value);
    }
  }

  likePost() {
    this.postService.likePost(this.post.id, (success) => {
      if(success === true) {
        this.post.isLiked = true;
        this.post.likes ++;
      }
  });
}

  unlikePost() {
    this.postService.unlikePost(this.post.id, (success) => {
      if(success === true) {
        this.post.isLiked = false;
        this.post.likes --;
      }
  });
 }

 deletePost() {
  this.postService.deletePost(this.post.id)
 }
}
