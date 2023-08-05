import { Component } from '@angular/core';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent {

  comments = false;

  showComments() {
    if (this.comments == false) {
      this.comments = true;
    } else {
      this.comments = false;
    }
  }

}
