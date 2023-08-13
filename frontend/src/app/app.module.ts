import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { FriendsComponent } from './friends/friends.component';
import { PostComponent } from './home/post/post.component';
import { FriendsPageComponent } from './friends-page/friends-page.component';
import { FriendProfileComponent } from './friends-page/friend-profile/friend-profile.component';
import { ProfileComponent } from './profile/profile.component';
import { MessagesComponent } from './messages/messages.component';
import { MessagesFriendsComponent } from './messages/messages-friends/messages-friends.component';
import { SearchComponent } from './search/search.component';
import { SearchProfileComponent } from './search/search-profile/search-profile.component';
import { GdprModalComponent } from './gdpr-modal/gdpr-modal.component';
import { CommentComponent } from './home/comment/comment.component';
import { MessageBubbleComponent } from './messages/message-bubble/message-bubble.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    HomeComponent,
    FriendsComponent,
    PostComponent,
    FriendsPageComponent,
    FriendProfileComponent,
    ProfileComponent,
    MessagesComponent,
    MessagesFriendsComponent,
    SearchComponent,
    SearchProfileComponent,
    GdprModalComponent,
    CommentComponent,
    MessageBubbleComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
