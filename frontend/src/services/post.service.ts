import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Post } from 'src/models/Post.model';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  public dashboardPosts = new BehaviorSubject<Post[]>([]);
  public userProfilePosts = new BehaviorSubject<Post[]>([]);

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
        this.getDashboardPosts(1);
      },
      error: (result) => {
        alert("There was an error while posting your post : " + result.error);
      }
    });
  }

  getDashboardPosts(pageNumber: number) {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const pageSize = 5;
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };

    this.http.get<Post[]>("https://localhost:7243/api/Post/GetDashboardPostsByUserId?userId=" + userId + "&pageNumber=" + pageNumber + "&pageSize=" + pageSize, requestOptions).subscribe({
      next: (result) => {
        this.dashboardPosts.next(result);
      },
      error: (result) => {
        alert("Error while loading dashboard posts : " + result.error);
      }
    })
  }

  getTotalAmountOfPosts(): Observable<number> {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };
  
    return this.http.get<number>("https://localhost:7243/api/Post/GetTotalAmountOfPosts?userId=" + userId, requestOptions);
  }

  getUserProfilePosts() {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };

    this.http.get<Post[]>("https://localhost:7243/api/Post/GetProfilePostsByUserId?userId=" + userId, requestOptions).subscribe({
      next: (result) => {
        this.userProfilePosts.next(result);
      },
      error: (result) => {
        alert("Error while loading profile posts : " + result.error);
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
        this.getDashboardPosts(1);
        this.getUserProfilePosts();
      },
      error: (result) => {
        alert("Error while adding your comment : " + result.error);
      }
    });
  }

  likePost(postId: string , callback: (success: boolean) => void): void {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestBody = {
      author: userId,
      postId: postId
    }
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!)
    };

    this.http.post("https://localhost:7243/api/Like", requestBody, requestOptions).subscribe({
      next: () => {
        callback(true);
      },
      error: (result) => {
        alert("Error while adding your like : " + result.error);
        callback(false);
      }
    });
  }

  
  unlikePost(postId: string, callback: (success: boolean) => void): void {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestBody = {
      author: userId,
      postId: postId
    }
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!),
      responseType: 'text',
      body: requestBody
    };

    this.http.delete("https://localhost:7243/api/Like", requestOptions).subscribe({
      next: () => {
        callback(true);
      },
      error: (result) => {
        alert("Error while adding your like : " + result.error);
        callback(false);
      }
    });
  }

  resetCreatePostForm() {
    const postContent = document.getElementById("createPostContent") as HTMLInputElement;
    postContent.value = "";
  }

  deletePost(postId: string) {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestBody = {
      author: userId,
      postId: postId
    }
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!),
      responseType: 'text',
      body: requestBody
    };

    this.http.delete("https://localhost:7243/api/Post", requestOptions).subscribe({
      next: () => {
        this.getDashboardPosts(1);
        this.getUserProfilePosts();
      },
      error: (result) => {
        alert("Error while deleting your post : " + result.error);
      }
    });
  }

  deleteComment(postId: string, commentId: string) {
    const userToken = this.authService.getUserTokenAndDecode();
    const userId = userToken.serialNumber;
    const requestBody = {
      author: userId,
      commentId: commentId
    }
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', "bearer " + localStorage.getItem('userToken')!),
      responseType: 'text',
      body: requestBody
    };

    this.http.delete("https://localhost:7243/api/Comment", requestOptions).subscribe({
      next: () => {
        this.getDashboardPosts(1);
        this.getUserProfilePosts();
      },
      error: (result) => {
        alert("Error while deleting your post : " + result.error);
      }
    });
  }
}
