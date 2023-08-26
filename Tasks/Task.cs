using System.ComponentModel.DataAnnotations.Schema;

namespace Rajware.Sample.Tasks;

[Table("tasks")]
public class Task
{
    [Column("id")]
    public int ID { get; set; }
    [Column("description")]
    public string Description {get;set;} = "";
    [Column("deadline")]
    public DateTime Deadline {get;set;}
    [Column("completed")]
    public bool Completed { get; set; }
}
