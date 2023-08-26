internal partial class Program
{
    private static void SetupDevOnlyApis(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        app.MapGet("/conf", (HttpContext context, IConfiguration configuration) =>
        {
            return configuration;
        });

        app.MapGet("/confs", (IConfiguration configroot) =>
        {
            var Configroot = (IConfigurationRoot)configroot;
            string str = "";
            foreach (var provider in Configroot.Providers.ToList())
            {
                str += provider.ToString() + "\n";
            }

            return str;
        });

        app.MapGet("/conf/{id}", (string id, IConfiguration configuration) =>
        {
            app.Logger.LogInformation("Lassoon: {0}, id: {1}", configuration, id);
            return configuration[id];
        });
    }
}