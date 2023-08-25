namespace Rajware.Sample.Tasks;

public class Task
{
    public int ID { get; set; }
    public string Description {get;set;} = "";
    public DateTime Deadline {get;set;}
    public bool Completed { get; set; }
}
