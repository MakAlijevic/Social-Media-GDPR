export class Message {
    public id: string;
    public senderId: string;
    public recieverId: string;
    public content: string;
    public isRead: boolean;
    public createdAt: string;

    constructor(id: string, senderId: string, recieverId: string, content: string, isRead: boolean, createdAt: string) {
        this.id = id;
        this.senderId = senderId;
        this.recieverId = recieverId;
        this.content = content;
        this.isRead = isRead;
        this.createdAt = createdAt;
    }
}