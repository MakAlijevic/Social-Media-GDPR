import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class PostService {

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
        this.resetCreatePostFrom();
      },
      error: (result) => {
        alert("There was an error while posting your post : " + result.error);
      }
    });
  }

  resetCreatePostFrom() {
    const postContent = document.getElementById("createPostContent") as HTMLInputElement;
    postContent.value = "";
  }
}
