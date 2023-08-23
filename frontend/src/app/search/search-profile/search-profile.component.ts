import { Component, Input } from '@angular/core';
import { User } from 'src/models/User.model';

@Component({
  selector: 'app-search-profile',
  templateUrl: './search-profile.component.html',
  styleUrls: ['./search-profile.component.css']
})
export class SearchProfileComponent {
  @Input() userData!: User;
}
