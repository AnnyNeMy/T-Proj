import { BehaviorSubject, Subject } from "rxjs";

export class CommonVariables {
    public static updatedDate: Date = new Date();

}

export const UserNameChanged: BehaviorSubject<string> = new BehaviorSubject<string>("Войти");

