import { Component, Input } from '@angular/core';
import { Post } from 'src/models/Post.model';
import { PostService } from 'src/services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent {

  constructor(private postService: PostService) {
  }

  @Input() post!: Post
  comments = false;

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

}
