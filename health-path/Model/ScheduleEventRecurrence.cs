namespace health_path.Model;

public class ScheduleEventRecurrence
{
    public Guid Id {get; set; }
    public Guid EventId {get; set; }
    public int DayOfWeek { get; set; }
    public string DayOfWeekName => DayOfWeek switch {
        0 => "Sunday",
        1 => "Monday",
        2 => "Tuesday",
        3 => "Wednesday",
        4 => "Thursday",
        5 => "Friday",
        _ => "Saturday",
    };
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
