using Microsoft.Extensions.FileProviders;

internal partial class Program
{
    static void SetupStaticServing(WebApplicationBuilder builder, WebApplication app)
    {
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
    }
}