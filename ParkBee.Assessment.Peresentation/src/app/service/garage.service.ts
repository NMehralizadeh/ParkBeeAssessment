import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { GarageDetailInfo } from '../viewModels/GarageDetailInfo';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class GarageService {
  constructor(private http: HttpClient) {}

  garageControllerUrl: string = `${environment.baseUrl}`;
  getGarageDetailInfo(): Observable<GarageDetailInfo> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${environment.token}`,
    });
    return this.http.get<GarageDetailInfo>(
      `${this.garageControllerUrl}api/Garage`,
      {
        headers: headers,
      }
    );
  }

  refreshDoorStatus(doorId: number): Observable<boolean> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${environment.token}`,
    });
    return this.http.post<boolean>(
      `${this.garageControllerUrl}api/Garage/RefreshStatus/${doorId}`,
      {},
      {
        headers: headers,
      }
    );
  }
}
