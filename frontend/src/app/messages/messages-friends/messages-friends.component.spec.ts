import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MessagesFriendsComponent } from './messages-friends.component';

describe('MessagesFriendsComponent', () => {
  let component: MessagesFriendsComponent;
  let fixture: ComponentFixture<MessagesFriendsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MessagesFriendsComponent]
    });
    fixture = TestBed.createComponent(MessagesFriendsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
