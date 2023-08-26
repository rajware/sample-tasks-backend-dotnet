using Microsoft.EntityFrameworkCore;
using Rajware.Sample.Tasks.EFCoreRepo;

internal partial class Program
{
    private record DBSetupResult(bool Success, string Message);

    private static readonly Dictionary<string, Func<WebApplicationBuilder, DBSetupResult>> 
        dbOptions = new(){
            {"development", SetupInMemory},
            {"inmemory", SetupInMemory},
            {"postgres", SetupPostGres}
    };

    private static DBSetupResult SetupDB(WebApplicationBuilder builder)
    {
        var dbOption = builder.Configuration["TASKS_STORAGE"]?.ToLowerInvariant() ?? "development";
        if (!dbOptions.Keys.Contains(dbOption))
        {
            return new(false,string.Format("no such storage option: {0}",dbOption));
        }
        var setupFunc = dbOptions[dbOption];
        return setupFunc(builder);
    }

    private static DBSetupResult SetupInMemory(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<TasksDb>(opt => opt.UseInMemoryDatabase("TasksDB"));
        return new(true,"successfully configured InMemory storage");
    }

    private static DBSetupResult SetupPostGres(WebApplicationBuilder builder)
    {
        var host = builder.Configuration["TASKS_DBSERVER"] ?? "db";
        var port = builder.Configuration["TASKS_DBPORT"] ?? "5432";
        var user = builder.Configuration["TASKS_USERNAME"] ?? "";
        var pswd = builder.Configuration["TASKS_PASSWORD"] ?? "";
        var dbnm = builder.Configuration["TASKS_DBNAME"] ?? "";

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pswd) || string.IsNullOrEmpty(dbnm))
        {
            return new(false, "Postgres configuration options not provided. Exiting.");
        }

        var dsn = string.Format(
            @"Host={0};Port={1};Username={2};Password={3};Database={4}",
            host, port, user, pswd, dbnm
        );
        builder.Services.AddDbContext<TasksDb>(
           opt => opt.UseNpgsql(dsn)
        );
        return new(true,"successfully configured Postgres storage");
    }
}

