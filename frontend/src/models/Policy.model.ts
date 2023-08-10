export class Policy {
    public id: string;
    public name: string;
    public content: string;
    public createdAt: string;

    constructor(id: string, name: string, content: string, createdAt: string) {
        this.id = id;
        this.name = name;
        this.content = content;
        this.createdAt = createdAt;
    }
}