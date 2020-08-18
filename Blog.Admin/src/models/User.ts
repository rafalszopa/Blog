export default class User {
    FirstName: string = '';
    LastName: string = '';
    Roles: string[] = [];

    constructor() {
        this.FirstName = '';
        this.LastName = '';
        this.Roles = new Array<string>();
    }
}