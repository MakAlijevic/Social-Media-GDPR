export class User {
    public userId: string;
    public firstName: string;
    public lastName: string;
    public email: string;
    public isOnline: boolean;
    public createdAt: string;
    public isFollowed: boolean;

    constructor(userId: string, firstName: string, lastName: string, email: string, isOnline: boolean, createdAt: string, isFollowed: boolean ) {
        this.userId = userId;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.isOnline = isOnline;
        this.createdAt = createdAt;
        this.isFollowed = isFollowed;
    }
}