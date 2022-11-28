import { Component, OnInit } from '@angular/core';
import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { ScheduleService } from '../schedule.service';
import { Observable } from 'rxjs';
import { ScheduleEvent } from '../model/schedule.model';

class ScheduleDataSource extends DataSource<ScheduleEvent> {

  constructor(private scheduleService: ScheduleService) {
    super();
  }

  connect(_: CollectionViewer): Observable<readonly ScheduleEvent[]> {
    return this.scheduleService.getEvents();
  }
  
  disconnect(_: CollectionViewer): void {
    // no action
  }
}

@Component({
  selector: 'app-class-schedule',
  templateUrl: './class-schedule.component.html',
  styleUrls: ['./class-schedule.component.css']
})
export class ClassScheduleComponent implements OnInit {

  public readonly dataSource: ScheduleDataSource;
  public readonly columnsToDisplay = ['name', 'recurrences', 'description'];

  constructor(scheduleService: ScheduleService) {
    this.dataSource = new ScheduleDataSource(scheduleService);
  }

  ngOnInit(): void {
  }

}
