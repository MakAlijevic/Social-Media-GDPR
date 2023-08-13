import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Post } from 'src/models/Post.model';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  public dashboardPosts = new BehaviorSubject<Post[]>([]);

  constructor(private http: HttpClient, private authService: AuthService) { }

  addNewPost(postContent: string) {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestBody = {
      author: userId,
      content: postContent
    };
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };

    this.http.post("https://localhost:7243/api/Post", requestBody, requestOptions).subscribe({
      next: () => {
        alert("Post successfully posted!");
        this.resetCreatePostForm();
        this.getDashboardPosts();
      },
      error: (result) => {
        alert("There was an error while posting your post : " + result.error);
      }
    });
  }

  getDashboardPosts() {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };

    this.http.get<Post[]>("https://localhost:7243/api/Post/GetDashboardPostsByUserId?userId=" + userId, requestOptions).subscribe({
      next: (result) => {
        this.dashboardPosts.next(result);
      },
      error: (result) => {
        alert("Error while loading dashboard posts : " + result.error);
      }
    })
  }

  addCommentToPost(postId: string, content: string) {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestBody = {
      author: userId,
      postId: postId,
      content: content
    }
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };

    this.http.post("https://localhost:7243/api/Comment", requestBody, requestOptions).subscribe({
      next: () => {
        alert("Comment added successfully");
        this.getDashboardPosts();
      },
      error: (result) => {
        alert("Error while adding your comment : " + result.error);
      }
    })
  }

  resetCreatePostForm() {
    const postContent = document.getElementById("createPostContent") as HTMLInputElement;
    postContent.value = "";
  }
}
