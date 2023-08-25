namespace Rajware.Sample.Tasks;

public interface ITaskRepository {
    public List<Task> GetAll();
    public Task GetById(uint taskID);
    public Task Add(Task task);

    public Task Update(Task task);
    public Task DeleteByID(uint taskID);
}