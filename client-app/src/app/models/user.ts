export interface User {
     userName: string;
     displayName: string;
     token: string;
     image?: string;
}

export interface UserFormValue {
     email?: string;
     passwod?: string;
     displayName?: string;
     userName?: string;
}