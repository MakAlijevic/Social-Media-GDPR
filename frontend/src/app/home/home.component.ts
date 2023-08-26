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
  currentPage: number = 1;
  totalPages = 0;
  pageNumbersArray! : number[];

  constructor(private postService: PostService) {

  }

  ngOnInit(): void {
    this.loadDashboardPosts(this.currentPage);
    this.getTotalPages();
  }

  loadDashboardPosts(pageNumber: number) {
    this.postService.getDashboardPosts(pageNumber);
    this.postService.dashboardPosts.subscribe(result => {
      this.dashboardPosts = result;
    })
  }

  onPageChange(pageNumber: number) {
    this.currentPage = pageNumber;
    this.loadDashboardPosts(pageNumber);
  }
  
  getTotalPages(): void {
    this.postService.getTotalAmountOfPosts().subscribe(totalPosts => {
      const pageSize = 5; 
      this.totalPages = Math.ceil(totalPosts / pageSize);
      this.pageNumbersArray = Array.from({ length: this.totalPages }, (_, index) => index + 1);
    });
  }

  addNewPost() {
    const postContent = document.getElementById("createPostContent") as HTMLInputElement;
    if (postContent.value !== null && postContent.value !== "") {
      this.postService.addNewPost(postContent.value);
    }
  }

}
