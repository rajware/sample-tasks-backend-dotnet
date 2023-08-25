using Microsoft.EntityFrameworkCore;
using RT=Rajware.Sample.Tasks;

namespace Rajware.Sample.Tasks.EFCoreRepo;
public class TasksDb : DbContext
{
    public DbSet<RT.Task> Tasks => Set<RT.Task>();
    public TasksDb(DbContextOptions<TasksDb> options)
            : base(options) { }
}
