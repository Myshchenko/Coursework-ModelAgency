import { User } from "./User";

export interface Model extends User {
    height: number;
    weight: number;
    chest: number;
    waist: number;
    hips: number;
    shoes: number;
    hair: string;
  }