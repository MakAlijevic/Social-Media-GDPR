import { Component } from '@angular/core';

@Component({
  selector: 'app-profile-posts',
  templateUrl: './profile-posts.component.html',
  styleUrls: ['./profile-posts.component.css']
})
export class ProfilePostsComponent {

  comments = false;

  showComments() {
    if (this.comments == false) {
      this.comments = true;
    } else {
      this.comments = false;
    }
  }

}
