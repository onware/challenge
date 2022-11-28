import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { ScheduleEvent } from './model/schedule.model';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  private readonly scheduleUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.scheduleUrl = baseUrl + "api/schedule";
  }

  getEvents() {
    return this.http.get(this.scheduleUrl, { "observe": "body", "responseType": "json" }).pipe(map(x => x as readonly ScheduleEvent[]));
  }
}