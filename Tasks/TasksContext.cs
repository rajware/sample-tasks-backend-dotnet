namespace Rajware.Sample.Tasks;

public class TasksContext {
    private ITaskRepository repo;

    public Task AddTask(string description, DateTime deadline) {
        var newTask = new Task(){
            Description=description,
            Deadline=deadline,
            Completed=false
        };

        var result = repo.Add(newTask);
        return result;
    }

    public List<Task> GetAll() {
        return repo.GetAll();
    }
    public Task GetByID(uint id) {
        var result = repo.GetById(id);
        return result;
    }

    public Task Update(Task task) {
        var result = repo.Update(task);
        return result;
    }

    public Task DeleteByID(uint id) {
        return repo.DeleteByID(id);
    }
    public TasksContext(ITaskRepository repo) {
        this.repo = repo;
    }
}