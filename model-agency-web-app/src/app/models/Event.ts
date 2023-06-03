import { EventType } from "./EventType";

export interface Event {
    id: number;
    details: string;
    createdBy?: number;
    eventType: string;
    targetDate: Date;
    address: string;
    createdAt?: Date;
}