import { Component, Input } from '@angular/core';
import { User } from 'src/models/User.model';

@Component({
  selector: 'app-friend-profile',
  templateUrl: './friend-profile.component.html',
  styleUrls: ['./friend-profile.component.css']
})
export class FriendProfileComponent {

  @Input() userData!: User;

}
