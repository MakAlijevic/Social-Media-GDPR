import { Component, Input } from '@angular/core';
import { User } from 'src/models/User.model';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent {

  @Input() userData!: User;

}
