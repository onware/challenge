namespace health_path.Model;

public class ScheduleEvent
{
    public Guid Id {get; set;}
    public string Name {get; set;} = "";
    public string Description {get; set;} = "";

    public List<ScheduleEventRecurrence> Recurrences {get; set;} = new List<ScheduleEventRecurrence>{};
}
