import { Component, Input } from '@angular/core';
import { User } from 'src/models/User.model';

@Component({
  selector: 'app-messages-friends',
  templateUrl: './messages-friends.component.html',
  styleUrls: ['./messages-friends.component.css']
})
export class MessagesFriendsComponent {
  @Input() friend!: User

}
