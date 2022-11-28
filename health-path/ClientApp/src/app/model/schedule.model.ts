
interface ScheduleEventRecurrence {
    readonly id: string;
    readonly eventId: string;
    readonly dayOfWeek: number;
    readonly dayOfWeekName: string;
    readonly endTime: string;
    readonly startTime: string;
}

interface ScheduleEvent {
    readonly id: string;
    readonly name: string;
    readonly description: string;
    readonly recurrences: readonly ScheduleEventRecurrence[];
}

export {ScheduleEvent, ScheduleEventRecurrence};
