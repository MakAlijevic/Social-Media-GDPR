import { Component } from '@angular/core';
import { User } from 'src/models/User.model';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  allUsers! : User[];

  constructor(private userService: UserService) {

  }

  ngOnInit(): void {
    this.userService.searchResults.subscribe(result => {
      this.allUsers = result;
    })
  }
}
