internal partial class Program
{
    private static int Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Inject database driver
        var dbInjected = SetupDB(builder);

        var app = builder.Build();

        if (!dbInjected.Success)
        {
            app.Logger.LogCritical("Critical error: {0}", dbInjected.Message);
            return 1;
        }
        app.Logger.LogInformation("Storage initialized: {0}", dbInjected.Message);

        // Set up static content
        SetupStaticServing(builder, app);

        SetupDevOnlyApis(app);

        // Set up the Tasks api
        SetupTasksApi(app);

        app.Run();

        return 0;
    }
}