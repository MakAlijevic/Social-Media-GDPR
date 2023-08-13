export class Comment {
    public id: string;
    public author: string;
    public firstName: string;
    public lastName: string;
    public email: string;
    public content: string;
    public createdAt: string;

    constructor(id: string, author: string, firstName: string, lastName: string, email: string, content: string, createdAt: string) {
        this.id = id;
        this.author = author;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.content = content;
        this.createdAt = createdAt;
    }
}