import { ECommonStatus } from "./ECommonStatus";

export interface CommonResponse<T = any> {
     Status: ECommonStatus;
     Message: string;
     Data: T;
}