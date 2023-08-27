using RT = Rajware.Sample.Tasks;
using Rajware.Sample.Tasks.EFCoreRepo;

internal partial class Program
{
    private static void SetupTasksApi(WebApplication app)
    {
        app.MapGet("/tasks", (TasksDb db) =>
        {
            var tc = GetTasksCotext(db);
            return SendResult(() => tc.GetAll());
        });

        app.MapGet("/tasks/{id}", (uint id, TasksDb db) =>
        {
            var tc = GetTasksCotext(db);
            return SendResult(() => tc.GetByID(id));
        });

        app.MapPost("/tasks", (RT.Task t, TasksDb db) =>
        {
            var tc = GetTasksCotext(db);
            return SendResult(
                () => tc.AddTask(t.Description, t.Deadline)
            );
        });

        app.MapPut("/tasks", (RT.Task t, TasksDb db) =>
        {
            var tc = GetTasksCotext(db);
            return SendResult(() => tc.Update(t));
        });

        app.MapDelete("/tasks/{id}", (uint id, TasksDb db) =>
        {
            var tc = GetTasksCotext(db);
            return SendResult(() => tc.DeleteByID(id));
        });
    }

    private static RT.TasksContext GetTasksCotext(TasksDb db)
    {
        var repo = new EFCoreRepo(db);
        return new RT.TasksContext(repo);
    }

    // SendResult wraps any data to be sent to the client in
    // an ApiResult object.
    private static IResult SendResult<T>(Func<T> f)
    {
        try
        {
            T result = f();
            return Results.Ok(
                new ApiResult
                {
                    Data = result,
                    Error = 0,
                    Message = ""
                });
        }
        catch (InvalidOperationException)
        {
            return Results.NotFound(
                new ApiResult
                {
                    Data = null,
                    Error = 404,
                    Message = "not found"
                });
        }
        catch (Exception e)
        {
            return Results.Json(
                data: new ApiResult
                {
                    Data = null,
                    Error = 500,
                    Message = e.Message
                },
                statusCode: 500
            );
        }
    }
}