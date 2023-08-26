using Microsoft.EntityFrameworkCore;
using Rajware.Sample.Tasks.EFCoreRepo;
using RT = Rajware.Sample.Tasks;
using Microsoft.Extensions.FileProviders;

internal class Program
{

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

    private static RT.TasksContext GetTasksCotext(TasksDb db)
    {
        var repo = new EFCoreRepo(db);
        return new RT.TasksContext(repo);
    }
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Inject database driver
        builder.Services.AddDbContext<TasksDb>(opt => opt.UseInMemoryDatabase("TasksDB"));
       
        var app = builder.Build();

        // Serve static content from the wwwroot directory.
        // index.html is the default file.
        var webroot = Path.Combine(builder.Environment.ContentRootPath, "wwwroot");
        var fileProvider = new PhysicalFileProvider(webroot);
        var dfOptions = new DefaultFilesOptions()
        {
            FileProvider = fileProvider,
            RequestPath = "",
            DefaultFileNames = new string[] { "index.html" }
        };
        var sfOptions = new StaticFileOptions()
        {
            FileProvider = fileProvider,
            RequestPath = ""
        };

        app.UseDefaultFiles(dfOptions);
        app.UseStaticFiles(sfOptions);

        // Set up the Tasks api
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
            return SendResult(
                () => tc.Update(t)
            );
        });

        app.MapDelete("/tasks/{id}", (uint id, TasksDb db) =>
        {
            var tc = GetTasksCotext(db);

            return SendResult(() => tc.DeleteByID(id));

        });

        app.Run();
    }
}