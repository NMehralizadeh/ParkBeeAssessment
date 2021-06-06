import { Door } from './Door';

export interface GarageDetailInfo {
  garageId: number;
  name: string;
  address: string;
  doors: Door[];
  doorCount: number;
  onlineDoorCount: number;
  offlineDoorCount: number;
}
