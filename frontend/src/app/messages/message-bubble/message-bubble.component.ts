import { Component, Input, OnInit } from '@angular/core';
import { Message } from 'src/models/Message.model';
import { AuthService } from 'src/services/auth.service';

@Component({
  selector: 'app-message-bubble',
  templateUrl: './message-bubble.component.html',
  styleUrls: ['./message-bubble.component.css']
})
export class MessageBubbleComponent implements OnInit {

  @Input() message!: Message;
  loggedUserId!: string;

  constructor(private authService: AuthService) {

  }
  ngOnInit(): void {
    const userToken = this.authService.getUserTokenAndDecode();
    this.loggedUserId = userToken.serialNumber;
  }
}
