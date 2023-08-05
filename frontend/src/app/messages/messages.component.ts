import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  ngOnInit() {
    this.scrollToBottom();
  }

  scrollToBottom() {
    var scrollableContent = document.getElementById('scrollable-content');
    scrollableContent!.scrollTop = scrollableContent!.scrollHeight;
  }

}
