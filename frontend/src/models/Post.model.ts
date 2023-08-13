import { Comment } from 'src/models/Comment.model';

export class Post {
    public id: string;
    public author: string;
    public firstName: string;
    public lastName: string;
    public email: string;
    public content: string;
    public createdAt: string;
    public comments: Comment[];
    public likes: number;

    constructor(id: string, author: string, firstName: string, lastName: string, email: string, content: string, createdAt: string, comments: Comment[], likes: number) {
        this.id = id;
        this.author = author;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.content = content;
        this.createdAt = createdAt;
        this.comments = comments;
        this.likes = likes;
    }
}