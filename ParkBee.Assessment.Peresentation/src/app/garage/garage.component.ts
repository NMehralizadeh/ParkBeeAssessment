import { Component, OnInit } from '@angular/core';
import { GarageService } from '../service/garage.service';
import { LoginService } from '../service/login.service';
import { MessageService } from '../service/message.service';
import { GarageDetailInfo } from '../viewModels/GarageDetailInfo';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-garage',
  templateUrl: './garage.component.html',
  styleUrls: ['./garage.component.css'],
})
export class GarageComponent implements OnInit {
  constructor(
    private garageServie: GarageService,
    private messageService: MessageService,
    private loginService: LoginService
  ) {}

  garageDetailInfo: GarageDetailInfo;

  ngOnInit(): void {
    this.loginService.login('admin', 'admin').subscribe((r) => {
      environment.token = r['token'];
      this.getGarageDetailInfo();
    });
  }

  getGarageDetailInfo() {
    this.garageServie.getGarageDetailInfo().subscribe((gs) => {
      this.garageDetailInfo = gs;

      this.computeValues();
    });
    this.messageService.add(`Status of my garage fetched! `);
  }

  refreshDoorStatus(doorId: number) {
    this.garageServie.refreshDoorStatus(doorId).subscribe((doorStatus) => {
      this.garageDetailInfo.doors = this.garageDetailInfo.doors.map((door) => {
        if (door.doorId == doorId) {
          door.isOnline = doorStatus;
        }
        return door;
      });
      this.computeValues();
    });
  }

  computeValues() {
    this.garageDetailInfo.onlineDoorCount = this.garageDetailInfo.doors.filter(
      (door) => door.isOnline == true
    ).length;
    this.garageDetailInfo.offlineDoorCount = this.garageDetailInfo.doors.filter(
      (door) => door.isOnline == false
    ).length;
    this.garageDetailInfo.doorCount =
      this.garageDetailInfo.onlineDoorCount +
      this.garageDetailInfo.offlineDoorCount;
  }
}
