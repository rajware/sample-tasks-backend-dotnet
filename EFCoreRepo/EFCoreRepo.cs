using Rajware.Sample.Tasks;

namespace Rajware.Sample.Tasks.EFCoreRepo;

public class EFCoreRepo : ITaskRepository
{
    private readonly TasksDb db;
    public Task Add(Task task)
    {
        var result = db.Tasks.Add(task);
        db.SaveChanges();
        return result.Entity;
    }

    public Task DeleteByID(uint taskID)
    {
        var oldTask = GetById(taskID);
        db.Tasks.Remove(oldTask);
        db.SaveChanges();
        return oldTask;
    }

    public List<Task> GetAll()
    {
        return db.Tasks.ToList();
    }

    public Task GetById(uint taskID)
    {
        var result = db.Tasks.First((t) => t.ID == taskID)
                     ?? throw new KeyNotFoundException($"id ${taskID} not found");
        return result;
    }

    public Task Update(Task task)
    {
        var result = db.Tasks.Update(task);
        db.SaveChanges();
        return result.Entity;
    }

    public EFCoreRepo(TasksDb db) {
        this.db = db;
    }
}