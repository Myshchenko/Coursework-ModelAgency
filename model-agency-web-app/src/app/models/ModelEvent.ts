import { Event } from "./Event";

export interface ModelEvent extends Event {
    acceptingType: string;
}