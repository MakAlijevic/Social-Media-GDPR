import { Component, Input, OnInit } from '@angular/core';
import { Comment } from 'src/models/Comment.model';
import { Post } from 'src/models/Post.model';
import { AuthService } from 'src/services/auth.service';
import { PostService } from 'src/services/post.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  
  @Input() comment!: Comment
  @Input() postId!: string
  authUser = "";

  constructor(private authService: AuthService, private postService: PostService){

  }

  ngOnInit(): void {
    const userToken = this.authService.getUserTokenAndDecode();
    this.authUser = userToken.serialNumber;
  }

  deleteComment() {
    this.postService.deleteComment(this.postId, this.comment.id)
   }

}
